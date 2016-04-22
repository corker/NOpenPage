using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public delegate IWebDriver WebDriverResolver();

    public delegate IWebElement WebElementResolver(ISearchContext context, WebElementProvider provider);

    public delegate IWebElement WebElementProvider(ISearchContext context);

    public interface IBrowserConfiguration
    {
        IBrowserConfiguration WithWebDriverResolver(WebDriverResolver resolver);
        IBrowserConfiguration WithWebElementResolver(WebElementResolver resolver);
        IBrowserConfiguration WithWebElementResolver<T>(WebElementResolver resolver) where T : PageControl;
    }
}