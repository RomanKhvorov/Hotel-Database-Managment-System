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
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HotelManagementSystem.Repositories;
using HotelManagementSystem.Entities;
using HotelManagementSystem.DesktopUI.Code;

namespace HotelManagementSystem.DesktopUI
{
    public partial class Authorization : Window
    {
        private string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public Authorization()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = tbPassword.Password;

            var hashedPassword = new StringBuilder();

            using (MD5 md5Provider = new MD5CryptoServiceProvider())
            {
                byte[] bytes = md5Provider.ComputeHash(Encoding.ASCII.GetBytes(password));

                for (int i = 0; i < bytes.Length; i++)
                {
                    hashedPassword.Append(bytes[i].ToString("x2"));
                }
            }

            var adminsRep = new SqlAdministratorRepository(connection);

            var admin = adminsRep.GetAdministratorByLogin(login, hashedPassword.ToString());

            if (admin != null)
            {
                CurrentAdministrator.SignIn(admin);

                (new MainWindow()).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.", "Authorization error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}
