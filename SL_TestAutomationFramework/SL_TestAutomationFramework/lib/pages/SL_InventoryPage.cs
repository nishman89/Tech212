
using OpenQA.Selenium;

namespace SL_TestAutomationFramework.lib.pages
{
    public class SL_InventoryPage
    {
        private IWebDriver _seleniumDriver;

        public SL_InventoryPage(IWebDriver seleniumDriver)
        {
            _seleniumDriver = seleniumDriver;
        }
    }
}
