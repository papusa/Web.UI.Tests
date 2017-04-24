using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Web.UI.Tests.Selenium.Drivers
{
    class ChromeDriverProvider
    {
        private static readonly string ChromeDriverPath = $"{Path.AltDirectorySeparatorChar}Resources{Path.AltDirectorySeparatorChar}Drivers{Path.AltDirectorySeparatorChar}Chrome";

        public static IWebDriver ChromeDriver
        {
            get
            {
                var chromeDriver = new ChromeDriver(ChromeService, ChromeOptions);
                return chromeDriver;
            }
        }

        private static ChromeDriverService ChromeService
        {
            get
            {
                var assemblyPath = AssemblyDirectory;
                var chromeDriverService = ChromeDriverService.CreateDefaultService($"{assemblyPath}{ChromeDriverPath}");
                chromeDriverService.HideCommandPromptWindow = true;
                return chromeDriverService;
            }
        }

        private static ChromeOptions ChromeOptions
        {
            get
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--disable-extensions");
                return chromeOptions;
            }
        }

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
