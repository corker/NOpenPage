using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageContext : IPageControlContext, IPageContext
    {
        private readonly Lazy<IWebDriver> _driver;
        private readonly IProvideWebElementResolvers _elementResolvers;

        public PageContext(Lazy<IWebDriver> driver, IProvideWebElementResolvers elementResolvers)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (elementResolvers == null) throw new ArgumentNullException(nameof(elementResolvers));
            _driver = driver;
            _elementResolvers = elementResolvers;
        }

        public IWebDriver Driver => _driver.Value;

        public T Control<T>() where T : PageControl
        {
            return (T) Activator.CreateInstance(typeof (T), this);
        }

        public IEnumerable<T> Controls<T>(By @by) where T : PageControl
        {
            if (@by == null) throw new ArgumentNullException(nameof(@by));
            var elements = Driver.FindElements(@by);
            var controls = elements.Select(x => (T) Activator.CreateInstance(typeof (T), x, this));
            return controls;
        }

        public IPageControlContextImpl GetImpl(WebElementProvider provider, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            var element = new Lazy<IWebElement>(() => _elementResolvers.Get(type)(Driver, provider));
            return new PageControlContextImpl(element, _elementResolvers);
        }
    }
}