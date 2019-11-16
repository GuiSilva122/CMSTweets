using Microsoft.Extensions.Options;
using System.Windows;

namespace CMSTweets
{
    public partial class MainWindow : Window
    {
        private readonly AppSettings _settings;
        
        public MainWindow(IOptions<AppSettings> paramSettings)
        {
            InitializeComponent();

            _settings = paramSettings.Value;
            Main.Content = new ConfigPage(_settings);
        }
    }
}
