using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.ecommerceproject.POMClass
{
    internal class TopNavPOM
    {
        private IWebDriver _driver;

        public TopNavPOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        private IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkoutLink => _driver.FindElement(By.LinkText("Checkout"));
        private IWebElement _myAccountLink => _driver.FindElement(By.LinkText("My account"));

        public void GoToShopPage()
        {
            _shopLink.Click();
        }

        public void GoToCartPage()
        {
            _cartLink.Click();
        }

        public void GoToCheckoutPage()
        {
            _checkoutLink.Click();
        }

        public void GoToMyAccountPage()
        {
            _myAccountLink.Click();
        }
    }
}
