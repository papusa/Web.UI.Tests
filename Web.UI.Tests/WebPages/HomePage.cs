using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;

using Web.UI.Tests.Models;
using Web.UI.Tests.Selenium;

namespace Web.UI.Tests.WebPages
{
    class HomePage : WebPage
    {
        public static List<Field> Fields => new List<Field>
                                                {
                                                    new Field
                                                        {
                                                            FieldType = ElementType.Input,
                                                            Name = "SearchInput",
                                                            Locator = By.ClassName("search-field")
                                                        }
                                                };

        public static void FillInFields(List<InputField> fieldsToEnter)
        {
            CommonFillInFields(fieldsToEnter, Fields);
        }

        public static void OpenSearchBar()
        {
            Web.Click(By.CssSelector("button.fa-search"));
            Thread.Sleep(1000);
        }

        public static void ClickSearch()
        {
            Web.Click(By.CssSelector("input.search-submit"));
            Thread.Sleep(1000);
        }
    }

}