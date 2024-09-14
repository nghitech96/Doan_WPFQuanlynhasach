using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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
using System.IO;
using System.Security.Cryptography;

namespace DemoRibbon
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Entropy { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void btn_login(object sender, RoutedEventArgs e)
        {
            AppConfig.Password = txtPassword.Password;
            AppConfig.Username = txtUserName.Text;

            /*var builder = new SqlConnectionStringBuilder();
            builder.TrustServerCertificate = true;
            builder.DataSource = Server;
            builder.UserID = Username;
            builder.Password = Password;
            builder.InitialCatalog = Database;*/


            var connectionString = AppConfig.ConnectionString();
            loadingProgressBar.IsIndeterminate = true;
            var (success, message, connection) = await Task.Run(() =>
            {
                var _connection = new SqlConnection(connectionString);
                bool success = true;
                string message = "";

                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ex.Message;
                }

                //Test khi chay qua nhanh

                return new Tuple<bool, string, SqlConnection>(success, message, _connection);
            });

            loadingProgressBar.IsIndeterminate = false;

            if (success)
            {
                MessageBox.Show("Login successfully");

                if (rememberPassCheckbox.IsChecked == true)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(AppConfig.Password);
                    var entropy = new byte[20];

                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(entropy);

                    }

                    var cypherText = ProtectedData.Protect(passwordInBytes, entropy, DataProtectionScope.CurrentUser);
                    AppConfig.Password = Convert.ToBase64String(cypherText);
                    AppConfig.Entropy = Convert.ToBase64String(entropy);
                    AppConfig.Save();

                    
                    /* Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    config.AppSettings.Settings["Username"].Value = Username;
                    config.AppSettings.Settings["Password"].Value = Convert.ToBase64String(cypherText);
                    config.AppSettings.Settings["Entropy"].Value = Convert.ToBase64String(entropy);
                    config.Save(ConfigurationSaveMode.Full);
                    System.Configuration.ConfigurationManager.RefreshSection("appSettings");*/

                }
                var screen = new MainWindow();
                screen.Show();

                this.Close();
            }
               
                else
                {
                    MessageBox.Show($"Cannot connect.");
                }
            
            
            /*string username = "admin";
            string password = "1234";

            if (username == "admin" && password == "1234")
            {
                var screen = new MainWindow();
                screen.Show();

                this.Close();*/

        }



        private void btn_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            AppConfig.Reload();

            Username = AppConfig.Username;
            Password = AppConfig.Password;

            DataContext = this;

            txtPassword.Password = AppConfig.Password;
        }

        private void ReloadSetting()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            Username = config["Username"] ?? "";
            var passwordIn64 = config["Password"] ?? "";
            var entropyIn64 = config["Entropy"] ?? "";
            Server = config["Server"] ?? "";
            Database = config["Database"] ?? "";

            if (passwordIn64.Length != 0)
            {
                var passwordInBytes = Convert.FromBase64String(passwordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64);
                var unencryptedPassword = ProtectedData.Unprotect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                Password = Encoding.UTF8.GetString(unencryptedPassword);

            }
        }

        private void setting(object sender, RoutedEventArgs e)
        {
            var screen = new Config();
            if (screen.ShowDialog() == true)
            {
                ReloadSetting();
            }
        }

        private void select(object sender, RoutedEventArgs e)
        {
            /*var config = new ConfigurationBuilder()
                .AddUserSecrets<LoginWindow>()
                .Build();

            var connectionString = config.GetSection("DB")["ConnectionString"];
            Debug.WriteLine(connectionString);

            var _connection = new SqlConnection(connectionString);
            string sql = "select * from UserLogin";

            var command = new SqlCommand(sql, _connection);
            _connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader[0];
                string name = (string)reader[1];

                Debug.WriteLine($"{id} - {name}");
            }*/
        }
    }
}




