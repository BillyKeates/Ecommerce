using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace uk.co.edgewords.ecommerceprojectfinal.Support
{
    internal class MyHelpers
    {
        private IWebDriver _driver;
        private ISpecFlowOutputHelper _outputHelper;

        public MyHelpers(IWebDriver driver, ISpecFlowOutputHelper outputHelper)
        {
            this._driver = driver;
            this._outputHelper = outputHelper;
        }

        public void WaitForElement(By locator, int timeToWait)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeToWait) );
            wait.Until(drv => drv.FindElement(locator).Enabled);
        }

        public void ScreenshotPage(IWebDriver driver, string screenshotname)
        {
            string screenshotpath = "C:\\screenshots\\" + screenshotname;
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(screenshotpath, ScreenshotImageFormat.Png);

            _outputHelper.AddAttachment(screenshotpath);
            TestContext.AddTestAttachment(screenshotpath, screenshotname);
        }
    }
}
