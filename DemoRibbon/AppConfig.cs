using Azure.Core.Pipeline;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Orc.Automation.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DemoRibbon
{
    public class AppConfig
    {
        public static string Username { get; set; } ="";
        public static string Password { get; set; } = "";
        public static string Server { get; set; } = "";
        public static string Database { get; set; } = "";
        public static string Entropy { get; set; } = "";

        public static void Reload()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;
            Username = config["Username"] ?? "";
            var PasswordIn64 = config["Password"] ?? "";
            var entropyIn64 = config["Entropy"] ?? "";
            Server = config["Server"] ?? "";
            Database = config["Database"] ?? "";

            if (PasswordIn64.Length != 0)
            {
                var passwordInBytes = Convert.FromBase64String(PasswordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64);

                try
                {
                    var unencryptedPassword = ProtectedData.Unprotect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                    Password = Encoding.UTF8.GetString(unencryptedPassword);
                }
                catch (CryptographicException ex)
                {
                    // Handle the exception or log the error
                    Console.WriteLine("Error decrypting password: " + ex.Message);
                }
            }
        }

        public static string ConnectionString()
        {
            Reload();
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = AppConfig.Server;
            builder.InitialCatalog = Database;
            builder.UserID = Username;
            builder.Password = Password;
            
            //builder.TrustServerCertificate = true;

            string connectionString = builder.ConnectionString;
            return connectionString;
        }

        public static void Save()
        {
            Configuration config = System.Configuration.ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Username"].Value = Username;
            config.AppSettings.Settings["Password"].Value = Password;
            config.AppSettings.Settings["Entropy"].Value = Entropy;
            config.Save(ConfigurationSaveMode.Full);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
