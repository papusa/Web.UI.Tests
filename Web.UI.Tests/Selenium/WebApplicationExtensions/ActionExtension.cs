using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions
{
    static class ActionExtension
    {
        public static Actions MoveToElement(this WebApplication webApplication, By locator)
        {
            var element = webApplication.FindElement(locator);
            return webApplication.MoveToElement(element);
        }

        public static Actions MoveToElement(this WebApplication webApplication, IWebElement element)
        {
            return webApplication.Actions().MoveToElement(element);
        }

        public static Actions MoveToElement(this WebApplication webApplication, By locator, int offsetX, int offsetY)
        {
            var element = webApplication.FindElement(locator);
            return webApplication.Actions().MoveToElement(element, offsetX, offsetY);
        }

        public static Actions MoveToElement(this WebApplication webApplication, IWebElement element, int offsetX, int offsetY)
        {
            return webApplication.Actions().MoveToElement(element, offsetX, offsetY);
        }

        public static Actions Actions(this WebApplication webApplication)
        {
            return new Actions(webApplication.WebDriver);
        }
    }
}
