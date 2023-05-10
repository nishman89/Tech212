
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
            // Maximise browser
            SL_Website.SeleniumDriver.Manage().Window.Maximize();
            // Navigate to home page
            SL_Website.SL_HomePage.VisitHomePage();
            // Enter valid username
            SL_Website.SL_HomePage.EnterUserName(AppConfigReader.UserName);
            // Enter valid password
            SL_Website.SL_HomePage.EnterPassword(AppConfigReader.Password);
            // Click login button
            SL_Website.SL_HomePage.ClickLoginButton();
            // Check landing page is correct
            Assert.That(SL_Website.SeleniumDriver.Url, Is.EqualTo(AppConfigReader.InventoryPageUrl));
        }

        [Test]
        public void GivenIAmOnTheHomePage_WhenIEnterAValidEmailAndInvalidPassword_ThenIShouldSeeAnErrorMessage_WhichContainsEpicSadFace()
        {

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            SL_Website.SeleniumDriver.Quit();
        }

    }
}
