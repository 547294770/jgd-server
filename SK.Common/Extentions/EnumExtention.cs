using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Common.Extentions
{
    public static class EnumExtention
    {
        //public static Dictionary<string, string> GetEnums(this Enum em)
        //{
        //    //em.ge

        //    string[] names = Enum.GetNames(em.GetType());
        //    return new Dictionary<string, string>();
        //}

        public static T ToEnum<T>(this string value)
        {
            try
            {
                return  (T) Enum.Parse(typeof(T), value);
            }
            catch 
            {
            }

            return default(T);
        }
    }
}
