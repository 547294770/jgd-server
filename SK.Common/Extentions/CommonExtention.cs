using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common.Extentions
{
    public static class CommonExtention
    {
        public static T Fill<T>(this NameValueCollection form,T model) where T : new()
        {
            var obj = model;
            var type = model.GetType();

            var properties = type.GetProperties();
            foreach (var pro in properties)
            {
                if (form.AllKeys.Contains(pro.Name))
                {
                    if (pro.PropertyType.IsEnum)
                    {
                        object enumOjb = Enum.Parse(pro.PropertyType, form[pro.Name]);
                        pro.SetValue(obj, enumOjb);
                    }
                    else
                    {
                        pro.SetValue(obj, Convert.ChangeType(form[pro.Name], pro.PropertyType), null);
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 获取描述属性的值
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum em)
        {
            var field = em.GetType().GetField(em.ToString());
            if (field == null) return "null";

            var attrs = field.GetCustomAttributesData();
            var description = "";

            foreach (var item in attrs)
            {
                if (item.AttributeType == typeof(DescriptionAttribute))
                {
                    if (item.ConstructorArguments.Count > 0) {
                        description = (string)item.ConstructorArguments[0].Value;
                    }
                    break;
                }
            }

            return description;
        }

        public static string GetDescription<T>(this object em) where T : class
        {
            var field = typeof(T).GetField(em.ToString());
            if (field == null) return "null";

            var attrs = field.GetCustomAttributesData();
            var description = "";

            foreach (var item in attrs)
            {
                if (item.AttributeType == typeof(DescriptionAttribute))
                {
                    if (item.ConstructorArguments.Count > 0)
                    {
                        description = (string)item.ConstructorArguments[0].Value;
                    }
                    break;
                }
            }

            return description;
        }
        
    }
}
