using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageContext : IPageControlContext, IPageContext
    {
        private readonly IResolveWebElements _elements;

        public PageContext(IWebDriver driver, IResolveWebElements elements)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (elements == null) throw new ArgumentNullException(nameof(elements));

            Driver = driver;
            _elements = elements;
        }

        public IWebDriver Driver { get; }

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

        public IPageControlContextImpl GetImpl(Func<ISearchContext, IWebElement> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var element = new Lazy<IWebElement>(() => _elements.Resolve(provider));
            return new PageControlContextImpl(element, _elements);
        }
    }
}