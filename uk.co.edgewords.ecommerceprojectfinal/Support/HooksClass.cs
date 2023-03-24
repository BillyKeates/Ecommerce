using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.edgewords.ecommerceproject.POMClass;

namespace uk.co.edgewords.ecommerceprojectfinal.Support
{
    [Binding]
    internal class HooksClass
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private ISpecFlowOutputHelper _outputHelper;
        private string browser;

        public HooksClass(ScenarioContext scenarioContext,ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _outputHelper = outputHelper;
        }

        [Before]
        public void SetUp()
        {
            _scenarioContext["output"] = _outputHelper;
            browser = Environment.GetEnvironmentVariable("BROWSER");
            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver(); break;
                case "chrome":
                    _driver = new ChromeDriver(); break;
                default:
                    _outputHelper.WriteLine("Unknown browser - starting chrome");
                    _driver = new ChromeDriver();
                    break;
            }

            _scenarioContext["myDriver"] = _driver;
            _driver.Url = Environment.GetEnvironmentVariable("BASEURL");

            LogInPagePOM login = new LogInPagePOM(_driver,_outputHelper);
            //Enter username and password into the input elements on the log in page
            login.EnterUsername(Environment.GetEnvironmentVariable("SECRET_USERNAME"));
            login.EnterPassword(Environment.GetEnvironmentVariable("SECRET_PASSWORD"));
            login.ClickLogIn();

            //Check if demo store notice is displayed
            //Click dismiss if it is displayed
            ShopPagePOM shop = new ShopPagePOM(_driver,_outputHelper);
            shop.DismissAlert();
        }

        [After]
        public void TearDown()
        {
            //Empty the cart if item is in it
            TopNavPOM topNav = new TopNavPOM(_driver);
            topNav.GoToCartPage();
            
            CartPagePOM cart = new CartPagePOM(_driver,_outputHelper);
            while(cart.IsCartEmpty() == false)
            {
                cart.IsCartEmpty();
            }
            
            //Go to my account page
            topNav.GoToMyAccountPage();

            //Log out of account
            MyAccountPagePOM myAccount = new MyAccountPagePOM(_driver,_outputHelper);
            myAccount.Logout();

            _driver.Quit();
        }
    }
}