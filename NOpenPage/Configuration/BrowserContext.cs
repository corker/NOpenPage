using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContext
    {
        private readonly WebDriverResolver _driverResolver;

        public BrowserContext(WebDriverResolver driverResolver, IProvideWebElementResolvers elementResolvers)
        {
            Guard.NotNull(nameof(driverResolver), driverResolver);
            Guard.NotNull(nameof(elementResolvers), elementResolvers);

            _driverResolver = driverResolver;
            WebElementResolvers = elementResolvers;
        }

        public IProvideWebElementResolvers WebElementResolvers { get; }

        public IWebDriver ResolveWebDriver()
        {
            var driver = _driverResolver();
            if (driver == null)
            {
                throw new InvalidOperationException("Can't resolve WebDriver. WebDriverResolver returns null.");
            }
            return driver;
        }
    }
}