using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContextBuilder : IBrowserConfiguration
    {
        private readonly WebElementResolverRegistry _elementResolvers;
        private WebDriverResolver _driverResolver;

        public BrowserContextBuilder()
        {
            _elementResolvers = new WebElementResolverRegistry();
        }

        public IBrowserConfiguration WithWebDriverResolver(WebDriverResolver resolver)
        {
            Guard.NotNull(nameof(resolver), resolver);
            _driverResolver = resolver;
            return this;
        }

        public IBrowserConfiguration WithWebElementResolver(WebElementResolver resolver)
        {
            Guard.NotNull(nameof(resolver), resolver);
            _elementResolvers.SetDefault(resolver);
            return this;
        }

        public IBrowserConfiguration WithWebElementResolver<T>(WebElementResolver resolver) where T : PageControl
        {
            Guard.NotNull(nameof(resolver), resolver);
            _elementResolvers.Add<T>(resolver);
            return this;
        }

        public BrowserContext Build()
        {
            if (_driverResolver == null) throw new InvalidOperationException("WebDriverResolver was not set");
            return new BrowserContext(_driverResolver, _elementResolvers);
        }
    }
}