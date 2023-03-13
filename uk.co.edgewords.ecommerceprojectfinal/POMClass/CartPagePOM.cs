using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.ecommerceprojectfinal.Support;

namespace uk.co.edgewords.ecommerceproject.POMClass
{
    internal class CartPagePOM
    {

        private IWebDriver _driver;

        public CartPagePOM(IWebDriver driver)
        {
            this._driver = driver;
        }


        private IWebElement _couponInput => _driver.FindElement(By.Id("coupon_code")); //coupon input box
        private IWebElement _applyCouponBtn => _driver.FindElement(By.CssSelector(".coupon > button")); //button to apply coupon

        private IWebElement _checkoutBtn => _driver.FindElement(By.LinkText("Proceed to checkout")); //button to proceed to checkout


        private IWebElement _itemPrice => _driver.FindElement(By.CssSelector(".cart-subtotal > td")); //element containing original price of the item
        private IWebElement _discount => _driver.FindElement(By.CssSelector(".cart-discount > td > .amount")); //element containing total dicount applied

        private IWebElement _shippingCost => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span")); //element containing shipping fees fro the item

        private IWebElement _totalPrice => _driver.FindElement(By.CssSelector(".order-total > td")); //element containing the calculated price the user must pay


        public void EnterCoupon(string coupon)
        {
            _couponInput.SendKeys(coupon);
        }

        public void ApplyCoupon()
        {
            _applyCouponBtn.Click();

        }


        public bool CheckDiscount()
        {
            //convert the price displayed on the site into a decimal
            decimal ItemPrice = Convert.ToDecimal(_itemPrice.Text.Substring(1));

            //Set explicit wait until disount is displayed
            MyHelpers help = new MyHelpers(_driver);
            help.WaitForElement(By.CssSelector(".cart-discount > td > .amount"), 3);


            //convert the discount calculated on the site to a decimal
            decimal ActualDiscount = Convert.ToDecimal(_discount.Text.Substring(1));

            //calculate what the discount should be
            decimal ExpectedDiscount = ItemPrice * 0.15m;

            Console.WriteLine("The Expected discount is: "+ExpectedDiscount + ", the actual discount found was " + ActualDiscount);

            if(ActualDiscount == ExpectedDiscount)
            {
                return true;
            }
            return false;

        }

        public bool CheckPrice()
        {
            //Convert the shipping price displayed on site to a decimal
            decimal ShippingPrice = Convert.ToDecimal(_shippingCost.Text.Substring(1));

            //convert the total price calculated by the site to a decimal
            decimal ActualPrice = Convert.ToDecimal(_totalPrice.Text.Substring(1));

            //Convert the original price of the item to a decimal
            decimal ItemPrice = Convert.ToDecimal(_itemPrice.Text.Substring(1));

            //calculate what the discount and total price should be
            decimal ExpectedDiscount = ItemPrice * 0.15m;
            decimal ExpectedTotalPrice = (ItemPrice - ExpectedDiscount) + ShippingPrice;
            Console.WriteLine("The expected total is "+ExpectedTotalPrice + ", the actual total found was " + ActualPrice);


            if(ActualPrice == ExpectedTotalPrice)
            {
                return true;
            }

            return false;

            
        }

        


        public void GoToCheckout()
        {
            _checkoutBtn.Click();
        }

    }
}
