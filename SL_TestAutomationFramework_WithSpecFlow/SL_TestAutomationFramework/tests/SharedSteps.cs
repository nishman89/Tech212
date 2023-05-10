using OpenQA.Selenium.Chrome;
using SL_TestAutomationFramework.lib.pages;
using SL_TestAutomationFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SL_TestAutomationFramework.tests
{
    public class SharedSteps
    {
        public SL_Website<ChromeDriver> SL_Website { get; } = new();
        protected Credentials? _credentials;

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

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            GivenIAmOnTheHomePage();
            GivenIHaveEnteredAValidUserName();
            GivenIHaveEnteredAValidPassword();
            WhenIClickTheLoginButton();
        }



        [AfterScenario]
        public void DisposeWebDriver()
        {
            SL_Website.SeleniumDriver.Quit();
        }
    }
}
