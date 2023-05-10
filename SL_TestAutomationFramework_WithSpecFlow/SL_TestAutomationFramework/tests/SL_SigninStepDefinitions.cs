using OpenQA.Selenium.Chrome;
using SL_TestAutomationFramework.lib.pages;
using System;
using TechTalk.SpecFlow;
using SL_TestAutomationFramework.Utils;
using TechTalk.SpecFlow.Assist;

namespace SL_TestAutomationFramework.tests
{
    [Binding]
    public class SL_SigninStepDefinitions
    {
        public SL_Website<ChromeDriver> SL_Website { get; } = new();
        private Credentials? _credentials;

        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            SL_Website.SL_HomePage.VisitHomePage();
        }

        [Given(@"I have entered a valid user name")]
        public void GivenIHaveEnteredAValidUserName()
        {
            SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
        }

        [Given(@"I have entered a valid password")]
        public void GivenIHaveEnteredAValidPassword()
        {
            SL_Website.SL_HomePage.EnterPassword(AppConfigReader.Password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            SL_Website.SL_HomePage.ClickLoginButton();
        }

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


        [AfterScenario]
        public void DisposeWebDriver()
        {
            SL_Website.SeleniumDriver.Quit();
        }
    }
}
