namespace NOpenPage
{
    /// <summary>
    ///     An interface that <see cref="Page" /> class has to implement to open a page using <see cref="Browser.Open{T}" />
    ///     method.
    /// </summary>
    public interface IOpenPages
    {
        void Open();
    }
}