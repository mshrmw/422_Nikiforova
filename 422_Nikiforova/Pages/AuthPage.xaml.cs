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

namespace _422_Nikiforova.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int failedAttempts = 0;
        private User currentUser;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void ButtonChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEnter_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
