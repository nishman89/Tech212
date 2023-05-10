using System;
using TechTalk.SpecFlow;

namespace SL_TestAutomationFramework.tests
{
    [Binding]
    [Scope(Feature = "SL_Buying")]
    public class SL_BuyingStepDefinitions : SharedSteps
    {

        [When(@"I click to add the sauce labs backpack to cart")]
        public void WhenIClickToAddTheSauceLabsBackpackToCart()
        {
            SL_Website.SL_InventoryPage.AddSauceLabsBackPackToBasket();
        }

        [Then(@"the basket counter should increase by (.*)")]
        public void ThenTheBasketCounterShouldIncreaseBy(int expected)
        {
            Assert.That(SL_Website.SL_InventoryPage.BasketCount(), Is.EqualTo(expected));
        }
    }
}
