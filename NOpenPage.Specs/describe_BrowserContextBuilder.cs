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
            act = () => _webDriver = _browserContext.ResolveWebDriver();
            it["should throw"] = expect<InvalidOperationException>();

            context["with custom web driver resolver"] = () =>
            {
                before = () => { _target.WithWebDriverResolver(_webDriverReslover); };

                context["when custom web driver resolver is null"] = () =>
                {
                    beforeAll = () => _webDriverReslover = null;
                    it["should throw"] = expect<ArgumentNullException>();
                };

                context["when web driver resolved as null"] = () =>
                {
                    beforeAll = () => _webDriverReslover = () => null;
                    it["should throw"] = expect<InvalidOperationException>();
                };

                context["when web driver resolved"] = () =>
                {
                    beforeAll = () => _webDriverReslover = () => ExpectedWebDriver;
                    it["should return expected web driver"] = () => { _webDriver.should_be_same(ExpectedWebDriver); };

                    context["when create web element resolver"] = () =>
                    {
                        act = () => _webElementResolver = _browserContext.CreateWebElementResolver(_searchContext);

                        context["with search context as null"] = () =>
                        {
                            before = () => _searchContext = null;
                            it["should throw"] = expect<ArgumentNullException>();
                        };

                        context["when resolves a web element"] = () =>
                        {
                            before = () =>
                            {
                                _searchContext = ExpectedSearchContext;
                                _webElementProvider = c => ExpectedWebElement;
                            };
                            act = () => _webElement = _webElementResolver.Resolve(_webElementProvider);

                            it["should return expected web element"] =
                                () => { _webElement.should_be_same(ExpectedWebElement); };

                            context["with custom factory as null"] =
                                () =>
                                {
                                    before = () => { _target.WithWebElementResolver(null); };
                                    it["should throw"] = expect<ArgumentNullException>();
                                };

                            context["with custom factory"] =
                                () =>
                                {
                                    before = () => { _target.WithWebElementResolver((c, p) => p(c)); };
                                    it["should return expected web element"] =
                                        () => { _webElement.should_be_same(ExpectedWebElement); };
                                };

                            context["with web element provider as null"] = () =>
                            {
                                before = () => _webElementProvider = null;
                                it["should throw"] = expect<ArgumentNullException>();
                            };

                            context["with web element resolved as null"] =
                                () =>
                                {
                                    before = () => _webElementProvider = c => null;
                                    it["should throw"] = expect<InvalidOperationException>();
                                };

                        };
                    };
                };
            };
        }

        private BrowserContextBuilder _target;
        private BrowserContext _browserContext;
        private Func<IWebDriver> _webDriverReslover;
        private IWebDriver _webDriver;
        private ISearchContext _searchContext;
        private IResolveWebElements _webElementResolver;
        private Func<ISearchContext, IWebElement> _webElementProvider;
        private IWebElement _webElement;

        private static readonly IWebDriver ExpectedWebDriver = A.Fake<IWebDriver>(x => x.Strict());
        private static readonly ISearchContext ExpectedSearchContext = A.Fake<ISearchContext>(x => x.Strict());
        private static readonly IWebElement ExpectedWebElement = A.Fake<IWebElement>();
    }
}