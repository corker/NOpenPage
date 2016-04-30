using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

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
            Control<SearchBox>().Search(text);
        }

        public void AssertSearchResultsCountIs(int count)
        {
            var countByControl = Control<SearchResultList>().Items.Length;
            Assert.AreEqual(count, countByControl);

            var countByControls = Controls<SearchResultListItem>(By.ClassName("package")).Count();
            Assert.AreEqual(count, countByControls);
        }

        public void AssertCustomWebElementResolverExecuted()
        {
            Control<PageControlWithException>().AssertCustomWebElementResolverExecuted();
        }
    }
}