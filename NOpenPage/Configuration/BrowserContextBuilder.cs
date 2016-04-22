using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContextBuilder : IBrowserConfiguration
    {
        private Func<IWebDriver> _driverResolver;
        private Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> _elementResolver;

        public IBrowserConfiguration WithWebDriverResolver(Func<IWebDriver> resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            _driverResolver = resolver;
            return this;
        }

        public IBrowserConfiguration WithWebElementResolver(Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            _elementResolver = resolver;
            return this;
        }

        public BrowserContext Build()
        {
            if (_driverResolver == null) throw new InvalidOperationException("WebDriverResolver was not set");
            var elementResolver = _elementResolver ?? ((context, provider) => provider(context));
            return new BrowserContext(_driverResolver, elementResolver);
        }
    }
}