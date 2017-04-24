using TechTalk.SpecFlow;

using Web.UI.Tests.Keys;
using Web.UI.Tests.Selenium;

namespace Web.UI.Tests.Extensions
{
    static class ScenarioContextExtension
    {
        public static string GetTimeStamp(this ScenarioContext scenarioContext)
        {
            return scenarioContext.Get<string>(ScenarioContextKeys.TimeStampKey);
        }

        public static ApplicationContext GetApplicationContext(this ScenarioContext scenarioContext)
        {
            return scenarioContext.Get<ApplicationContext>(ScenarioContextKeys.AppContextKey);
        }

        public static string GetCurrentUser(this ScenarioContext scenarioContext)
        {
            return scenarioContext.Get<string>(ScenarioContextKeys.CurrentUser);
        }

        public static void SetCurrentUser(this ScenarioContext scenarioContext, string userName)
        {
            scenarioContext.Set(userName, ScenarioContextKeys.CurrentUser);
        }
    }
}