using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContext
    {
        private readonly WebDriverResolver _driverResolver;

        public BrowserContext(WebDriverResolver driverResolver, IProvideWebElementResolvers elementResolvers)
        {
            if (driverResolver == null) throw new ArgumentNullException(nameof(driverResolver));
            if (elementResolvers == null) throw new ArgumentNullException(nameof(elementResolvers));

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