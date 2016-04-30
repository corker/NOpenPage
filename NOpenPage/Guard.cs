using System;

namespace NOpenPage
{
    public static class Guard
    {
        public static void NotNull<T>(string paramName, T value)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}