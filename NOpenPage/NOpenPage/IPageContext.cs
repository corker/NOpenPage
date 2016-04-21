using System.Collections.Generic;
using OpenQA.Selenium;

namespace NOpenPage
{
    public interface IPageContext
    {
        IWebDriver Driver { get; }
        T Control<T>() where T : PageControl;
        IEnumerable<T> Controls<T>(By @by) where T : PageControl;
    }
}