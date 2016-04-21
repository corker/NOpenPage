using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NOpenPage.Configuration
{
    public class BrowserContext
    {
        private readonly Func<IWebDriver> _driverResolver;
        private readonly Func<ISearchContext, IWait<ISearchContext>> _waitFactory;

        public BrowserContext(Func<IWebDriver> driverResolver, Func<ISearchContext, IWait<ISearchContext>> waitFactory)
        {
            if (driverResolver == null) throw new ArgumentNullException(nameof(driverResolver));
            if (waitFactory == null) throw new ArgumentNullException(nameof(waitFactory));

            _driverResolver = driverResolver;
            _waitFactory = waitFactory;
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

            Func<IWait<ISearchContext>> waitFactory = () => _waitFactory(context);
            return new WebElementResolver(waitFactory);
        }

        private class WebElementResolver : IResolveWebElements
        {
            private readonly Func<IWait<ISearchContext>> _waitFactory;

            public WebElementResolver(Func<IWait<ISearchContext>> waitFactory)
            {
                _waitFactory = waitFactory;
            }

            public IWebElement Resolve(Func<ISearchContext, IWebElement> provider)
            {
                if (provider == null) throw new ArgumentNullException(nameof(provider));

                var wait = _waitFactory();
                if (wait == null)
                {
                    throw new InvalidOperationException("Can't create Waiter. WaitFactory returns null.");
                }
                return wait.Until(provider);
            }
        }
    }
}