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
            var driverResolver = CreateWebDriverResolver(_driverResolver);
            return new BrowserContext(driverResolver, _elementResolvers);
        }

        private static WebDriverResolver CreateWebDriverResolver(WebDriverResolver driverResolver)
        {
            return () =>
            {
                var driver = driverResolver();
                if (driver == null)
                {
                    var message = "Can't resolve WebDriver. Resolver returns null.";
                    throw new InvalidOperationException(message);
                }
                return driver;
            };
        }
    }
}