using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage
{
    public abstract class Page
    {
        private readonly IPageContext _context;

        protected Page(IPageContext context)
        {
            _context = context;
        }

        protected IWebDriver Driver => _context.Driver;

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