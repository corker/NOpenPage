using System;
using OpenQA.Selenium;
using Xunit;

namespace NOpenPage.Examples.SpecFlow.Pages.NuGetOrg
{
    public class PageControlWithException : PageControl
    {
        public PageControlWithException(IPageControlContext context) : base(By.TagName("form"), context)
        {
        }

        public void AssertCustomWebElementResolverExecuted()
        {
            try
            {
                Element.Click();
                Assert.True(false, "Custom element resolver was not executed.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.Equal("ResolvedWithException", ex.Message);
            }
        }
    }
}