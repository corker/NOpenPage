using System;
using NSpec;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace NOpenPage.Specs
{
    public class describe_Browser : nspec
    {
        public void when_on_test_page()
        {
            act = () => Browser.On<TestPage>();
            it["should throw"] = expect<InvalidOperationException>();
        }

        public void when_configure_with_null_action()
        {
            act = () => Browser.Configure(null);
            it["should throw"] = expect<ArgumentNullException>();
        }

        public void when_configure_with_null_web_driver_provider()
        {
            act = () => Browser.Configure(c => c.WithWebDriverResolver(null));
            it["should throw"] = expect<ArgumentNullException>();
        }

        public void when_configure_with_generic_null_web_element_resolver()
        {
            act = () => Browser.Configure(c => c.WithWebElementResolver(null));
            it["should throw"] = expect<ArgumentNullException>();
        }

        public void when_configure_with_null_web_element_resolver()
        {
            act = () => Browser.Configure(c => c.WithWebElementResolver<TestPageControl>(null));
            it["should throw"] = expect<ArgumentNullException>();
        }

        public void when_configure_with_the_same_web_element_resolver()
        {
            act = () => Browser.Configure(config =>
                config
                    .WithWebElementResolver<TestPageControl>((s, p) => null)
                    .WithWebElementResolver<TestPageControl>((s, p) => null));
            it["should throw"] = expect<InvalidOperationException>();
        }

        private class TestPageControl : PageControl
        {
            public TestPageControl() : base((IWebElement) null, null)
            {
            }
        }

        private class TestPage : Page
        {
            public TestPage(IPageContext context) : base(context)
            {
            }
        }

        public class Run
        {
            public Run(ITestOutputHelper helper)
            {
                _helper = helper;
            }

            private readonly ITestOutputHelper _helper;

            [Fact]
            public void Specs()
            {
                _helper.Run<describe_Browser>();
            }
        }
    }
}