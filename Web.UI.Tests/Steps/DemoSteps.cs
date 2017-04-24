using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechTalk.SpecFlow;

using Web.UI.Tests.Environment;
using Web.UI.Tests.Extensions;
using Web.UI.Tests.Models;
using Web.UI.Tests.Selenium;
using Web.UI.Tests.WebPages;

namespace Web.UI.Tests.Steps
{
    [Binding]
    public sealed class DemoSteps
    {
        private readonly ApplicationContext appContext;

        private readonly ScenarioContext scenarioContext;

        public DemoSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            appContext = scenarioContext.GetApplicationContext();
        }

        [When(@"user attempts to search for username")]
        public void WhenUserAttemptsToSearchForUsername()
        {
            var curUserName = scenarioContext.GetCurrentUser();
            var srchTxt = Config.GetAccountDetails(curUserName).Username;
            HomePage.OpenSearchBar();
            var fieldsInput = new List<InputField>{ new InputField{ FieldName = "SearchInput",FieldValue = srchTxt }};
            HomePage.FillInFields(fieldsInput);
            HomePage.ClickSearch();
        }

        [Then(@"profit")]
        public void ThenProfit()
        {
            //TODO implement rest steps
        }

    }
}