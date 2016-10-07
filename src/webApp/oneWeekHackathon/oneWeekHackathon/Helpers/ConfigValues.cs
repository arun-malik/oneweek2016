using System;
using System.Configuration;

namespace oneWeekHackathon.Helpers
{
    public class ConfigValues
    {
        public static string SmtpServer
        {

            get { return ConfigurationManager.AppSettings[Constants.SMTP_SERVER]; }

        }

        public static int SmtpPort
        {

            get { return Convert.ToInt32(ConfigurationManager.AppSettings[Constants.SMTP_PORT]); }

        }

        public static string FromEmail
        {

            get { return ConfigurationManager.AppSettings[Constants.FROM_EMAIL]; }

        }

        public static string FromEmailEncryptedPassword
        {

            get { return ConfigurationManager.AppSettings[Constants.FROM_EMAIL_ENCRYPTED_PASSWORD]; }

        }

        public static string AzureStorageConnection
        {

            get { return ConfigurationManager.ConnectionStrings[Constants.AZURE_STORAGE_CONNECTION].ConnectionString; }

        }

        public static string AzureContainerName
        {

            get { return ConfigurationManager.AppSettings[Constants.AZURE_CONTAINER_NAME]; }

        }
    }
}