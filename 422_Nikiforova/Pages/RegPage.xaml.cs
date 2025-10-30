﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            comboBxRole.SelectedIndex = 0;
        }
        private void lblLogHitn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtbxLog.Focus();
        }
        private void lblPassHitn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            passBxFrst.Focus();
        }
        private void lblPassSecHitn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            passBxScnd.Focus();
        }
        private void lblFioHitn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtbxFIO.Focus();
        }
        private void txtbxLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblLogHitn.Visibility = Visibility.Visible;
            if (txtbxLog.Text.Length > 0)
            {
                lblLogHitn.Visibility = Visibility.Hidden;
            }
        }
        private void txtbxFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblFioHitn.Visibility = Visibility.Visible;
            if (txtbxFIO.Text.Length > 0)
            {
                lblFioHitn.Visibility = Visibility.Hidden;

            }
        }
        private void passBxFrst_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lblPassHitn.Visibility = Visibility.Visible;
            if (passBxFrst.Password.Length > 0)
            {
                lblPassHitn.Visibility = Visibility.Hidden;

            }
        }
        private void passBxScnd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lblPassSecHitn.Visibility = Visibility.Visible;
            if (passBxScnd.Password.Length > 0)
            {
                lblPassSecHitn.Visibility = Visibility.Hidden;

            }
        }
        public static string GetHash(String password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtbxLog.Text) || string.IsNullOrEmpty(txtbxFIO.Text) || string.IsNullOrEmpty(passBxFrst.Password) || string.IsNullOrEmpty(passBxScnd.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            DB_PaymentEntities db = new DB_PaymentEntities();
            {
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == txtbxLog.Text);
                if (user != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }
            }
            if (passBxFrst.Password.Length >= 6)
            {
                bool en = true;
                bool number = false;
                for (int i = 0; i < passBxFrst.Password.Length; i++)
                {
                    if (passBxFrst.Password[i] >= '0' && passBxFrst.Password[i] <= '9') number = true;
                    else if (!((passBxFrst.Password[i] >= 'A' && passBxFrst.Password[i] <= 'Z') || (passBxFrst.Password[i] >= 'a' && passBxFrst.Password[i] <= 'z'))) en = false;
                }
                if (!en) MessageBox.Show("Используйте только английскую расскладку!");
                else if (!number) MessageBox.Show("Добавьте хотя бы одну цифру!");
                if (en && number)
                {
                    if (passBxFrst.Password != passBxScnd.Password)
                    {
                        MessageBox.Show("Пароли не совпадают!");
                    }
                    else
                    {
                        string hashedPassword = GetHash(passBxFrst.Password);
                        User userObject = new User
                        {
                            FIO = txtbxFIO.Text,
                            Login = txtbxLog.Text,
                            Password = hashedPassword,

                            Role = comboBxRole.Text
                        };
                        db.User.Add(userObject);
                        db.SaveChanges();
                        MessageBox.Show("Пользователь успешно зарегистрирован!");
                        txtbxLog.Clear();
                        passBxFrst.Clear();
                        passBxScnd.Clear();
                        comboBxRole.SelectedIndex = 0;
                        txtbxFIO.Clear();
                        NavigationService?.Navigate(new AuthPage());

                    }
                }
            }
            else MessageBox.Show("Пароль слишком короткий, должно быть минимум 6 символов!");
            
        }
    }
}
