using System;
using System.Collections.Generic;

namespace NOpenPage.Configuration
{
    public class WebElementResolverRegistry : IProvideWebElementResolvers
    {
        private readonly Dictionary<Type, WebElementResolver> _resolvers;

        private WebElementResolver _default;

        public WebElementResolverRegistry()
        {
            _resolvers = new Dictionary<Type, WebElementResolver>();
            _default = (context, provider) => provider(context);
        }

        public void SetDefault(WebElementResolver resolver)
        {
            _default = resolver;
        }

        public void Add<T>(WebElementResolver resolver) where T : PageControl
        {
            var type = typeof(T);
            if (_resolvers.ContainsKey(type))
            {
                string message = $"Can't register WebElementResolver. Type {type.Name} is already regitered.";
                throw new InvalidOperationException(message);
            }
            _resolvers.Add(type, resolver);
        }

        public WebElementResolver Get(Type type)
        {
            WebElementResolver result;
            return _resolvers.TryGetValue(type, out result) ? result : _default;
        }
    }
}