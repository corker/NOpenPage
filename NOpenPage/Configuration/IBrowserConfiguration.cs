using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NOpenPage.Configuration
{
    public interface IBrowserConfiguration
    {
        IBrowserConfiguration WithWebDriverResolver(Func<IWebDriver> provider);
        IBrowserConfiguration WithWaitFactory(Func<ISearchContext, IWait<ISearchContext>> factory);
    }
}