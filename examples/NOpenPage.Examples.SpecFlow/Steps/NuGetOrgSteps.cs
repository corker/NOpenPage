using NOpenPage.Examples.SpecFlow.Pages.NuGetOrg;
using TechTalk.SpecFlow;

namespace NOpenPage.Examples.SpecFlow.Steps
{
    [Binding]
    public class NuGetOrgSteps
    {
        [Given(@"I have opened a nuget home page")]
        public void GivenIHaveOpenedANugetHomePage()
        {
            Browser.Do(NuGetOrgPage.Navigate);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string text)
        {
            Browser.On<NuGetOrgPage>().Search(text);
        }

        [Then(@"I can see (.*) packages")]
        public void ThenICanSeePackages(int count)
        {
            Browser.On<NuGetOrgPage>().AssertSearchResultsCountIs(count);
        }

        [Then(@"it should use custom web element resolver for configured page control")]
        public void ThenItShouldUseCustomWebElementResolverForConfiguredPageControl()
        {
            Browser.On<NuGetOrgPage>().AssertCustomWebElementResolverExecuted();
        }
    }
}