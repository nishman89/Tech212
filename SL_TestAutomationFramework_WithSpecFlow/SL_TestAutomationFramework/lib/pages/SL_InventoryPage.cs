using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL_TestAutomationFramework.lib.pages
{
    public class SL_InventoryPage
    {
        private IWebDriver _seleniumDriver;
        private IWebElement _addSauceLabsBackpackToCartButton => _seleniumDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
        private IWebElement _basketCount => _seleniumDriver.FindElement(By.ClassName("shopping_cart_link"));

        public SL_InventoryPage(IWebDriver seleniumDriver) => _seleniumDriver = seleniumDriver;

        public void AddSauceLabsBackPackToBasket() => _addSauceLabsBackpackToCartButton.Click();
        public int BasketCount()
        {
            var success = int.TryParse(_basketCount.Text, out int result);
            return success ? result : 0;
        }
    }
}
