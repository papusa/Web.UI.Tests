using System;

namespace Web.UI.Tests.Selenium.Attributes
{
    class ExpectWebElement : Attribute
    {
        public ExpectWebElement(int order)
        {
            Order = order;
        }

        public int Order { get; internal set; }
    }
}
