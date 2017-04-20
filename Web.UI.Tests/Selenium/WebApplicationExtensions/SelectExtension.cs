using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class SelectExtension {
        public static void SelectByText(this WebApplication webApplication, By locator, string text) {
            if (!string.IsNullOrEmpty(text)) {
                webApplication.SelectElement(locator).SelectByText(text);
            }
        }

        public static void SelectByText(this WebApplication webApplication, IWebElement element, string text) {
            if (!string.IsNullOrEmpty(text)) {
                webApplication.SelectElement(element).SelectByText(text);
            }
        }

        public static void SelectByValue(this WebApplication webApplication, By locator, string text) {
            webApplication.SelectElement(locator).SelectByValue(text);
        }

        public static void SelectByValue(this WebApplication webApplication, IWebElement locator, string text) {
            webApplication.SelectElement(locator).SelectByValue(text);
        }

        public static void SelectByIndex(this WebApplication webApplication, By locator, int index) {
            webApplication.SelectElement(locator).SelectByIndex(index);
        }

        public static string GetSelectedOptionText(this WebApplication webApplication, By locator) {
            return webApplication.GetSelectedOption(locator).Text;
        }

        public static string GetSelectedOptionText(this WebApplication webApplication, IWebElement select) {
            return webApplication.GetSelectedOption(select).Text;
        }

        private static IWebElement GetSelectedOption(this WebApplication webApplication, By locator) {
            return webApplication.SelectElement(locator).SelectedOption;
        }

        private static IWebElement GetSelectedOption(this WebApplication webApplication, IWebElement select) {
            return webApplication.SelectElement(select).SelectedOption;
        }

        private static SelectElement SelectElement(this WebApplication webApplication, By locator) {
            webApplication.WaitUntil(ExpectedConditions.ElementToBeClickable(locator));
            var select = webApplication.FindElement(locator);
            return new SelectElement(select);
        }

        private static SelectElement SelectElement(this WebApplication webApplication, IWebElement element) {
            return new SelectElement(element);
        }
    }
}
