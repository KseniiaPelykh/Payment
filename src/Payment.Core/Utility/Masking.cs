using System.Reflection.Metadata.Ecma335;

namespace Payment.Core.Utility
{
    public static class Masking
    {
        public static string GetMask(string value) =>
            value.Substring(value.Length - 4).PadLeft(value.Length, '*');
    }
}
