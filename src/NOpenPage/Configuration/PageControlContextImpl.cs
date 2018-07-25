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
            Guard.NotNull(nameof(element), element);
            Guard.NotNull(nameof(elementResolvers), elementResolvers);

            _element = element;
            _elementResolvers = elementResolvers;
        }

        public IPageControlContextImpl GetImpl(WebElementProvider provider, Type type)
        {
            Guard.NotNull(nameof(provider), provider);
            Guard.NotNull(nameof(type), type);

            var element = new Lazy<IWebElement>(() => _elementResolvers.Get(type)(Element, provider));
            return new PageControlContextImpl(element, _elementResolvers);
        }

        public IWebElement Element => _element.Value;

        public T Control<T>() where T : PageControl
        {
            return (T) Activator.CreateInstance(typeof(T), this);
        }

        public IEnumerable<T> Controls<T>(By by) where T : PageControl
        {
            Guard.NotNull(nameof(by), by);
            var elements = Element.FindElements(by);
            var type = typeof(T);
            var controls = elements.Select(x => (T) Activator.CreateInstance(type, x, this));
            return controls;
        }
    }
}