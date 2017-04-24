using System;
using TechTalk.SpecFlow;
using Web.UI.Tests.Extensions;
using Web.UI.Tests.Keys;
using Web.UI.Tests.Selenium;

namespace Web.UI.Tests.Steps
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ApplicationContext appContext = new ApplicationContext();
        private readonly ScenarioContext scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = int.MinValue)]
        public void BeforeScenario()
        {
            scenarioContext.Set(appContext, ScenarioContextKeys.AppContextKey);
            scenarioContext.Set(new DateTime().GetTimeStamp(), ScenarioContextKeys.TimeStampKey);
        }

        [AfterScenario(Order = int.MinValue)]
        public void TakeScreenshot()
        {
            WebApplications.TakeScreenshot(scenarioContext.GetApplicationContext());
        }

        [AfterScenario(Order = int.MaxValue)]
        public void After()
        {
            WebApplications.TearDown(scenarioContext.GetApplicationContext());
        }
    }
}
