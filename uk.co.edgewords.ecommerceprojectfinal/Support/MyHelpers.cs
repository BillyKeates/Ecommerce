using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.ecommerceprojectfinal.Support
{
    internal class MyHelpers
    {

        private IWebDriver driver;

        public MyHelpers(IWebDriver driver)
        {
            this.driver = driver;

        }

        public void WaitForElement(By locator, int timeToWait)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait) );
            wait.Until(drv => drv.FindElement(locator).Enabled);
        }

    }
}
