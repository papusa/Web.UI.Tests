using OpenQA.Selenium;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class HtmlAttributeExtension {
        public static string GetStyleAttribute(this WebApplication webApplication, By locator) {
            return webApplication.GetAttribute(locator, HtmlAttributes.Style);
        }

        public static string GetSrcAttribute(this WebApplication webApplication, By locator) {
            return webApplication.GetAttribute(locator, HtmlAttributes.Src);
        }

        public static string GetValueAttribute(this WebApplication webApplication, By locator) {
            return webApplication.GetAttribute(locator, HtmlAttributes.Value);
        }

        public static string GetValueAttribute(this WebApplication webApplication, IWebElement element) {
            return element.GetAttribute(HtmlAttributes.Value);
        }

        public static string GetClassAttribute(this WebApplication webApplication, By locator) {
            return webApplication.GetAttribute(locator, HtmlAttributes.Class);
        }

        public static string GetClassAttribute(this WebApplication webApplication, IWebElement element) {
            return element.GetAttribute(HtmlAttributes.Class);
        }

        public static string GetTitleAttribute(this WebApplication webApplication, IWebElement element) {
            return element.GetAttribute(HtmlAttributes.Title);
        }

        public static string GetTitleAttribute(this WebApplication webApplication, By locator) {
            return webApplication.GetAttribute(locator, HtmlAttributes.Title);
        }

        private static string GetAttribute(this WebApplication webApplication, By locator, string attributeName) {
            return webApplication.FindElement(locator).GetAttribute(attributeName);
        }

        private class HtmlAttributes {
            public const string Style = "style";
            public const string Value = "value";
            public const string Src = "src";
            public const string Class = "Class";
            public const string Title = "title";
        }
    }
}
