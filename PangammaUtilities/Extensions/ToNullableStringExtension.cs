using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangammaUtilities.Extensions
{
    /// <summary>
    /// Developed by Taylor Love
    /// </summary>
    public static class ToNullableStringExtension
    {
        /// <summary>
        /// <para>More convenient than using T.TryParse(string, out T). 
        /// Works with primitive types, structs, and enums.
        /// Tries to parse the string to an instance of the type specified.
        /// If the input cannot be parsed, null will be returned.
        /// </para>
        /// <para>
        /// If the value of the caller is null, null will be returned.
        /// So if you have "string s = null;" and then you try "s.ToNullable...",
        /// null will be returned. No null exception will be thrown. 
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_self"></param>
        /// <returns></returns>
        public static T? ToNullable<T>(this string p_self) where T : struct
        {
            if (!string.IsNullOrEmpty(p_self))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (converter.IsValid(p_self))
                {
                    return (T)converter.ConvertFromString(p_self);
                }
                else if(typeof(T).IsEnum)
                {
                    T t; // tries to parse using the int value.
                    if (Enum.TryParse<T>(p_self, out t)) return t;   
                }
            }

            return null;
        }

        /// <summary>
        /// <para>
        /// Null is returned if the enum cannot be determined by the string
        /// representation of the Enum name or value.
        /// Example: "0" works, so does "DefaultEnumLabel".
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_self"></param>
        /// <returns></returns>
        public static T? ToNullableEnum<T>(this string p_self) where T : struct
        {
            if (!string.IsNullOrEmpty(p_self))
            {
                if (typeof(T).IsEnum)
                {
                    T t;
                    if (Enum.TryParse<T>(p_self, out t))    // tries to parse using the int value.
                    {
                        return t;
                    }
                }
                else
                {
                    throw new ArgumentException("Type T must be of Enum Type.");
                }
            }

            return null;
        }

        /// <summary>
        /// Attempts to parse the string into the specified type. The type can
        /// be nullable, or it can be a primitive type. Or it must derive from
        /// TypeConverter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this string value)
        {
            T result = default(T);
            if (!string.IsNullOrEmpty(value))
            {
                // This will throw a null exception if no converter exists for this.
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (converter.IsValid(value))
                {
                    result = (T)converter.ConvertFromString(value);
                }
            }
            return result;
        }
    }
}