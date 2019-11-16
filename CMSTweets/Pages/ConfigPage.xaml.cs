using System;
using System.Windows;
using System.Windows.Controls;

namespace CMSTweets
{
    public partial class ConfigPage : Page
    {
        private readonly AppSettings _settings;
        public ConfigPage(AppSettings paramSettings)
        {
            _settings = paramSettings;
            InitializeComponent();
        }

        private void ComecarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtHashTag.Text) && txtHashTag.Text.Length > 1)
            {
                if (txtHashTag.Text[0] != '#')
                    txtHashTag.Text = "#" + txtHashTag.Text;

                this.NavigationService.Navigate(new TweetListPage(_settings, txtHashTag.Text));
            }
            else
                MessageBox.Show("Insira a #hashtag", "Tweets", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
