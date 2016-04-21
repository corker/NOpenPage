using NUnit.Framework;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class NuGetOrgPage : Page, IOpenPages
    {
        public NuGetOrgPage(IPageContext context) : base(context)
        {
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl("https://www.nuget.org");
        }

        public void Search(string text)
        {
            Control<SearchPanel>().Search(text);
        }

        public void AssertSearchResultsCountIs(int count)
        {
            Assert.AreEqual(count, Control<SearchResultList>().Items.Length);
        }
    }
}