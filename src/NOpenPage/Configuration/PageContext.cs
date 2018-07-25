using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageContext : IPageControlContext, IPageContext
    {
        private readonly WebDriverResolver _driverResolver;
        private readonly IProvideWebElementResolvers _elementResolvers;

        public PageContext(WebDriverResolver driverResolver, IProvideWebElementResolvers elementResolvers)
        {
            Guard.NotNull(nameof(driverResolver), driverResolver);
            Guard.NotNull(nameof(elementResolvers), elementResolvers);
            _driverResolver = driverResolver;
            _elementResolvers = elementResolvers;
        }

        public IWebDriver Driver =>  _driverResolver();

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

            var element = new Lazy<IWebElement>(() => _elementResolvers.Get(type)(Driver, provider));
            return new PageControlContextImpl(element, _elementResolvers);
        }
    }
}