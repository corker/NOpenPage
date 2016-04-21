using System;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    public interface IPageControlContext
    {
        IPageControlContextImpl GetImpl(Func<ISearchContext, IWebElement> provider);
    }
}