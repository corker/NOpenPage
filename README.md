# NOpenPage [![Build status](https://ci.appveyor.com/api/projects/status/p4f9evyjh6rs9hgt?svg=true)](https://ci.appveyor.com/project/corker/nopenpage) [![Coveralls](https://img.shields.io/coverity/scan/8717.svg)](https://scan.coverity.com/projects/corker-nopenpage) [![NuGet Status](http://img.shields.io/nuget/v/NOpenPage.svg?style=flat)](https://www.nuget.org/packages/NOpenPage/) [![Join the chat at https://gitter.im/corker/NOpenPage](https://badges.gitter.im/corker/NOpenPage.svg)](https://gitter.im/corker/NOpenPage?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

A page object pattern implementation for Selenium.

As Martin Fowler explains in this  [article](http://martinfowler.com/bliki/PageObject.html)

> When you write tests against a web page, you need to refer to elements within that web page in order to click links and determine what's displayed. However, if you write tests that manipulate the HTML elements directly your tests will be brittle to changes in the UI. A page object wraps an HTML page, or fragment, with an application-specific API, allowing you to manipulate page elements without digging around in the HTML.

Why?
====

There are some implementations for the page object pattern like [Bumblebee](https://www.nuget.org/packages/Bumblebee.Automation) or [LoadableComponent](https://github.com/SeleniumHQ/selenium/wiki/LoadableComponent) in [WebDriver.Support](https://www.nuget.org/packages/Selenium.Support/). Then why do you need another one? The motivation behind NOpenPage is to have a page object layer with a little learning curve and flexible enough to not to stand on your way.

What?
====

There are only three classes that you need to be aware of to start with NOpenPage:
- Browser is a static class that is an entry point to page and control implementations;
- Page is a base class for all page objects;
- PageControl is a base class for all page controls.

How?
====

1. Install a nuget package
--

```
Install-Package NOpenPage
```

2. Configure NOpenPage
--
Here is a code from a sample project that can be found in this repository. The code shows how to configure NOpenPage with [SpecFlow](http://www.specflow.org/) - BDD framework that implements Gherkin language for .NET environment.
```
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
```
3. Use Browser class from tests
--
Here is a code from a sample project that can be found in this repository. The code shows how to use NOpenPage with [SpecFlow](http://www.specflow.org/) - BDD framework that implements Gherkin language for .NET environment.
```
namespace NOpenPage.Examples.SpecFlow.Steps
{
    [Binding]
    public class NuGetOrgSteps
    {
        [Given(@"I have opened a nuget home page")]
        public void GivenIHaveOpenedANugetHomePage()
        {
            Browser.Open<NuGetOrgPage>();
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string text)
        {
            Browser.On<NuGetOrgPage>().Search(text);
        }

        [Then(@"I can see (.*) packages")]
        public void ThenICanSeePackages(int count)
        {
            Browser.On<NuGetOrgPage>().AssertSearchResultsCountIs(count);
        }
    }
}
```

Please be aware, this is an alpha version. Thus not tested well and lack of comments in the code. To be continued...

Who am I?
--
My name is Michael Borisov. I'm interested in continuous delivery, distributed systems, CQRS, DDD, event sourcing, micro services architecture and everything related to the topic.

If you have any questions or comments regarding to the project please feel free to contact me on [Twitter](https://twitter.com/fkem) or [LinkedIn](https://www.linkedin.com/in/michaelborisov)

Happy coding!
