using System;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    /// <summary>
    ///     An entry point for NOpenPage.
    ///     - Configures integration with Selenium WebDriver
    ///     - Serves as a factory for user defined page classes
    /// </summary>
    public static class Browser
    {
        private static Lazy<BrowserContext> _context;

        static Browser()
        {
            _context = new Lazy<BrowserContext>(() => new BrowserContextBuilder().Build());
        }

        /// <summary>
        ///     Configure Browser before start using NOpenPage
        /// </summary>
        /// <param name="action">A configuration to be applied</param>
        public static void Configure(Action<IBrowserConfiguration> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            var builder = new BrowserContextBuilder();
            action(builder);
            _context = new Lazy<BrowserContext>(() => builder.Build());
        }

        /// <summary>
        ///     Create a page class of <typeparamref name="T" /> with a current <see cref="PageContext" />.
        /// </summary>
        /// <typeparam name="T">A type of a page class to create</typeparam>
        /// <returns>
        ///     A new instance of <typeparamref name="T" />
        /// </returns>
        public static T On<T>() where T : Page
        {
            var page = CreatePage<T>();
            return page;
        }

        /// <summary>
        ///     Create a page class of <typeparamref name="T" /> with the currect <see cref="PageContext" /> and open this page in
        ///     a browser.
        /// </summary>
        /// <typeparam name="T">A type of a page class to create</typeparam>
        /// <returns>
        ///     A new instance of <typeparamref name="T" />
        /// </returns>
        public static T Open<T>() where T : Page, IOpenPages
        {
            var page = CreatePage<T>();
            page.Open();
            return page;
        }

        private static T CreatePage<T>() where T : Page
        {
            var context = _context.Value;
            var driver = new Lazy<IWebDriver>(context.ResolveWebDriver);
            var elementResolvers = context.WebElementResolvers;
            var pageContext = new PageContext(driver, elementResolvers);
            return (T) Activator.CreateInstance(typeof (T), pageContext);
        }
    }
}