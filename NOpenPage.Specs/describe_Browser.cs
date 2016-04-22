using System;

namespace NOpenPage.Specs
{
    public class describe_Browser : nspec
    {
        public void when_on_test_page()
        {
            before = () => Browser.On<TestPage>();
            it["should throw"] = expect<InvalidOperationException>();
        }

        private class TestPage : Page
        {
            public TestPage(IPageContext context) : base(context)
            {
            }
        }
    }
}