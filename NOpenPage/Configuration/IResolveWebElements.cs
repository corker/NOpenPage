using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public interface IResolveWebElements
    {
        IWebElement Resolve(Func<ISearchContext, IWebElement> provider);
    }
}