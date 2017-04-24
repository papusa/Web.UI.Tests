using TechTalk.SpecFlow;
using Web.UI.Tests.Extensions;
using Web.UI.Tests.Selenium;
using Web.UI.Tests.WebPages;

namespace Web.UI.Tests.Steps
{
    [Binding]
    class CommonSteps : TechTalk.SpecFlow.Steps
    {
        private readonly ApplicationContext appContext;
        private readonly ScenarioContext scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            appContext = scenarioContext.GetApplicationContext();
        }

        [Given(@"(.*) opens homepage")]
        public void GivenUserOpensHomepage(string user)
        {
            IsOnTheHomePage(user);
        }

        private void IsOnTheHomePage(string user)
        {
            WebPage.InitializeSession(user, appContext);
            WebPage.SwitchToUser(user, appContext);
            scenarioContext.SetCurrentUser(user);
            WebPage.OpenHomePage();
            WebPage.Web.MaximizeWindow();
        }
    }
}
