using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageContext : IPageControlContext, IPageContext
    {
        private readonly Lazy<IWebDriver> _driver;
        private readonly IProvideWebElementResolvers _resolvers;

        public PageContext(Lazy<IWebDriver> driver, IProvideWebElementResolvers resolvers)
        {
            Guard.NotNull(nameof(driver), driver);
            Guard.NotNull(nameof(resolvers), resolvers);
            _driver = driver;
            _resolvers = resolvers;
        }

        public IWebDriver Driver => _driver.Value;

        public T Control<T>() where T : PageControl
        {
            return (T) Activator.CreateInstance(typeof(T), this);
        }

        public IEnumerable<T> Controls<T>(By by) where T : PageControl
        {
            Guard.NotNull(nameof(by), by);
            var elements = Driver.FindElements(by);
            var type = typeof(T);
            var controls = elements.Select(x => (T) Activator.CreateInstance(type, x, this));
            return controls;
        }

        public IPageControlContextImpl GetImpl(WebElementProvider provider, Type type)
        {
            Guard.NotNull(nameof(provider), provider);
            Guard.NotNull(nameof(type), type);

            var element = new Lazy<IWebElement>(() => _resolvers.Get(type)(Driver, provider));
            return new PageControlContextImpl(element, _resolvers);
        }
    }
}