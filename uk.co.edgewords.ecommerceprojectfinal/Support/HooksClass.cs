using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.ecommerceprojectfinal.Support
{
    [Binding]
    internal class HooksClass
    {
        public static IWebDriver driver;
        public static string browser;


        [Before]
        public void SetUp()
        {
            browser = Environment.GetEnvironmentVariable("BROWSER");

            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver(); break;
                case "chrome":
                    driver = new ChromeDriver(); break;
                default:
                    Console.WriteLine("Unknown browser - starting chrome");
                    driver = new ChromeDriver();
                    break;
            }

            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";


        }

        [After]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
