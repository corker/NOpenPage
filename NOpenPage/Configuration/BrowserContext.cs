using System;
using OpenQA.Selenium;

namespace NOpenPage.Configuration
{
    public class BrowserContext
    {
        private readonly WebDriverResolver _driverResolver;
        private readonly IProvideWebElementResolvers _elementResolvers;

        public BrowserContext(WebDriverResolver driverResolver, IProvideWebElementResolvers elementResolvers)
        {
            Guard.NotNull(nameof(driverResolver), driverResolver);
            Guard.NotNull(nameof(elementResolvers), elementResolvers);

            _driverResolver = driverResolver;
            _elementResolvers = elementResolvers;
        }


        public IPageContext CreatePageContext()
        {
            return new PageContext(_driverResolver, _elementResolvers);
        }
    }
}