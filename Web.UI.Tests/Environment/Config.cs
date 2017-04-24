using System;
using System.Configuration;

namespace Web.UI.Tests.Environment
{
    class Config
    {
        private static ConfigHandler AccountsConfigSection
        {
            get
            {
                var ch = (ConfigHandler)ConfigurationManager.GetSection("testAccountsSettings");
                if (ch == null)
                {
                    throw new Exception("Configuration section not found. Check App.config");
                }
                return ch;
            }
        }

        public static string Browser => ConfigurationManager.AppSettings["browser"];

        public static int TimeOut => Convert.ToInt32(ConfigurationManager.AppSettings["timeOut"]);

        public static string BrowserWindowSize => ConfigurationManager.AppSettings["browserWindowSize"];

        public static string HomePage => ConfigurationManager.AppSettings["URL"];

        #region === Test Accounts ===

        public static AccountDetails GetAccountDetails(string userName)
        {
            return AccountsConfigSection.TestAccounts[userName];
        }

        #endregion
    }
}