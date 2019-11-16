using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CMSTweets
{
    public partial class UserControlTweetReceived : UserControl
    {
        public UserControlTweetReceived(String paramUser, String paramText, DateTime paramDate, String paramUserImageURL)
        {
            InitializeComponent();

            userImage.ImageSource = new BitmapImage(new Uri(paramUserImageURL));
            txtUser.Text = paramUser;
            txtTweet.Text = paramText;
            txtReceivedDate.Text = paramDate.ToString(((DateTime.UtcNow - paramDate).TotalDays > 1) ? "dd/MM/yyyy HH:mm" : "HH:mm");
        }
    }
}
