using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Web.UI.Tests.Selenium.Attributes;

namespace Web.UI.Tests.Selenium.WebApplicationExtensions {
    static class ExpectWebElementsExtension {
        public static void ExpectWebElements<T>(this WebApplication webApplication, int order) {
            var fields = typeof(T).GetFields();

            fields.ToList().ForEach(fieldInfo => {
                var attribute = (ExpectWebElement)Attribute.GetCustomAttribute(fieldInfo, typeof(ExpectWebElement));
                if (attribute != null && attribute.Order == order) {
                    var fieldValue = (By)fieldInfo.GetValue(null);
                    try {
                        webApplication.WaitUntil(ExpectedConditions.ElementIsVisible(fieldValue));
                    }
                    catch (Exception e) {
                        throw new Exception($"Error, expected element '{fieldInfo.Name} : {fieldValue}'\n{e.StackTrace}");
                    }
                }
            });
        }
    }
}
