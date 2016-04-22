using System;
using FakeItEasy;
using NOpenPage.Configuration;
using NSpec;
using OpenQA.Selenium;

namespace NOpenPage.Specs.UnitTests
{
    public class describe_BrowserContext : nspec
    {
        public void when_created_without_web_driver_resolver()
        {
            before = () =>
            {
                _elementResolver =
                    A.Fake<Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement>>(x => x.Strict());
                _action = () => _target = new BrowserContext(_driverResolver, _elementResolver);
            };

            it["should throw"] = () => { expect<ArgumentNullException>(_action); };
        }

        public void when_created_without_web_element_factory()
        {
            before = () =>
            {
                _driverResolver = A.Fake<Func<IWebDriver>>(x => x.Strict());
                _action = () => _target = new BrowserContext(_driverResolver, _elementResolver);
            };

            it["should throw"] = () => { expect<ArgumentNullException>(_action); };
        }

        public void when_created()
        {
            before = () =>
            {
                _elementResolver =
                    A.Fake<Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement>>(x => x.Strict());
                _driverResolver = A.Fake<Func<IWebDriver>>(x => x.Strict());
            };

            act = () => { _target = new BrowserContext(_driverResolver, _elementResolver); };

            context["when resolve web driver"] = () =>
            {
                before = () => _action = () => _webDriver = _target.ResolveWebDriver();

                context["when resolver returns null"] = () =>
                {
                    before = () => { A.CallTo(() => _driverResolver()).Returns(null); };

                    it["should throw"] = () => { expect<ArgumentNullException>(_action); };
                };

                context["when resolver returns driver"] = () =>
                {
                    before = () => { A.CallTo(() => _driverResolver()).Returns(ExpectedWebDriver); };
                    act = () => _action();

                    it["should return the same driver"] = () => { _webDriver.should_be_same(ExpectedWebDriver); };
                };
            };

            context["when create web element resolver"] = () =>
            {
                before = () => _action = () => _webElementResolver = _target.CreateWebElementResolver(_searchContext);

                context["when no search context provided"] =
                    () =>
                    {
                        before = () => _searchContext = null;

                        it["should throw"] = () => { expect<ArgumentNullException>(_action); };
                    };

                context["when search context provided"] = () =>
                {
                    before = () => { _searchContext = A.Fake<ISearchContext>(x => x.Strict()); };
                    act = () => _action();

                    context["when resolver returns null"] = () =>
                    {
                        before = () => { _elementResolver = (c, p) => null; };

                        it["should throw"] = () =>
                        {
                            Func<ISearchContext, IWebElement> provider = c => null;
                            Action action = () => { _webElementResolver.Resolve(provider); };
                            expect<ArgumentNullException>(action);
                        };
                    };

                    context["when resolver returns element"] = () =>
                    {
                        before =
                            () =>
                            {
                                _webElementProvider = c => ExpectedWebElement;
                                _elementResolver = (c, p) => p(c);
                            };

                        act = () => _webElement = _webElementResolver.Resolve(_webElementProvider);

                        it["should return the same element"] =
                            () => { _webElement.should_be_same(ExpectedWebElement); };
                    };
                };
            };
        }

        private BrowserContext _target;
        private Func<IWebDriver> _driverResolver;
        private Func<ISearchContext, Func<ISearchContext, IWebElement>, IWebElement> _elementResolver;
        private ISearchContext _searchContext;
        private IWebDriver _webDriver;
        private IResolveWebElements _webElementResolver;
        private Action _action;

        private static readonly IWebDriver ExpectedWebDriver = A.Fake<IWebDriver>(x => x.Strict());
        private static readonly IWebElement ExpectedWebElement = A.Fake<IWebElement>(x => x.Strict());
        private IWebElement _webElement;
        private Func<ISearchContext, IWebElement> _webElementProvider;
    }
}