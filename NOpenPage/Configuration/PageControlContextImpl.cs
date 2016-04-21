using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageControlContextImpl : IPageControlContextImpl, IPageControlContext
    {
        private readonly Lazy<IWebElement> _element;
        private readonly IResolveWebElements _elements;

        public PageControlContextImpl(Lazy<IWebElement> element, IResolveWebElements elements)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (elements == null) throw new ArgumentNullException(nameof(elements));

            _element = element;
            _elements = elements;
        }

        public IWebElement Element => _element.Value;

        public T Control<T>() where T : PageControl
        {
            return (T) Activator.CreateInstance(typeof (T), this);
        }

        public IEnumerable<T> Controls<T>(By @by) where T : PageControl
        {
            if (@by == null) throw new ArgumentNullException(nameof(@by));

            var elements = Element.FindElements(@by);
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