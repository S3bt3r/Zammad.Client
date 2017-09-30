using System;

namespace Zammad.Client.Core.Internal
{
    public static class TypeUtility
    {
        public static void CopyProperties<T>(T from, T to)
        {
            ArgumentCheck.ThrowIfNull(from, nameof(from));
            ArgumentCheck.ThrowIfNull(to, nameof(to));

            var fromType = from.GetType();
            var toType = to.GetType();

            if (toType.IsSubclassOf(fromType) == false)
            {
                throw new ArgumentException($"The type of the argument \"{nameof(to)}\" must be derived from the type of the argument \"{nameof(from)}\".");
            }

            foreach (var property in fromType.GetProperties())
            {
                if (property.CanRead)
                {
                    var value = property.GetValue(from);
                    if (property.CanWrite)
                    {
                        property.SetValue(to, value);
                    }
                }
            }
        }
    }
}
