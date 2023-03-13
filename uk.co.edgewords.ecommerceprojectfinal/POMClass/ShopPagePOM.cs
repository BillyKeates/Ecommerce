using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.ecommerceprojectfinal.Support;

namespace uk.co.edgewords.ecommerceproject.POMClass
{
    internal class ShopPagePOM
    {
        private IWebDriver _driver;


        public ShopPagePOM(IWebDriver driver)
        {
            this._driver = driver;

        }

        Random myrand= new Random();

        private IWebElement _addToCartBtn => _driver.FindElement(By.CssSelector(".post-"+myrand.Next(27,38) + " >  .add_to_cart_button")); //button to add a random item to cart
        
        
        private IWebElement _viewCartBtn => _driver.FindElement(By.LinkText("View cart")); //button to view cart


        private IWebElement _footerAlert => _driver.FindElement(By.CssSelector(".woocommerce-store-notice__dismiss-link")); //link to dismiss alert box at footer

        public void AddItemToCart()
        {
            string browser = Environment.GetEnvironmentVariable("BROWSER");


            if (browser == "firefox")
            {
                //move the screen to the specified element
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", _addToCartBtn);

            }
            else
            {
                //scroll the screen to the chosen item
                Actions action = new Actions(_driver);
                action.MoveToElement(_addToCartBtn);
                action.Perform();
            }



            _addToCartBtn.Click();
        }

        public void ViewCart()
        {
            //Set explicit wait until item is added to cart
            MyHelpers help = new MyHelpers(_driver);
            help.WaitForElement(By.LinkText("View cart"), 3);

            _viewCartBtn.Click();

        }


        public void DismissAlert()
        {
            if (_footerAlert.Displayed)
            {
                _footerAlert.Click();
            }
        }


    }
}
