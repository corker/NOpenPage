using OpenQA.Selenium;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class SearchBoxSubmit : PageControl
    {
        public SearchBoxSubmit(IPageControlContext context) : base(By.TagName("button"), context)
        {
        }

        public void Submit()
        {
            Element.Click();
        }
    }
}