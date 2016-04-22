using System;
using NOpenPage.Configuration;

namespace NOpenPage
{
    public static class Browser
    {
        private static Lazy<BrowserContext> _context;

        static Browser()
        {
            _context = new Lazy<BrowserContext>(() => new BrowserContextBuilder().Build());
        }

        public static void Configure(Action<IBrowserConfiguration> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            var builder = new BrowserContextBuilder();
            action(builder);
            _context = new Lazy<BrowserContext>(() => builder.Build());
        }

        public static T On<T>() where T : Page
        {
            var driver = _context.Value.ResolveWebDriver();
            var elements = _context.Value.CreateWebElementResolver(driver);
            var context = new PageContext(driver, elements);
            return (T) Activator.CreateInstance(typeof (T), context);
        }

        public static T Open<T>() where T : Page, IOpenPages
        {
            var page = On<T>();
            page.Open();
            return page;
        }
    }
}