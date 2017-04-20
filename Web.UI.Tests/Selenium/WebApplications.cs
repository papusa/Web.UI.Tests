using System;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Web.UI.Tests.Environment;
using Web.UI.Tests.Selenium.WebApplicationExtensions;

namespace Web.UI.Tests.Selenium {
    class WebApplications {
        private static readonly ThreadLocal<WebApplication> WebHolder = new ThreadLocal<WebApplication>();

        public static WebApplication Web => NewOrExistingWebApplication(WebHolder);

        public static WebApplication NewWebApplication() {
            return new WebApplication(NewWebDriver());
        }

        public static IWebDriver NewWebDriver() {
            var browser = Config.Browser;

            switch (browser.ToLower()) {
                case "chrome":
                    return WebDriverFactory.ChromeDriver;

                default: throw new ArgumentException($"No such driver \"{browser}\" available!");
            }
        }

        public static void TakeScreenshot(ApplicationContext appContext) {
            try {
                if (ScenarioContext.Current.TestError != null) {
                    Web.TakeScreenshot();
                }
            }
            catch {
                //ignore
            }
        }

        public static void TearDown(ApplicationContext appContext) {
            try {
            }
            finally {
                foreach (var driver in appContext.UserDrivers.Values) {
                    driver.Quit();
                }

                WebHolder.Value = null;
            }
        }

        private static WebApplication NewOrExistingWebApplication(ThreadLocal<WebApplication> holder) {
            return holder.Value ?? (holder.Value = NewWebApplication());
        }
    }
}
