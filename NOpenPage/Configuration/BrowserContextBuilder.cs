using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NOpenPage.Configuration
{
    public class BrowserContextBuilder : IBrowserConfiguration
    {
        private Func<IWebDriver> _driverProvider;
        private Func<ISearchContext, IWait<ISearchContext>> _waitFactory;

        public IBrowserConfiguration WithWebDriverResolver(Func<IWebDriver> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            _driverProvider = provider;
            return this;
        }

        public IBrowserConfiguration WithWaitFactory(Func<ISearchContext, IWait<ISearchContext>> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            _waitFactory = factory;
            return this;
        }

        public BrowserContext Build()
        {
            if (_driverProvider == null) throw new InvalidOperationException("WebDriverResolver was not set");
            var waitFactory = _waitFactory ?? DefaultWaitFactory;
            return new BrowserContext(_driverProvider, waitFactory);
        }

        private static IWait<ISearchContext> DefaultWaitFactory(ISearchContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var wait = new DefaultWait<ISearchContext>(context)
            {
                Timeout = TimeSpan.FromMinutes(1),
                PollingInterval = TimeSpan.FromMilliseconds(500.0)
            };
            wait.IgnoreExceptionTypes(typeof (NotFoundException));
            return wait;
        }
    }
}