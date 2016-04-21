using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public interface IPageControlContextImpl
    {
        IWebElement Element { get; }
        T Control<T>() where T : PageControl;
        IEnumerable<T> Controls<T>(By @by) where T : PageControl;
    }
}