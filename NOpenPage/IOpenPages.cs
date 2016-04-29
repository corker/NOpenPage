namespace NOpenPage
{
    /// <summary>
    ///     An interface <see cref="Page" /> class has to implement to be able to open a page using
    ///     <see cref="Browser.Open{T}" /> method.
    /// </summary>
    public interface IOpenPages
    {
        /// <summary>
        ///     Opens a page in a browser
        /// </summary>
        void Open();
    }
}