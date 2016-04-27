using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     A page context interface provides an access to <see cref="IWebDriver" /> for <see cref="Page" /> class and serves
    ///     as a factory for <see cref="PageControl" />.
    /// </summary>
    public interface IPageContext
    {
        /// <summary>
        ///     Provides an instance of the current <see cref="IWebDriver" />.
        /// </summary>
        IWebDriver Driver { get; }

        /// <summary>
        ///     Creates an instance of
        ///     <typeparam name="T"></typeparam>
        ///     .
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <returns>
        ///     An instance of
        ///     <typeparam name="T"></typeparam>
        /// </returns>
        T Control<T>() where T : PageControl;

        /// <summary>
        ///     Creates an array of
        ///     <typeparam name="T"></typeparam>
        ///     .
        /// </summary>
        /// <typeparam name="T">A type of a page control class to create</typeparam>
        /// <param name="by">Explains how to find an element within a document</param>
        /// <returns>
        ///     An array of
        ///     <typeparam name="T"></typeparam>
        /// </returns>
        IEnumerable<T> Controls<T>(By @by) where T : PageControl;
    }
}