using System;
using FakeItEasy;
using FluentAssertions;
using NOpenPage.Configuration;
using NSpec;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace NOpenPage.Specs
{
    [Tag("describe_BrowserContext")]
    public class describe_BrowserContextBuilder : nspec
    {
        private static readonly IWebDriver ExpectedWebDriver = A.Fake<IWebDriver>(x => x.Strict());
        private BrowserContext _browserContext;
        private WebDriverResolver _driverResolver;

        private BrowserContextBuilder _target;
        private IWebDriver _webDriver;

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
                    it["should return expected web driver"] = () =>
                    {
                        _webDriver.Should().BeSameAs(ExpectedWebDriver);
                    };
                };
            };
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
                _helper.Run<describe_BrowserContextBuilder>();
            }
        }
    }
}