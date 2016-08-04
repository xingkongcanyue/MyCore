using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class TypeConvertor
    {
        public static int ToInt(object obj)
        {
            if (obj == null || obj==DBNull.Value)
                return 0;
            return Convert.ToInt32(obj);
        }

        public static Guid ToGuid(object obj)
        {
            if(obj==null || obj==DBNull.Value)
            if (obj is Guid)
                return (Guid)obj;
            Guid result;
            Guid.TryParse(obj.ToString(),out result);
            return result;
        }

        public static string ToString(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToString(obj);
        }


        public static T To<T>(object obj)
        {
            if (obj == null || DBNull.Value.Equals(obj))
            {
                return default(T);
            }
            if (obj is T)
            {
                return (T)obj;
            }
            var type = typeof(T);
            if (type == typeof(string))
                return (T)(object)obj.ToString();
            return (T)System.Convert.ChangeType(obj, typeof(T));
        }
    }
}
