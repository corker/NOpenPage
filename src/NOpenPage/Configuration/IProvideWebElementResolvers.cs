using System;

namespace NOpenPage.Configuration
{
    public interface IProvideWebElementResolvers
    {
        WebElementResolver Get(Type type);
    }
}