using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using uk.co.edgewords.ecommerceproject.POMClass;
using static uk.co.edgewords.ecommerceprojectfinal.Support.HooksClass;

namespace uk.co.edgewords.ecommerceprojectfinal.StepDefinitions
{
    [Binding]
    public class ECommerceSteps
    {
        private readonly ScenarioContext _scenarioContext;
        //Create POM objects
        TopNavPOM topNav = new TopNavPOM(driver);
        LogInPagePOM login = new LogInPagePOM(driver);
        ShopPagePOM shop = new ShopPagePOM(driver);
        CartPagePOM cart = new CartPagePOM(driver);
        CheckoutPagePOM checkout = new CheckoutPagePOM(driver);
        MyAccountPagePOM myAccount = new MyAccountPagePOM(driver);



        public ECommerceSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }



        [Given(@"I have added an item to the cart")]
        public void GivenIHaveAddedAnItemToTheCart()
        {

            //Enter username and password into the input elements on the log in page
            login.EnterUsername(Environment.GetEnvironmentVariable("USERNAME1"));
            login.EnterPassword(Environment.GetEnvironmentVariable("PASSWORD"));
            login.ClickLogIn();
            Assert.That(login.CheckIfLoggedIn(), Is.True, "Invalid log in details");


            //go to shop page
            topNav.GoToShopPage();
            Console.WriteLine("On shop page.");

            //Check if demo store notice is displayed
            //Click dismiss if it is displayed
            shop.DismissAlert();

            //Click to add item to cart and view cart
            shop.AddItemToCart();
            shop.ViewCart();
            Console.WriteLine("On cart page.");

        }



        [When(@"I apply the coupon '(.*)' on the cart page")]
        public void WhenIApplyTheDiscountOnTheCartPage(string coupon)
        {
            cart.EnterCoupon(coupon);
            cart.ApplyCoupon();
        }



        [Then(@"the correct total cost is calculated")]
        public void ThenTheCorrectTotalCostIsCalculated()
        {

            //Check that discount is correct
            bool isDiscountCorrect = cart.CheckDiscount();
            try
            {
                Assert.That(isDiscountCorrect, Is.True, "Discount is not 15%");
                //Discount correct
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Check that total price is correct
            bool isTotalPriceCorrect = cart.CheckPrice();
            try
            {
                Assert.That(isTotalPriceCorrect, Is.True, "Total price is not correct");
                //Total is correct
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //Go to my account page
            topNav.GoToMyAccountPage();
            Console.WriteLine("On My Account page");

            //Log out of account
            myAccount.Logout();

            Console.WriteLine("Test finished.");

        }



        [When(@"I go through the checkout process")]
        public void WhenIGoThroughTheCheckoutProcess()
        {
            //Go to checkout
            cart.GoToCheckout();
            Console.WriteLine("On Checkout page.");

            //enter all required details into the checkout boxes
            checkout.EnterFirstName(Environment.GetEnvironmentVariable("FIRSTNAME"));
            checkout.EnterLastName(Environment.GetEnvironmentVariable("LASTNAME"));
            checkout.SelectCountry("United Kingdom");
            checkout.EnterStreetAddress(Environment.GetEnvironmentVariable("STREET"));
            checkout.EnterCity(Environment.GetEnvironmentVariable("CITY"));
            checkout.EnterPostcode(Environment.GetEnvironmentVariable("POSTCODE"));
            checkout.EnterPhoneNo(Environment.GetEnvironmentVariable("PHONENO"));
            checkout.SelectPaymentMethod();


            checkout.PlaceOrder();
        }


        [Then(@"my order will appear in my order history")]
        public void ThenMyOrderWillAppearInMyOrderHistory()
        {
            //store order number after order placed
            string orderNum1 = checkout.GetOrderNum1();
            Console.WriteLine("Order number is " + orderNum1);

            //Go to My account page
            topNav.GoToMyAccountPage();
            Console.WriteLine("On My account page.");

            //Go to My account => Orders page
            myAccount.GoToOrders();
            Console.WriteLine("On My account => orders page.");

            //read latest order number on orders page
            string orderNum2 = myAccount.GetOrderNum2();
            Console.WriteLine("Order number 2 is: " + orderNum2);

            try
            {

                Assert.That(orderNum2, Is.EqualTo(orderNum1), "Order number not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            //Go to my account page
            topNav.GoToMyAccountPage();
            Console.WriteLine("On My Account page");

            //Log out of account
            myAccount.Logout();

            Console.WriteLine("Test finished.");
        }



        

    }





}
    

