using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.edgewords.ecommerceproject.POMClass;
using uk.co.edgewords.ecommerceprojectfinal.Support;

namespace uk.co.edgewords.ecommerceprojectfinal.StepDefinitions
{
    [Binding]
    public class OrderNumberSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private User _userLoggedIn;
        private ISpecFlowOutputHelper _outputHelper;

        public OrderNumberSteps(ScenarioContext scenarioContext, User userLoggedIn)
        {
            _scenarioContext = scenarioContext;
            _userLoggedIn = userLoggedIn;
            this._driver = (IWebDriver)_scenarioContext["myDriver"];
            this._outputHelper = (ISpecFlowOutputHelper)_scenarioContext["output"];
        }

        [When(@"I go through the checkout process with these credentials")]
        public void WhenIGoThroughTheCheckoutProcessWithTheseCredentials(Table table)
        {
            //Go to checkout
            CartPagePOM cart = new CartPagePOM(_driver, _outputHelper);
            cart.GoToCheckout();
            Console.WriteLine("On Checkout page.");

            _userLoggedIn = table.CreateInstance<User>();

            //enter all required details into the checkout boxes
            CheckoutPagePOM checkout = new CheckoutPagePOM(_driver, _outputHelper);
            checkout.EnterFirstName(_userLoggedIn.userFName);
            checkout.EnterLastName(_userLoggedIn.userLName);
            checkout.SelectCountry(_userLoggedIn.userCountry);
            checkout.EnterStreetAddress(_userLoggedIn.userStreet);
            checkout.EnterCity(_userLoggedIn.userCity);
            checkout.EnterPostcode(_userLoggedIn.userPostcode);
            checkout.EnterPhoneNo(_userLoggedIn.userPhoneNo);
            checkout.SelectPaymentMethod();


            checkout.PlaceOrder();
        }

        [Then(@"my order will appear in my order history")]
        public void ThenMyOrderWillAppearInMyOrderHistory()
        {
            //store order number after order placed
            CheckoutPagePOM checkout = new CheckoutPagePOM(_driver, _outputHelper);
            string orderNumFromCheckout = checkout.GetOrderNumberPlaced();
            _outputHelper.WriteLine("Order number is " + orderNumFromCheckout);
            MyHelpers help = new MyHelpers(_driver, _outputHelper);
            help.ScreenshotPage(_driver, "OrderNumberAfterCheckout.png");

            //Go to My account page
            TopNavPOM topNav = new TopNavPOM(_driver);
            topNav.GoToMyAccountPage();

            //Go to My account => Orders page
            MyAccountPagePOM myAccount = new MyAccountPagePOM(_driver, _outputHelper);
            myAccount.GoToOrders();
            _outputHelper.WriteLine("On My account => orders page.");

            //read latest order number on orders page
            string orderNumInHistory = myAccount.GetOrderNumFromHistory();
            _outputHelper.WriteLine("Order number 2 is: " + orderNumInHistory);
            help.ScreenshotPage(_driver, "OrderNumberInOrderHistory.png");

            try
            {
                Assert.That(orderNumInHistory, Is.EqualTo(orderNumFromCheckout), "Order number not found");
            }
            catch (Exception ex)
            {
                _outputHelper.WriteLine(ex.ToString());
            }

            _outputHelper.WriteLine("Test finished.");
        }

    }
}
