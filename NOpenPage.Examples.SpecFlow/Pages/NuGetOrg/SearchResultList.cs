using System.Linq;
using OpenQA.Selenium;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class SearchResultList : PageControl
    {
        public SearchResultList(IPageControlContext context) : base(By.Id("searchResults"), context)
        {
        }

        public SearchResultListItem[] Items => Controls<SearchResultListItem>(By.XPath("li")).ToArray();
    }
}