using System.Text;

namespace System
{
    public static class ExceptionExtensions
    {
        public static string FullMessage(this Exception ex)
        {
            var strBuilder = new StringBuilder();
            while (ex != null)
            {
                strBuilder.AppendFormat($"{ex.Message}{Environment.NewLine}");
                ex = ex.InnerException;
            }
            return strBuilder.ToString();
        }
    }
}