using System.Collections.Generic;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     A base class for page controls
    ///     - Provides an access to a <see cref="IWebElement" />
    ///     - Serves as a factory for nested <see cref="PageControl" /> classes
    /// </summary>
    public abstract class PageControl
    {
        private readonly IPageControlContextImpl _context;

        /// <summary>
        ///     Initializes a new instance with a page control context when element is available.
        ///     E.g. <see cref="PageControlContextImpl.Controls{T}" /> creates page controls with elements that was found.
        /// </summary>
        /// <param name="element">An element that will be in the specific context for the page control</param>
        /// <param name="context">A page control context to resolve a page control specific context</param>
        protected PageControl(IWebElement element, IPageControlContext context) : this(x => element, context)
        {
        }

        /// <summary>
        ///     Initializes a new instance with a page control context when only <see cref="By" /> s available.
        ///     E.g. <see cref="PageControlContextImpl.Control{T}" /> creates page control using <see cref="By" />.
        /// </summary>
        /// <param name="by">A query to search for an element</param>
        /// <param name="context">A page control context to resolve a page control specific context</param>
        protected PageControl(By by, IPageControlContext context) : this(c => c.FindElement(by), context)
        {
        }

        private PageControl(WebElementProvider provider, IPageControlContext context)
        {
            Guard.NotNull(nameof(provider), provider);
            Guard.NotNull(nameof(context), context);

            _context = context.GetImpl(provider, GetType());
        }

        /// <summary>
        ///     Provides an instance of an element.
        /// </summary>
        protected IWebElement Element => _context.Element;

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
        /// <param name="by">Explains how to find an element within the current element</param>
        /// <returns>
        ///     An array of new instances <typeparamref name="T" />
        /// </returns>
        protected IEnumerable<T> Controls<T>(By by) where T : PageControl
        {
            return _context.Controls<T>(by);
        }
    }
}