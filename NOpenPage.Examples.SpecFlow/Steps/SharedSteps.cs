using System;
using NOpenPage.Configuration;
using NOpenPage.Examples.SpecFlow.Pages.NuGetOrg;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace NOpenPage.Examples.SpecFlow.Steps
{
    [Binding]
    public class SharedSteps
    {
        private const string WebDriverKey = "WebDriver";

        static SharedSteps()
        {
            Browser.Configure(config =>
            {
                config
                    .WithWebDriverResolver(ResolveWebDriver)
                    .WithWebElementResolver(ResolveWebElement)
                    .WithWebElementResolver<SearchPanel>(ResolveSearchPanel);
            });
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

        private static IWebDriver ResolveWebDriver()
        {
            if (ScenarioContext.Current.ContainsKey(WebDriverKey))
            {
                return (IWebDriver) ScenarioContext.Current[WebDriverKey];
            }
            throw new InvalidOperationException("WebDriver not found");
        }

        private static IWebElement ResolveWebElement(ISearchContext context, WebElementProvider provider)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var wait = new DefaultWait<ISearchContext>(context)
            {
                Timeout = TimeSpan.FromMinutes(1),
                PollingInterval = TimeSpan.FromMilliseconds(500.0)
            };
            wait.IgnoreExceptionTypes(typeof (NotFoundException));
            return wait.Until(c => provider(c));
        }

        private static IWebElement ResolveSearchPanel(ISearchContext context, WebElementProvider provider)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var wait = new DefaultWait<ISearchContext>(context)
            {
                Timeout = TimeSpan.FromMinutes(10),
                PollingInterval = TimeSpan.FromMilliseconds(500.0)
            };
            wait.IgnoreExceptionTypes(typeof (NotFoundException));
            return wait.Until(c => provider(c));
        }
    }
}