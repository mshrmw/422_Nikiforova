using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _422_Nikiforova
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsTheme2 = false;
        public MainWindow()
        {
            InitializeComponent();
            ApplyTheme("Dictionary.xaml");
            MainFrame.Navigate(new Pages.AuthPage());
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => {
                DateTimeNow.Text = DateTime.Now.ToString();
            };
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть окно?", "Message", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.No)
            {
                e.Cancel = true;
            }    
            else e.Cancel = false;
        }

        private void ChangeThemeButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (IsTheme2)
                {
                    ApplyTheme("Dictionary.xaml");
                    this.Background = Brushes.Pink;
                    ChangeThemeButton.Content = "Темная тема";
                }
                else
                {
                    ApplyTheme("Dictionary2.xaml");
                    this.Background = Brushes.Green;
                    ChangeThemeButton.Content = "Светлая тема";
                }

                IsTheme2 = !IsTheme2;
            }
        }
        private void ApplyTheme(string themeFileName)
        {
            try
            {
                var uri = new Uri(themeFileName, UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки темы: {ex.Message}");
            }
        }
    }
}
