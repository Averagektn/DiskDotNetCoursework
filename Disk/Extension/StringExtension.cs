namespace Disk.Extension
{
    public static class StringExtension
    {
        public static string ToUpperFirstLetter(this string value) => value[..1].ToUpper() + value[1..];
        public static bool IsEmpty(this string value) => value.Length == 0;
    }
}
