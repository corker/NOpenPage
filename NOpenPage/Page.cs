using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     A base class for page objects
    ///     - Provides an access to the currect <see cref="IWebDriver" />
    ///     - Serves as a factory for <see cref="PageControl" /> classes
    /// </summary>
    public abstract class Page
    {
        private readonly IPageContext _context;

        /// <summary>
        ///     Initializes a new instance with a page context.
        /// </summary>
        /// <param name="context">A page context</param>
        protected Page(IPageContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Provides an instance of a driver.
        /// </summary>
        protected IWebDriver Driver => _context.Driver;

        /// <summary>
        ///     Creates an instance of <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <returns>
        ///     A new instance of <typeparamref name="T" />
        /// </returns>
        protected T Control<T>() where T : PageControl
        {
            return _context.Control<T>();
        }

        /// <summary>
        ///     Creates an array of <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <param name="by">Explains how to find an element within a document</param>
        /// <returns>
        ///     An array of new instances <typeparamref name="T" />
        /// </returns>
        protected IEnumerable<T> Controls<T>(By @by) where T : PageControl
        {
            return _context.Controls<T>(@by);
        }
    }
}