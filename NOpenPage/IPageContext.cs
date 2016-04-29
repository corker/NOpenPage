using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     A page context interface
    ///     - Provides an access to <see cref="IWebDriver" /> for <see cref="Page" /> class
    ///     - Serves as a factory for <see cref="PageControl" />
    /// </summary>
    public interface IPageContext
    {
        /// <summary>
        ///     Provides an instance of a current driver
        /// </summary>
        IWebDriver Driver { get; }

        /// <summary>
        ///     Creates an instance of <typeparamref name="T" />
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <returns>
        ///     A new instance of <typeparamref name="T" />
        /// </returns>
        T Control<T>() where T : PageControl;

        /// <summary>
        ///     Creates an array of <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <param name="by">Explains how to find an element within a document</param>
        /// <returns>
        ///     An array of new instances <typeparamref name="T" />
        /// </returns>
        IEnumerable<T> Controls<T>(By @by) where T : PageControl;
    }
}