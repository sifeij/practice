namespace System
{
    public static class StringExtensions
    {
        public static int WordCount(this String str) =>
            str.Split(new char[] { ' ', '.', '?' }, 
                      StringSplitOptions.RemoveEmptyEntries).Length;
    }
}