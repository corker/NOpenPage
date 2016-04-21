using System;
using System.Collections.Generic;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    public abstract class PageControl
    {
        private readonly IPageControlContextImpl _context;

        protected PageControl(IWebElement element, IPageControlContext context) : this(x => element, context)
        {
        }

        protected PageControl(By @by, IPageControlContext context) : this(c => c.FindElement(@by), context)
        {
        }

        private PageControl(Func<ISearchContext, IWebElement> element, IPageControlContext context)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (context == null) throw new ArgumentNullException(nameof(context));

            _context = context.GetImpl(element);
        }

        protected IWebElement Element => _context.Element;

        protected T Control<T>() where T : PageControl
        {
            return _context.Control<T>();
        }

        protected IEnumerable<T> Controls<T>(By @by) where T : PageControl
        {
            return _context.Controls<T>(@by);
        }
    }
}