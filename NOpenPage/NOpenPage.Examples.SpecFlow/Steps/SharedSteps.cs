using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace NOpenPage.Examples.SpecFlow.Steps
{
    [Binding]
    public class SharedSteps
    {
        private const string WebDriverKey = "WebDriver";

        static SharedSteps()
        {
            Browser.Configure(c => { c.WithWebDriverResolver(GetWebDriver); });
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            IWebDriver driver = new ChromeDriver();
            ScenarioContext.Current[WebDriverKey] = driver;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = (IWebDriver) ScenarioContext.Current[WebDriverKey];
            driver.Quit();
        }

        private static IWebDriver GetWebDriver()
        {
            if (ScenarioContext.Current.ContainsKey(WebDriverKey))
            {
                return (IWebDriver) ScenarioContext.Current[WebDriverKey];
            }
            throw new InvalidOperationException("WebDriver not found");
        }
    }
}