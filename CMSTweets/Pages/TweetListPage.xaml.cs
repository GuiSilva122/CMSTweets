using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using System.Collections.Generic;

namespace CMSTweets
{
    public partial class TweetListPage : Page
    {
        private readonly AppSettings _settings;
        private DispatcherTimer _timerToCheckNewTweets;
        private Int64 _sinceId;

        private Boolean _isFirstTime;
        private ITweet[] _lstTweetsMemory;

        private Int32 _currentIndex;

        public TweetListPage(AppSettings paramSettings, String paramHashTag)
        {   
            InitializeComponent();

            _settings = paramSettings;
            _lstTweetsMemory = new ITweet[_settings.NumberOfResults];
            _sinceId = 0;
            _isFirstTime = true;

            UpdateTitle(paramHashTag);
            Auth.SetUserCredentials(_settings.Consumer_Key, _settings.Consumer_Secret, _settings.AccessToken, _settings.AccessTokenSecret);
            TweetinviConfig.CurrentThreadSettings.TweetMode = TweetMode.Extended;
            TweetinviConfig.ApplicationSettings.TweetMode = TweetMode.Extended;

            InitTimer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ConfigPage(_settings));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timerToCheckNewTweets.Stop();
        }

        private void UpdateTitle(String paramTitle)
        {
            lblHashTag.Content = paramTitle;
        }

        private void InitTimer()
        {
            _timerToCheckNewTweets = new DispatcherTimer();
            _timerToCheckNewTweets.Tick += new EventHandler(CallbackTimerToCheckNewTweets);
            _timerToCheckNewTweets.Interval = new TimeSpan(0, 0, 0);
            _timerToCheckNewTweets.Start();
        }

        private void CallbackTimerToCheckNewTweets(object sender, EventArgs e)
        {
            try
            {
                QueryTwitter(lblHashTag.Content.ToString());
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Application error: {Ex.Message}", "Tweets", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void QueryTwitter(String paramHashtag)
        {
            try
            {
                var searchParameter = new SearchTweetsParameters(paramHashtag)
                {
                    SearchType = SearchResultType.Recent,
                    TweetSearchType = TweetSearchType.OriginalTweetsOnly,
                    MaximumNumberOfResults = _settings.NumberOfResults,
                    SinceId = _sinceId,
                    Since = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day),
                };

                List<ITweet> tweets = Search.SearchTweets(searchParameter).ToList();

                if (tweets.Count > 0)
                {
                    if (_isFirstTime)
                    {
                        InsertTweets(tweets);
                        _isFirstTime = false;
                    }
                    else if (tweets.Count == _settings.NumberOfResults)
                    {
                        stackPanelTweets.Children.Clear();
                        InsertTweets(tweets);
                    }
                    else
                    {
                        Int32 emptyPlaces = _settings.NumberOfResults - (_currentIndex + 1);

                        if (tweets.Count <= emptyPlaces)
                        {
                            UpdateTweetsMemory(tweets);
                            ShowNewTweetsOnScreen(tweets);
                        }
                        else
                        {
                            stackPanelTweets.Children.Clear();
                            Int32 numberToDelete = tweets.Count - emptyPlaces;
                            _currentIndex -= numberToDelete;
                            UpdateTweetsMemory(tweets);
                            ShowTweetsOnScreen();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Application error: {Ex.Message}", "Tweets", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _scrollViewer.ScrollToEnd();
                _timerToCheckNewTweets.Interval = new TimeSpan(0, 0, _settings.NumberOfSecondsToCheckNewTweets);
            }
        }

        private void ShowTweetsOnScreen()
        {
            for (Int32 i = _currentIndex; i >= 0; i--)
                stackPanelTweets.Children.Add(new UserControlTweetReceived(_lstTweetsMemory[i].CreatedBy.Name, _lstTweetsMemory[i].FullText, _lstTweetsMemory[i].CreatedAt, _lstTweetsMemory[i].CreatedBy.ProfileImageUrl400x400));
        }

        private void ShowNewTweetsOnScreen(List<ITweet> paramLstTweets)
        {
            for (Int32 i = paramLstTweets.Count - 1; i >= 0; i--)
                stackPanelTweets.Children.Add(new UserControlTweetReceived(_lstTweetsMemory[i].CreatedBy.Name, _lstTweetsMemory[i].FullText, _lstTweetsMemory[i].CreatedAt, _lstTweetsMemory[i].CreatedBy.ProfileImageUrl400x400));
        }

        private void InsertTweets(List<ITweet> paramLstTweets)
        {
            for (Int32 i = paramLstTweets.Count - 1; i >= 0; i--)
            {
                stackPanelTweets.Children.Add(new UserControlTweetReceived(paramLstTweets[i].CreatedBy.Name, paramLstTweets[i].FullText, paramLstTweets[i].CreatedAt, paramLstTweets[i].CreatedBy.ProfileImageUrl400x400));
                _lstTweetsMemory[i] = paramLstTweets[i];
                _sinceId = Math.Max(_sinceId, paramLstTweets[i].Id);
            }
            _currentIndex = paramLstTweets.Count - 1;
        }

        private void UpdateTweetsMemory(List<ITweet> paramLstTweets)
        {
            //Move os tweets antigos para o final da lista
            for (Int32 i = _currentIndex; i >= 0; i--)
                _lstTweetsMemory[i + paramLstTweets.Count] = _lstTweetsMemory[i];

            //Insere os novos tweets e atualiza o campo _sinceId
            for (Int32 i = 0; i < paramLstTweets.Count; i++)
            {
                _sinceId = Math.Max(_sinceId, paramLstTweets[i].Id);
                _lstTweetsMemory[i] = paramLstTweets[i];
            }
            
            //Atualiza o índice atual
            _currentIndex = _currentIndex + paramLstTweets.Count;
        }
    }
}
