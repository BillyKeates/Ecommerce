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
    internal class CartPagePOM
    {

        private IWebDriver _driver;
        private ISpecFlowOutputHelper _outputHelper;

        public CartPagePOM(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            this._driver = driver;
            this._outputHelper = outputHelper;
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
            MyHelpers help = new MyHelpers(_driver, _outputHelper);
            help.ScreenshotPage(_driver, "couponApplied.png");
        }

        public (decimal, decimal) CheckDiscount(int discount)
        {
            //convert the price displayed on the site into a decimal
            decimal itemPrice = Convert.ToDecimal(_itemPrice.Text.Substring(1));

            //Set explicit wait until disount is displayed
            MyHelpers help = new MyHelpers(_driver, _outputHelper);
            help.WaitForElement(By.CssSelector(".cart-discount > td > .amount"), 3);

            //convert the discount calculated on the site to a decimal
            decimal actualDiscount = Convert.ToDecimal(_discount.Text.Substring(1));

            //calculate what the discount should be
            decimal expectedDiscount = itemPrice * (discount * 0.01m);

            _outputHelper.WriteLine("The Expected discount is: "+expectedDiscount + ", the actual discount found was " + actualDiscount);
            return (actualDiscount, expectedDiscount);
        }

        public (decimal, decimal) CheckPrice(int discount)
        {
            //Convert the shipping price displayed on site to a decimal
            decimal shippingPrice = Convert.ToDecimal(_shippingCost.Text.Substring(1));

            //convert the total price calculated by the site to a decimal
            decimal actualPrice = Convert.ToDecimal(_totalPrice.Text.Substring(1));

            //Convert the original price of the item to a decimal
            decimal itemPrice = Convert.ToDecimal(_itemPrice.Text.Substring(1));

            //calculate what the discount and total price should be
            decimal expectedDiscount = itemPrice * (discount * 0.01m);
            decimal expectedTotalPrice = (itemPrice - expectedDiscount) + shippingPrice;
            _outputHelper.WriteLine("The expected total is "+expectedTotalPrice + ", the actual total found was " + actualPrice);

            return (actualPrice, expectedTotalPrice);            
        }

        public void GoToCheckout()
        {
            _checkoutBtn.Click();
        }

        public bool IsCartEmpty()
        {
            try
            {
                IWebElement removeBtn = _driver.FindElement(By.CssSelector(".remove"));
                removeBtn.Click();
                _outputHelper.WriteLine("Item removed from cart.");
                return false;
            }
            catch (Exception ex)
            {
                _outputHelper.WriteLine("Cart is already empty.");
                return true;
            }
        }
    }
}
