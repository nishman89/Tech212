using OpenQA.Selenium.Chrome;
using SL_TestAutomationFramework.lib.pages;
using System;
using TechTalk.SpecFlow;
using SL_TestAutomationFramework.Utils;
using TechTalk.SpecFlow.Assist;

namespace SL_TestAutomationFramework.tests
{
    [Binding]
    [Scope(Feature = "SL_Signin")]
    public class SL_SigninStepDefinitions : SharedSteps
    {
        

        [Then(@"I should land on the inventory page")]
        public void ThenIShouldLandOnTheInventoryPage()
        {
            Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.InventoryPageUrl)); ;
        }

        [Given(@"I have entered a invalid ""([^""]*)""")]
        public void GivenIHaveEnteredAInvalid(string password)
        {
            SL_Website.SL_HomePage.EnterPassword(password);
        }

        [Then(@"I should see an error message that contains ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessageThatContains(string expected)
        {
            Assert.That(SL_Website.SL_HomePage.CheckErrorMessage(),Does.Contain(expected));
        }

        [Given(@"I have the following credentials")]
        public void GivenIHaveTheFollowingCredentials(Table table)
        {
            _credentials = table.CreateInstance<Credentials>();
        }

        [Given(@"I input these credentials")]
        public void GivenIInputTheseCredentials()
        {
            SL_Website.SL_HomePage.EnterSigninCredentials(_credentials);
        }
    }
}
