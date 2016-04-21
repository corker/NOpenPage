using System;
using FakeItEasy;
using NOpenPage.Configuration;
using NSpec;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NOpenPage.Specs.Configuration
{
    public class describe_BrowserContext : nspec
    {
        public void when_created_without_web_driver_resolver()
        {
            before = () =>
            {
                _waitFactory = A.Fake<Func<ISearchContext, IWait<ISearchContext>>>();
                _action = () => _target = new BrowserContext(_driverResolver, _waitFactory);
            };

            it["should throw"] = () => { expect<ArgumentNullException>(_action); };
        }

        public void when_created_without_web_element_factory()
        {
            before = () =>
            {
                _driverResolver = A.Fake<Func<IWebDriver>>();
                _action = () => _target = new BrowserContext(_driverResolver, _waitFactory);
            };

            it["should throw"] = () => { expect<ArgumentNullException>(_action); };
        }

        public void when_created()
        {
            before = () =>
            {
                _waitFactory = A.Fake<Func<ISearchContext, IWait<ISearchContext>>>(x => x.Strict());
                _driverResolver = A.Fake<Func<IWebDriver>>(x => x.Strict());
            };

            act = () => { _target = new BrowserContext(_driverResolver, _waitFactory); };

            context["when resolve web driver"] = () =>
            {
                before = () => _action = () => _webDriver = _target.ResolveWebDriver();

                context["when resolver returns null"] = () =>
                {
                    before = () => { A.CallTo(() => _driverResolver()).Returns(null); };

                    it["should throw"] = () => { expect<ArgumentNullException>(_action); };
                };

                context["when resolver returns value"] = () =>
                {
                    before = () => { A.CallTo(() => _driverResolver()).Returns(ExpectedWebDriver); };
                    act = () => _action();

                    it["should return the same value"] = () => { _webDriver.should_be_same(ExpectedWebDriver); };
                };
            };

            context["when create web element resolver"] = () =>
            {
                before = () => _action = () => _webElementResolver = _target.CreateWebElementResolver(_searchContext);

                context["when receives no search context"] =
                    () => { it["should throw"] = () => { expect<ArgumentNullException>(_action); }; };

                context["when receives search context"] = () =>
                {
                    before = () => { _searchContext = A.Fake<ISearchContext>(); };
                    act = () => _action();

                    context["when wait factory returns null"] = () =>
                    {
                        before = () => { A.CallTo(() => _waitFactory(A<ISearchContext>._)).Returns(null); };

                        it["should throw"] = () =>
                        {
                            var provider = A.Fake<Func<ISearchContext, IWebElement>>();
                            Action action = () => { _webElementResolver.Resolve(provider); };
                            expect<ArgumentNullException>(action);
                        };
                    };

                    context["when wait factory returns value"] = () =>
                    {
                        before =
                            () => { A.CallTo(() => _waitFactory(A<ISearchContext>._)).Returns(ExpectedWebElement); };

                        it["should use wait factory"] = () =>
                        {
                            var provider = A.Fake<Func<ISearchContext, IWebElement>>();
                            _webElementResolver.Resolve(provider);
                            A.CallTo(() => _waitFactory(_searchContext)).MustHaveHappened(Repeated.Exactly.Once);
                        };
                    };
                };
            };
        }

        private BrowserContext _target;
        private Func<IWebDriver> _driverResolver;
        private Func<ISearchContext, IWait<ISearchContext>> _waitFactory;
        private ISearchContext _searchContext;
        private IWebDriver _webDriver;
        private IResolveWebElements _webElementResolver;
        private Action _action;

        private static readonly IWebDriver ExpectedWebDriver = A.Fake<IWebDriver>();
        private static readonly IWait<ISearchContext> ExpectedWebElement = A.Fake<IWait<ISearchContext>>();
    }
}