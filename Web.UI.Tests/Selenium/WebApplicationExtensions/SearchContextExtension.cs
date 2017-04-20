using System.Linq;
using OpenQA.Selenium;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class SearchContextExtension {
        public static IWebElement FindVisibleElement(this ISearchContext element, By by) {
            return element.FindElements(by).FirstOrDefault(webElement => webElement.Displayed);
        }
    }
}
