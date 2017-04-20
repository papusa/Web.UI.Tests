using TechTalk.SpecFlow;
using Web.UI.Tests.Extensions;
using Web.UI.Tests.Selenium;

namespace Web.UI.Tests.Steps {
    [Binding]
    class CommonSteps : TechTalk.SpecFlow.Steps {
        private readonly ApplicationContext appContext;
        private readonly ScenarioContext scenarioContext;

        public CommonSteps(ScenarioContext scenarioContext) {
            this.scenarioContext = scenarioContext;
            appContext = scenarioContext.GetApplicationContext();
        }
    }
}
