using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.edgewords.ecommerceproject.POMClass;

namespace uk.co.edgewords.ecommerceprojectfinal.StepDefinitions
{
    [Binding]
    public class CheckCouponSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private ISpecFlowOutputHelper _outputHelper;
        
        public CheckCouponSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
            this._outputHelper = (ISpecFlowOutputHelper)_scenarioContext["output"];
        }



        [Given(@"I have added an item to the cart")]
        public void GivenIHaveAddedAnItemToTheCart()
        {

            //go to shop page
            TopNavPOM topNav = new TopNavPOM(_driver);
            topNav.GoToShopPage();
            _outputHelper.WriteLine("On shop page.");



            //Click to add item to cart and view cart
            ShopPagePOM shop = new ShopPagePOM(_driver,_outputHelper);
            shop.AddItemToCart();
            shop.ViewCart();
            _outputHelper.WriteLine("On cart page.");

        }



        [When(@"I apply the coupon '(.*)' on the cart page")]
        public void WhenIApplyTheDiscountOnTheCartPage(string coupon)
        {
            CartPagePOM cart = new CartPagePOM(_driver,_outputHelper);
            cart.EnterCoupon(coupon);
            cart.ApplyCoupon();
        }

        [Then(@"the total cost reflects the discount of (.*)%")]
        public void ThenTheTotalCostReflectsTheDiscountOf(int discount)
        {
            //Check that discount is correct
            CartPagePOM cart = new CartPagePOM(_driver, _outputHelper);
            var discountResult = cart.CheckDiscount(discount);


            try
            {
                Assert.That(discountResult.Item1, Is.EqualTo(discountResult.Item2), "Discount is incorrect");
                //Discount correct
            }
            catch (Exception ex)
            {
                _outputHelper.WriteLine(ex.ToString());
            }

            //Check that total price is correct
            var totalPriceResult = cart.CheckPrice(discount);
            try
            {
                Assert.That(totalPriceResult.Item1, Is.EqualTo(totalPriceResult.Item2), "Total price is not correct");
            }
            catch (Exception ex)
            {
                _outputHelper.WriteLine(ex.ToString());
            }

            _outputHelper.WriteLine("Test finished.");
        }
    }
}