using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContext
    {
        private readonly Func<IWebDriver> _driverResolver;
        private readonly Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> _elementResolver;

        public BrowserContext(Func<IWebDriver> driverResolver,
            Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> elementResolver)
        {
            if (driverResolver == null) throw new ArgumentNullException(nameof(driverResolver));
            if (elementResolver == null) throw new ArgumentNullException(nameof(elementResolver));

            _driverResolver = driverResolver;
            _elementResolver = elementResolver;
        }

        public IWebDriver ResolveWebDriver()
        {
            var driver = _driverResolver();
            if (driver == null)
            {
                throw new InvalidOperationException("Can't resolve WebDriver. WebDriverResolver returns null.");
            }
            return driver;
        }

        public IResolveWebElements CreateWebElementResolver(ISearchContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return new WebElementResolver(context, _elementResolver);
        }

        private class WebElementResolver : IResolveWebElements
        {
            private readonly ISearchContext _context;
            private readonly Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> _resolver;

            public WebElementResolver(
                ISearchContext context,
                Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> resolver
                )
            {
                _context = context;
                _resolver = resolver;
            }

            public IWebElement Resolve(Func<ISearchContext, IWebElement> provider)
            {
                if (provider == null) throw new ArgumentNullException(nameof(provider));

                var element = _resolver(_context, provider);
                if (element == null)
                {
                    throw new InvalidOperationException("Can't resolve WebElement. Resolver returns null.");
                }
                return element;
            }
        }
    }
}