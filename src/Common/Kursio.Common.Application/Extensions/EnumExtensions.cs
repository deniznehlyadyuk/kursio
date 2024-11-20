using System.ComponentModel;
using System.Reflection;
using Kursio.Common.Application.Exceptions;

namespace Kursio.Common.Application.Extensions;

public static class EnumExtensions
{
    public static TEnum GetValueFromDescription<TEnum>(this string description)
        where TEnum : Enum
    {
        foreach (FieldInfo field in typeof(TEnum).GetFields())
        {
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null && attribute.Description == description)
            {
                return (TEnum)field.GetValue(null);
            }
        }

        throw new KursioException($"Enum için Description '{nameof(description)}' bulunamadı.");
    }
}
