using System;
using NOpenPage.Configuration;
using OpenQA.Selenium;

namespace NOpenPage
{
    public interface IPageControlContext
    {
        IPageControlContextImpl GetImpl(WebElementProvider provider, Type type);
    }
}