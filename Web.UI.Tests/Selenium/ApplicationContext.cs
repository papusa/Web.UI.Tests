using System.Collections.Generic;

using OpenQA.Selenium;

namespace Web.UI.Tests.Selenium
{
    class ApplicationContext
    {
        public ApplicationContext()
        {
            UserDrivers = new Dictionary<string, IWebDriver>();
        }

        public Dictionary<string, IWebDriver> UserDrivers { get; set; }
    }
}