using System;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     A page context interface. Creates <see cref="IPageControlContextImpl" /> for <see cref="PageControl" /> class.
    /// </summary>
    public interface IPageControlContext
    {
        /// <summary>
        ///     Creates a specific page control context.
        /// </summary>
        /// <param name="provider">A provider for <see cref="ISearchContext" /></param>
        /// <param name="type">A type of a page control that context will be used for</param>
        /// <returns>A new instance of a page control specific context</returns>
        IPageControlContextImpl GetImpl(WebElementProvider provider, Type type);
    }
}