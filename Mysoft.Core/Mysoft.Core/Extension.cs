using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mysoft.Core
{
    public static class Extensions
    {
        #region 反射扩展
        /// <summary>
        /// 快速获取属性值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object FastGetValue(this PropertyInfo property, object instance)
        {
            var getter = DynamicCalls.GetPropertyGetter(property);
            return getter(instance);
        }

        /// <summary>
        /// 快速给属性赋值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        public static void FastSetValue(this PropertyInfo property, object instance, object value)
        {
            var setter = DynamicCalls.GetPropertySetter(property);
            setter(instance, value);
        }

        #endregion

        #region 类型转换
        /// <summary>
        /// 将object类型转化为基础类型T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this object obj)
        {
            return TypeConvertor.To<T>(obj);
        }

        /// <summary>
        ///  将object类型转化为制定类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ToType(this object obj, Type type)
        {
            if (obj.GetType() == type)
            {
                return obj;
            }
            else if (type == typeof(String))
            {
                return obj.ToString();
            }
            else
            {
                var value = System.Convert.ChangeType(obj, type);
                return value;
            }
        }

        #endregion

        #region 比较
        /// <summary>
        /// 判断字符串是否相等
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="com"></param>
        /// <returns></returns>
        public static bool IsSame(this string str1, string str2, StringComparison com = StringComparison.OrdinalIgnoreCase)
        {
            return str1.Equals(str2, com);
        }

        #endregion  

    }
}
