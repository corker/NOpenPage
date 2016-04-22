using System;

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
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            _driverResolver = resolver;
            return this;
        }

        public IBrowserConfiguration WithWebElementResolver(WebElementResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            _elementResolvers.SetDefault(resolver);
            return this;
        }

        public IBrowserConfiguration WithWebElementResolver<T>(WebElementResolver resolver) where T : PageControl
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
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