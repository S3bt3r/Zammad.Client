using System;
using System.Collections.Generic;
using System.Linq;

namespace Zammad.Client.Core
{
    internal static class ArgumentCheck
    {
        internal static void ThrowIfNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void ThrowIfNullOrEmpty<T>(IEnumerable<T> value, string paramName)
        {
            ThrowIfNull(value, paramName);

            if (value.Count() == 0)
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

    }
}