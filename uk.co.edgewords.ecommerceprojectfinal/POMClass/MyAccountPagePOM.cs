using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.ecommerceproject.POMClass
{
    internal class MyAccountPagePOM
    {
        private IWebDriver _driver;

        public MyAccountPagePOM(IWebDriver driver)
        {
            this._driver = driver;
        }


        private IWebElement _logOutBtn => _driver.FindElement(By.LinkText("Logout")); //Log out button

        private IWebElement _ordersLink => _driver.FindElement(By.LinkText("Orders")); //Button to go to orders page
        private string _orderNum => _driver.FindElement(By.CssSelector(".woocommerce-orders-table > tbody > tr:nth-child(1) > td:nth-child(1)")).Text.Substring(1); //string containing the order number

        public void Logout()
        {
            _logOutBtn.Click();
        }

        

        public void GoToOrders()
        {
            _ordersLink.Click();
        }

        public string GetOrderNum2()
        {
            return _orderNum;
        }
    }
}
