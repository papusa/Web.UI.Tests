using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Web.UI.Tests.Environment;
using Web.UI.Tests.Selenium.WebApplicationExtensions;

namespace Web.UI.Tests.Selenium
{
    class WebApplication
    {
        private static List<IWebDriver> allDrivers;

        public WebApplication(IWebDriver webDriver)
        {
            WebDriver = webDriver;
            allDrivers = new List<IWebDriver>();
        }

        public IWebDriver WebDriver { get; private set; }
        public string CurrentUser { get; set; }
        public Dictionary<string, string> SessionWindows { get; set; } = new Dictionary<string, string>();

        public void AddSession(string userRole, ApplicationContext appContext)
        {
            IWebDriver newDriver;
            if (allDrivers.Count != 0)
            {
                newDriver = WebApplications.NewWebDriver();
                WebDriver = newDriver;
            }
            else
            {
                newDriver = WebDriver;
            }

            allDrivers.Add(newDriver);

            if (appContext.UserDrivers.ContainsKey(userRole))
            {
                return;
            }

            appContext.UserDrivers.Add(userRole, newDriver);
        }

        public void SwitchToUser(string userRole, ApplicationContext appContext)
        {
            CurrentUser = userRole;
            var newWebDriver = appContext.UserDrivers[userRole];
            WebDriver = newWebDriver;
        }

        public void SwitchToDefault()
        {
            WebDriver.SwitchTo().DefaultContent();
        }

        public void Open(string path)
        {
            WebDriver.Navigate().GoToUrl(path);
        }

        public void MaximizeWindow()
        {
            var size = Config.BrowserWindowSize;
            if (size == "Maximize" || !size.Contains('x'))
            {
                WebDriver.Manage().Window.Maximize();
            }
            else
            {
                try
                {
                    var sizeArr = size.Split('x');
                    WebDriver.Manage().Window.Size = new System.Drawing.Size(int.Parse(sizeArr[0]), int.Parse(sizeArr[1]));
                }
                catch
                {
                    WebDriver.Manage().Window.Maximize();
                }
            }
        }

        public IWebElement MarkElement(IWebElement element, string color = "red")
        {
            var js = WebDriver as IJavaScriptExecutor;
            js?.ExecuteScript($"arguments[0].setAttribute('style', 'border: 2px solid {color};')", element);
            return element;
        }

        public IWebElement MarkElement(By locator, string color = "red")
        {
            var element = FindElement(locator);
            var js = WebDriver as IJavaScriptExecutor;
            js?.ExecuteScript($"arguments[0].setAttribute('style', 'border: 2px solid {color};')", element);
            return element;
        }

        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public void TickUnselectedCheckbox(By locator)
        {
            if (!IsElementTicked(locator))
            {
                ClickJs(locator);
            }
        }

        public void TickUnselectedCheckbox(IWebElement locator)
        {
            bool selected = locator.Selected;
            if (!selected)
            {
                ClickJs(locator);
            }
        }

        public void UncheckCheckbox(IWebElement locator)
        {
            bool selected = locator.Selected;
            if (selected)
            {
                ClickJs(locator);
            }
        }

        public void UncheckCheckbox(By locator)
        {
            if (IsElementTicked(locator))
            {
                ClickJs(locator);
            }
        }

        public void Click(By locator)
        {
            WebApplications.Web.MoveToElement(locator).Perform();
            WaitUntil(ExpectedConditions.ElementToBeClickable(locator));
            FindElement(locator).Click();
        }

        public void Click(IWebElement element)
        {
            WaitUntil(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        public void VerticalScroll(int verticalPosition)
        {
            ExecuteJavaScript($"scroll(0, {verticalPosition});");
        }

        public void ExecuteJavaSriptOnElement(string script, IWebElement element)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            javaScriptExecutor.ExecuteScript(script, element);
        }

        public void ExecuteJavaScript(string script)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            javaScriptExecutor.ExecuteScript(script);
        }
        public string ExecuteJavaScriptReturn(string script)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            return javaScriptExecutor.ExecuteScript(script).ToString();
        }

        public string GetDocumentReadyState()
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            return javaScriptExecutor.ExecuteScript("return document.readyState").ToString();
        }

        public void WaitWhilePageLoading()
        {
            do
            {
                Thread.Sleep(100);
            }
            while (!GetDocumentReadyState().Equals("complete"));
        }

        public void ClickJs(By locator)
        {
            var target = FindElement(locator);
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            javaScriptExecutor.ExecuteScript(
                "var evt = document.createEvent('MouseEvents');"
                + "evt.initMouseEvent('click',true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);"
                + "arguments[0].dispatchEvent(evt);",
                target);
            Thread.Sleep(500);
        }

        public void ClickJs(IWebElement element)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;
            javaScriptExecutor.ExecuteScript(
                "var evt = document.createEvent('MouseEvents');"
                + "evt.initMouseEvent('click',true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);"
                + "arguments[0].dispatchEvent(evt);",
                element);
            Thread.Sleep(500);
        }

        public void Submit(By locator)
        {
            WaitUntil(ExpectedConditions.ElementToBeClickable(locator));
            FindElement(locator).Submit();
        }

        public void TypeNewText(By locator, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Clear(locator);
                SendKeys(locator, text);
            }
        }

        public void TypeNewText(IWebElement element, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                element.Clear();
                element.SendKeys(text);
            }
        }

        public void Clear(IWebElement element)
        {
            element.Clear();

        }

        public void SendKeys(By locator, string text)
        {
            WaitUntil(ExpectedConditions.ElementIsVisible(locator));
            FindElement(locator).SendKeys(text);
        }

        public void Clear(By locator)
        {
            WaitUntil(ExpectedConditions.ElementIsVisible(locator));
            var element = FindElement(locator);
            do
            {
                element.SendKeys(OpenQA.Selenium.Keys.Backspace);
            }
            while (element.GetAttribute("value").Length != 0);
        }

        public bool IsElementDisplayed(By locator)
        {
            if (IsElementPresent(locator))
            {
                try
                {
                    return WebDriver.FindElement(locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }

            return false;
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                WebDriver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementTicked(By locator)
        {
            return FindElement(locator).Selected;
        }

        public string GetText(By locator)
        {
            return FindElement(locator).Text;
        }

        public string GetSelected(By locator)
        {
            return FindElement(locator).Selected ? "true" : "false";
        }

        public IWebElement FindElement(By locator)
        {
            WaitUntil(ExpectedConditions.ElementExists(locator));
            return WebDriver.FindElement(locator);
        }

        public List<IWebElement> FindElements(By locator)
        {
            WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return WebDriver.FindElements(locator).ToList();
        }

        public void WaitElementIsNotVisible(By locator, int timeout = 15)
        {
            WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(locator), timeout);
        }

        public List<IWebElement> FindElementsWithoutWait(By locator)
        {
            return WebDriver.FindElements(locator).ToList();
        }

        public IWebElement FindDisplayedElementWithoutWait(By locator)
        {
            return FindElementsWithoutWait(locator).Find(x => x.Displayed);
        }

        public void WaitUntil(Func<IWebDriver, object> until)
        {
            WaitUntil(until, Config.TimeOut);
        }

        public void WaitUntil(Func<IWebDriver, bool> until)
        {
            WaitUntil(until, Config.TimeOut);
        }

        public void WaitUntil(Func<IWebDriver, object> until, int seconds)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(until);
        }

        public void WaitUntil(Func<IWebDriver, bool> until, int seconds)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            wait.Until(until);
        }
    }
}
