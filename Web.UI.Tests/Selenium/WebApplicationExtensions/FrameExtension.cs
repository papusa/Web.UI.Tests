using OpenQA.Selenium;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class FrameExtension {
        public static void SwitchToIFrame(this WebApplication webApplication, By locator) {
            var iframe = webApplication.FindElement(locator);
            webApplication.WebDriver.SwitchTo().Frame(iframe);
        }
    }
}
