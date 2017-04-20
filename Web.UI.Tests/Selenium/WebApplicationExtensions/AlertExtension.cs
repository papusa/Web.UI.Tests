using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class AlertExtension {
        public static void AcceptAlertIfPresent(this WebApplication webApplication, int timeOutInMs = 1000) {
            if (webApplication.IsAlertPresent(timeOutInMs)) {
                webApplication.AcceptAlert();
            }
        }

        public static void AcceptAlert(this WebApplication webApplication) {
            webApplication.SwitchToAlert().Accept();
        }

        public static string GetAlertText(this WebApplication webApplication) {
            return webApplication.SwitchToAlert().Text;
        }

        public static bool IsAlertPresent(this WebApplication webApplication, int timeOutInMs) {
            Thread.Sleep(timeOutInMs);
            var alert = ExpectedConditions.AlertIsPresent().Invoke(webApplication.WebDriver);
            return alert != null;
        }

        private static IAlert SwitchToAlert(this WebApplication webApplication) {
            webApplication.WaitUntil(ExpectedConditions.AlertIsPresent());
            return webApplication.WebDriver.SwitchTo().Alert();
        }
    }
}
