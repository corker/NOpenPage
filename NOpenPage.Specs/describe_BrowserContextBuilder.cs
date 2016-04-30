using System;
using FakeItEasy;
using NOpenPage.Configuration;
using NSpec;
using OpenQA.Selenium;

namespace NOpenPage.Specs
{
    [Tag("describe_BrowserContext")]
    public class describe_BrowserContextBuilder : nspec
    {
        public void before_each()
        {
            _target = new BrowserContextBuilder();
        }

        public void act_each()
        {
            _browserContext = _target.Build();
        }

        public void when_resolve_web_driver()
        {
            act = () => _webDriver = _browserContext.CreatePageContext().Driver;
            it["should throw"] = expect<InvalidOperationException>();

            context["with custom web driver resolver"] = () =>
            {
                before = () => { _target.WithWebDriverResolver(_driverResolver); };

                context["when custom web driver resolver is null"] = () =>
                {
                    beforeAll = () => _driverResolver = null;
                    it["should throw"] = expect<ArgumentNullException>();
                };

                context["when web driver resolved as null"] = () =>
                {
                    beforeAll = () => _driverResolver = () => null;
                    it["should throw"] = expect<InvalidOperationException>();
                };

                context["when web driver resolved"] = () =>
                {
                    beforeAll = () => _driverResolver = () => ExpectedWebDriver;
                    it["should return expected web driver"] = () => { _webDriver.should_be_same(ExpectedWebDriver); };
                };
            };
        }

        private BrowserContextBuilder _target;
        private BrowserContext _browserContext;
        private WebDriverResolver _driverResolver;
        private IWebDriver _webDriver;

        private static readonly IWebDriver ExpectedWebDriver = A.Fake<IWebDriver>(x => x.Strict());
    }
}