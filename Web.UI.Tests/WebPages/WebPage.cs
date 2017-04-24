using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using Web.UI.Tests.Environment;
using Web.UI.Tests.Extensions;
using Web.UI.Tests.Models;
using Web.UI.Tests.Selenium;
using Web.UI.Tests.Selenium.WebApplicationExtensions;

namespace Web.UI.Tests.WebPages
{
    class WebPage
    {
        public static WebApplication Web => WebApplications.Web;

        public static void InitializeSession(string userRole, ApplicationContext appContext)
        {
            Web.AddSession(userRole, appContext);
        }

        public static void OpenHomePage()
        {
            Web.Open(Config.HomePage);
        }

        public static void Refresh()
        {
            Web.Refresh();
        }

        public static void SwitchToUser(string userRole, ApplicationContext appContext)
        {
            Web.SwitchToUser(userRole, appContext);
        }

        public static bool IsNewWindowOpen()
        {
            try
            {
                Web.GetNewWindow();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected static void RegisterNewSessionWindow(string windowName)
        {
            Web.SessionWindows.Add(GenerateCurrentUsersWindowName(windowName), Web.GetNewWindow());
        }

        protected static void UnregisterSessionWindow(string windowName)
        {
            Web.SessionWindows.Remove(GenerateCurrentUsersWindowName(windowName));
        }

        protected static string GetSessionWindowByName(string windowName)
        {
            return Web.SessionWindows[GenerateCurrentUsersWindowName(windowName)];
        }

        protected static void ClickAndWait(By locator)
        {
            Web.Click(locator);
            WaitWhileLoading();
        }

        protected static void ClickAndWait(IWebElement element)
        {
            Web.Click(element);
            WaitWhileLoading();
        }

        protected static void ClickJsAndWait(By locator)
        {
            Web.ClickJs(locator);
            WaitWhileLoading();
        }

        protected static void ClickJsAndWait(IWebElement element)
        {
            Web.ClickJs(element);
            WaitWhileLoading();
        }

        protected static void WaitWhileLoading()
        {
            var currentTimeInSeconds = 0;
            while (IsNotTimedOut(currentTimeInSeconds))
            {
                Thread.Sleep(1000);
                currentTimeInSeconds++;
            }
        }

        protected static bool IsNotTimedOut(int currentTimeInSeconds)
        {
            return currentTimeInSeconds <= Config.TimeOut;
        }

        protected static bool IsTimedOut(int currentTimeInSeconds)
        {
            return currentTimeInSeconds >= Config.TimeOut;
        }

        protected static bool IsNotTimedOutInMs(int currentMs)
        {
            return currentMs <= Config.TimeOut * 1000;
        }

        protected static bool IsTimedOutInMs(int currentMs)
        {
            return currentMs >= Config.TimeOut * 1000;
        }

        protected static void CommonFillInFields(List<InputField> fieldsFromTable, List<Field> fieldsOnPage)
        {
            fieldsFromTable.ForEach(x =>
            {
                var fieldToEnterName = x.FieldName;
                var fieldValue = x.FieldValue;
                if (fieldValue != "$Skip.Value")
                {
                    var fieldToEnter = fieldsOnPage.Find(f => f.Name == fieldToEnterName);
                    if (fieldToEnter != null)
                    {
                        ProcessField(fieldToEnter, fieldValue);
                    }
                }
            });
        }

        private static bool IsLoadingSpinVisible(By loadingSpin)
        {
            throw new NotImplementedException();
        }

        private static string GenerateCurrentUsersWindowName(string windowName)
        {
            return $"{windowName} {Web.CurrentUser}";
        }

        private static void ProcessField(Field fieldFromPage, string fieldValue)
        {
            //WaitWhileLoading();
            var locator = fieldFromPage.Locator;
            Web.MoveToElement(locator).Perform();

            var ch = !string.IsNullOrEmpty(fieldValue) ? fieldValue.First() : '\0';

            switch (fieldFromPage.FieldType)
            {
                case ElementType.Input:
                    ProcessInput(fieldValue, locator);
                    break;
                case ElementType.Checkbox:
                    ProcessCheckbox(fieldValue, locator);
                    break;
                case ElementType.Select:
                    Web.SelectByText(locator, fieldValue);
                    break;

                case ElementType.RadioButton:
                    ProcessRadioButton(fieldValue, locator);
                    break;
                // TODO: Implement code for processing other type fields if necessary
                default: throw new ArgumentException($"There is no definition for type: {0}", fieldFromPage.FieldType.ToString());
            }

            //WaitWhileLoading();
        }

        public static string GetFieldValue(Field field)
        {
            var locator = field.Locator;
            Web.MoveToElement(locator).Perform();

            switch (field.FieldType)
            {
                case ElementType.Input:
                    return Web.GetValueAttribute(locator);
                case ElementType.Select:
                    return Web.GetSelectedOptionText(locator);
                case ElementType.Label:
                    return Web.GetText(locator);
                case ElementType.Checkbox:
                    return Web.GetSelected(locator);

                // TODO: Implement code for processing other type fields if necessary
                default: throw new ArgumentException($"There is no definition for type: {0}", field.FieldType.ToString());
            }
        }

        public static void ProcessInput(string fieldValue, By locator)
        {
            if (fieldValue.EmptyValueToEnter())
            {
                Web.Clear(locator);
            }
            else
            {
                Web.TypeNewText(locator, fieldValue);
            }
        }

        public static void ProcessInput(string fieldValue, IWebElement locator)
        {
            if (fieldValue.EmptyValueToEnter())
            {
                Web.Clear(locator);
            }
            else
            {
                Web.TypeNewText(locator, fieldValue);
            }
        }

        private static void ProcessCheckbox(string fieldValue, By locator)
        {
            if (fieldValue.EmptyValueToEnter() || !fieldValue.ToBool())
            {
                Web.UncheckCheckbox(locator);
                return;
            }

            var tick = fieldValue.ToBool();
            if (tick)
            {
                Web.TickUnselectedCheckbox(locator);
            }
        }

        private static void ProcessRadioButton(string fieldValue, By locator)
        {
            throw new NotImplementedException();
        }
    }
}
