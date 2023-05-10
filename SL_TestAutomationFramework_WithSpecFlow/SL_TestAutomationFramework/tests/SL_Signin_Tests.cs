using OpenQA.Selenium.Chrome;
using SL_TestAutomationFramework.lib.pages;

namespace SL_TestAutomationFramework.tests
{
    public class SL_Signin_Tests
    {
        private SL_Website<ChromeDriver> SL_Website = new(isHeadless:true);

        [Test]
        public void GivenIAmOnTheHomePage_WhenIEnterAValidEmailAndValidPassword_ThenIShouldLandOnTheInventoryPage()
        {
            SL_Website.SeleniumDriver.Manage().Window.Maximize();
            SL_Website.SL_HomePage.VisitHomePage();
            SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
            SL_Website.SL_HomePage.EnterPassword(AppConfigReader.Password);
            SL_Website.SL_HomePage.ClickLoginButton();
            Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.InventoryPageUrl));
        }

        [Test]
        public void GivenIAmOnTheHomePage_WhenIEnterAValidEmailAndInvalidPassword_ThenIShouldSeeAnErrorMessage_WhichContainsEpicSadFace()
        {
            SL_Website.SeleniumDriver.Manage().Window.Maximize();
            SL_Website.SL_HomePage.VisitHomePage();
            SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
            SL_Website.SL_HomePage.EnterPassword("wrong");
            SL_Website.SL_HomePage.ClickLoginButton();
            Assert.That(SL_Website.SL_HomePage.CheckErrorMessage(), Does.Contain("Epic sadface"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            SL_Website.SeleniumDriver.Quit();
            //SL_Website.SeleniumDriver.Dispose();
        }
    }
}
