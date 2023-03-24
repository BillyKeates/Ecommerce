using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.edgewords.ecommerceprojectfinal.Support;

namespace uk.co.edgewords.ecommerceproject.POMClass
{
    internal class CheckoutPagePOM
    {
        private IWebDriver _driver;
        private ISpecFlowOutputHelper _outputHelper;

        public CheckoutPagePOM(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            this._driver = driver;
            this._outputHelper = outputHelper;
        }

        private IWebElement _userFirstName => _driver.FindElement(By.Id("billing_first_name")); //first name input box
        private IWebElement _userLastName => _driver.FindElement(By.Id("billing_last_name")); //last name input box

        private IWebElement _selectCountry => _driver.FindElement(By.Id("select2-billing_country-container")); //country drop down box
        private IWebElement _countryInput => _driver.FindElement(By.CssSelector("input.select2-search__field")); //country input box

        private IWebElement _specifiedCountry => _driver.FindElement(By.CssSelector("li.select2-results__option")); //first result from country search
        private IWebElement _streetAddress => _driver.FindElement(By.Id("billing_address_1")); //street input box
        private IWebElement _userCity => _driver.FindElement(By.Id("billing_city")); //city input box
        private IWebElement _userPostcode => _driver.FindElement(By.Id("billing_postcode")); //postcode input box
        private IWebElement _userPhoneNo => _driver.FindElement(By.Id("billing_phone")); //phone number input box
        private IWebElement _paymentMethod => _driver.FindElement(By.CssSelector(".payment_method_cheque")); //cheque payment method radio button
        private IWebElement _placeOrderBtn => _driver.FindElement(By.Id("place_order")); //place order button
        private string _orderNum => _driver.FindElement(By.CssSelector(".woocommerce-order-overview__order > strong")).Text; //string containing order number


        public void EnterFirstName(string firstName)
        {
            _userFirstName.Clear();
            _userFirstName.SendKeys(firstName);

        }

        public void EnterLastName(string lastName)
        {
            _userLastName.Clear();
            _userLastName.SendKeys(lastName);
        }

        public void SelectCountry(string country)
        {
            _selectCountry.Click();
            _countryInput.SendKeys(country);
            _specifiedCountry.Click();

        }


        public void EnterStreetAddress(string streetAddress)
        {
            _streetAddress.Clear();
            _streetAddress.SendKeys(streetAddress);
        }

        public void EnterCity(string city)
        {
            _userCity.Clear();
            _userCity.SendKeys(city);
        }

        public void EnterPostcode(string postcode)
        {
            _userPostcode.Clear();
            _userPostcode.SendKeys(postcode);
        }
        public void EnterPhoneNo(string phoneNo)
        {
            _userPhoneNo.Clear();
            _userPhoneNo.SendKeys(phoneNo);
        }

        public void SelectPaymentMethod()
        {
            //Thread.Sleep included because paymnent method box needs to load in before it can be clicked
            Thread.Sleep(1000);
            _paymentMethod.Click();
        }

        public void PlaceOrder()
        {
            _placeOrderBtn.Click();
        }

        public string GetOrderNumberPlaced()
        {
            //Set explicit wait until order number is displayed
            MyHelpers help = new MyHelpers(_driver, _outputHelper);
            help.WaitForElement(By.CssSelector(".woocommerce-order-overview__order"), 3);
            return _orderNum;
        }


    }
}
