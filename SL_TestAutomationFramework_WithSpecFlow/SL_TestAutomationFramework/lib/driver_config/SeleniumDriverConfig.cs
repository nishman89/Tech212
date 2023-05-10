using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL_TestAutomationFramework.lib.driver_config
{
    public class SeleniumDriverConfig<T> where T : IWebDriver, new()
    {
        public IWebDriver Driver { get; set; }
        public SeleniumDriverConfig(int pageLoadInSecs, int implicitWaitInSecs, bool isHeadless)
        {
            // if the browser is headless, create headless driver
            Driver = isHeadless ? HeadlessDriver() : new T();
            // This is the time the driver will wait for the page to load
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadInSecs);
            // This is the time the driver waits for the elements to load
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitInSecs);
        }


        private IWebDriver HeadlessDriver()
        {
            if (typeof(T) == typeof(ChromeDriver))
            {
                var options = new ChromeOptions();
                options.AddArgument("headless");
                return new ChromeDriver(options);
            }
            else if (typeof(T) == typeof(FirefoxDriver))
            {
                var options = new FirefoxOptions();
                options.AddArgument("--headless");
                return new FirefoxDriver(options);
            }
            else
            {
                throw new ArgumentException("Headless mode not supported for this browser.");
            }
        }


    }
}
