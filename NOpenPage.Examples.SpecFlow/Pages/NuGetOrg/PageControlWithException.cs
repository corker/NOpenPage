using System;
using NUnit.Framework;
using OpenQA.Selenium;

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
                Assert.IsTrue(false, "Custom element resolver was not executed.");
            }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual("ResolvedWithException", ex.Message, "Unexpected exception thrown.");
            }
        }
    }
}