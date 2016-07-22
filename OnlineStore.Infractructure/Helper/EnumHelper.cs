using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infractructure.Helper
{
    public static class EnumHelper
    {
        public static string GetDescriptionFromEnum(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static T GetEnumFromDescription<T>(string description)
        {
            return (T)typeof(T)
                .GetFields()
                .First(f => f.GetCustomAttributes<DescriptionAttribute>()
                             .Any(a => a.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                )
                .GetValue(null);
        }
    }
}
