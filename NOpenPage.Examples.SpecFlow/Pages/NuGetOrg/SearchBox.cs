using OpenQA.Selenium;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class SearchBox : PageControl
    {
        public SearchBox(IPageControlContext context) : base(By.TagName("form"), context)
        {
        }

        public void Search(string text)
        {
            var input = Element.FindElement(By.Id("searchBoxInput"));
            input.Click();
            input.SendKeys(text);
            Control<SearchBoxSubmit>().Submit();
        }
    }
}