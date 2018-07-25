using System.Linq;
using OpenQA.Selenium;
using Xunit;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class NuGetOrgPage : Page
    {
        public NuGetOrgPage(IPageContext context) : base(context)
        {
        }

        public static void Navigate(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.nuget.org");
        }

        public void Search(string text)
        {
            Control<SearchBox>().Search(text);
        }

        public void AssertSearchResultsCountIs(int count)
        {
            var countByControl = Control<SearchResultList>().Items.Length;
            Assert.Equal(count, countByControl);

            var countByControls = Controls<SearchResultListItem>(By.ClassName("package")).Count();
            Assert.Equal(count, countByControls);
        }

        public void AssertCustomWebElementResolverExecuted()
        {
            Control<PageControlWithException>().AssertCustomWebElementResolverExecuted();
        }
    }
}