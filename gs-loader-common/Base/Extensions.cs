using System;

namespace gs_loader_common.Base
{
    public static class Extensions
    {
        public static bool Contains(this string[] array, string value)
        {
            foreach (var item in array)
                if (item.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }
    }
}
