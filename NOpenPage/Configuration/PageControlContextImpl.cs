using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class PageControlContextImpl : IPageControlContextImpl, IPageControlContext
    {
        private readonly Lazy<IWebElement> _element;
        private readonly IProvideWebElementResolvers _elementResolvers;

        public PageControlContextImpl(Lazy<IWebElement> element, IProvideWebElementResolvers elementResolvers)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (elementResolvers == null) throw new ArgumentNullException(nameof(elementResolvers));
            _element = element;
            _elementResolvers = elementResolvers;
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

        public IPageControlContextImpl GetImpl(WebElementProvider provider, Type type)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            var element = new Lazy<IWebElement>(() => _elementResolvers.Get(type)(Element, provider));
            return new PageControlContextImpl(element, _elementResolvers);
        }
    }
}