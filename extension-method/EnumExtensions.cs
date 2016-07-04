using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum enumValue)
        {
            return Enum.GetName(enumValue.GetType(), enumValue);
        }

        public static string GetDescription(this Enum enumValue)
        {
            DisplayAttribute attribute 
                = enumValue.GetType()
                       .GetField(enumValue.ToString())
                       .GetCustomAttributes(typeof(DisplayAttribute ), false)
                       .SingleOrDefault() as DisplayAttribute ;
            return attribute == null ? enumValue.ToString() : attribute.Description;
        }
    }
}