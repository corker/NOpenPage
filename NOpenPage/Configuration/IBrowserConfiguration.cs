using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public interface IBrowserConfiguration
    {
        IBrowserConfiguration WithWebDriverResolver(Func<IWebDriver> resolver);
        IBrowserConfiguration WithWebElementResolver(Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> resolver);
    }
}