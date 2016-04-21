using OpenQA.Selenium;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class SearchPanel : PageControl
    {
        public SearchPanel(IPageControlContext context) : base(By.TagName("form"), context)
        {
        }

        public void Search(string text)
        {
            var input = Element.FindElement(By.Id("searchBoxInput"));
            input.Click();
            input.SendKeys(text);
            var button = Element.FindElement(By.Id("searchBoxSubmit"));
            button.Click();
        }
    }
}