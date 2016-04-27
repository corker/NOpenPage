using System;
using NOpenPage.Configuration;

namespace NOpenPage
{
    /// <summary>
    ///     A page context interface provides an access to <see cref="IPageControlContextImpl" /> for
    ///     <see cref="PageControl" /> class.
    /// </summary>
    public interface IPageControlContext
    {
        IPageControlContextImpl GetImpl(WebElementProvider provider, Type type);
    }
}