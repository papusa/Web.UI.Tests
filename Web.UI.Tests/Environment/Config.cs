using System;
using System.Configuration;

namespace Web.UI.Tests.Environment {
    class Config {
        private static string currentTestAccountName;
        public static string CurrentTestAccountName {
            get {
                if (currentTestAccountName == null) {
                    throw new Exception("Current BO account user not set, check App.config environment user equals to user set in feature file.");
                }
                return currentTestAccountName;
            }

            set {
                currentTestAccountName = value;
            }
        }

        public static string Browser => ConfigurationManager.AppSettings["browser"];

        public static int TimeOut => Convert.ToInt32(ConfigurationManager.AppSettings["timeOut"]);

        public static string BrowserWindowSize => ConfigurationManager.AppSettings["browserWindowSize"];

        public static string TestEnvironment => ConfigurationManager.AppSettings["testEnvironment"];

        #region === Test Accounts ===

        private static readonly object ConfigPadLock = new object();
        private static TestAccountsConfig testAccounts = null;
        private static TestAccountsConfig TestAccounts {
            get {
                if (testAccounts == null) {
                    lock (ConfigPadLock) {
                        testAccounts = ConfigurationManager.GetSection("testAccounts") as TestAccountsConfig;
                    }
                }

                return testAccounts;
            }
        }

        private static EnvironmentElement testEnvironment = null;
        public static EnvironmentElement GetTestEnvironment() {
            if (testEnvironment == null) {
                foreach (EnvironmentElement item in TestAccounts.Elements) {
                    if (item.Name == TestEnvironment) {
                        testEnvironment = item;
                        break;
                    }
                }

                if (testEnvironment == null)
                    throw new ArgumentNullException($"Test environment with name '{TestEnvironment}' is not defined!");
            }

            return testEnvironment;
        }

        public static AccountDetailsElement GetBoAccountDetails() {
            return GetBoAccountDetails(CurrentTestAccountName);
        }

        public static AccountDetailsElement GetBoAccountDetails(string key) {
            foreach (AccountDetailsElement item in GetTestEnvironment().Elements) {
                if (item.Name == key) return item;
            }

            return null;
        }

        public static void RetriveCurrenTestAccountName(string username, string password) {
            foreach (AccountDetailsElement item in GetTestEnvironment().Elements) {
                if (item.Username == username && item.Password == password) {
                    CurrentTestAccountName = item.Name;
                    break;
                }
            }
        }

        public static bool IsLocalTestEnvironment() {
            return Config.TestEnvironment == "Local";
        }

        public static string HomePage => GetTestEnvironment().Url;
        #endregion
    }
}
