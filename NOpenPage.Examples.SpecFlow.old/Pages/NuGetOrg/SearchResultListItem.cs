using OpenQA.Selenium;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class SearchResultListItem : PageControl
    {
        public SearchResultListItem(IWebElement element, IPageControlContext context)
            : base(element, context)
        {
        }
    }
}