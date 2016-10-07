namespace oneWeekHackathon.Helpers
{
    public static class Constants
    {
        public enum Category { OptIn = 1 };
        public enum Status
        {
            Pending = 1,
            Ack = 2,
            InProgress = 3,
            Rejected = 4,
            Completed = 5,
            Error = 6,
            ReOpen = 7
        };

        public enum Priority
        {
            Low = 1,
            Medium = 2,
            High = 3
        };


        #region constants
        public const string SMTP_SERVER = "smtpServer";
        public const string SMTP_PORT = "smtpPort";
        public const string FROM_EMAIL = "fromEmail";
        public const string FROM_EMAIL_ENCRYPTED_PASSWORD = "fromEmailEncryptedPassword";
        public const string AZURE_STORAGE_CONNECTION = "AzureStorage";
        public const string AZURE_CONTAINER_NAME = "containerName";
        public const string OPTIN_EMAIL_GREETINGS = "Dear  Team, <br/> New Optin Request is created in system. Please find the details: <br/><br/>";
        public const string OPTIN_EMAIL_SUBJECT = "New Optin Request";
        #endregion
    }
}