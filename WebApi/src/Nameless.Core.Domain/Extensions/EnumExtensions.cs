using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Nameless.Core.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            var name = enumType.GetType()
                .GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();

            if (name == null) throw new ArgumentNullException(name);

            return name;
        }

        public static string GetEnumDisplayDescription(this Enum enumType)
        {
            var description = enumType.GetType()
                .GetMember(enumType.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetDescription();

            if (description == null) throw new ArgumentNullException(description);

            return description;
        }

        public static T GetEnumFromDescription<T>(string description) where T : Enum
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var itemEnum = (T)item;

                if (itemEnum != null)
                {
                    var itemEnumDescription = itemEnum.GetEnumDisplayDescription();
                    if (itemEnumDescription == description)
                    {
                        return itemEnum;
                    }
                }
            }

            throw new InvalidOperationException();
        }

        public static bool IsAnEnumDisplayDescriptions<T>(string description) where T : Enum
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                var itemEnum = (T)item;

                if (itemEnum != null)
                {
                    var itemEnumDescription = itemEnum.GetEnumDisplayDescription();
                    if (itemEnumDescription == description)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
