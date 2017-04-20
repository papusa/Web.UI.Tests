using OpenQA.Selenium;
using Web.UI.Tests.Selenium.Drivers;

namespace Web.UI.Tests.Selenium {
    class WebDriverFactory {
        public static IWebDriver ChromeDriver => ChromeDriverProvider.ChromeDriver;
    }
}
