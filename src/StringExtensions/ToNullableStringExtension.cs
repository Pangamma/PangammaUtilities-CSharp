﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangamma.Utilities
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
        /// <author>Contributed by Taylor Love (Pangamma)</author>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_self"></param>
        /// <returns></returns>
        public static T? ToNullable<T>(this string p_self) where T : struct
        {
            if (!string.IsNullOrEmpty(p_self))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (converter.IsValid(p_self)) return (T)converter.ConvertFromInvariantString(p_self);
                if (typeof(T).IsEnum) { T t; if (Enum.TryParse<T>(p_self, out t)) return t;}
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
        
        
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            return text.ReplaceFirst(search, replace, StringComparison.InvariantCulture);
        }

        public static string ReplaceFirst(this string text, string search, string replace, StringComparison comparisonType)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        // Allows case insensitive searching
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }

        // Allows case insensitive searching
        public static string Replace(this string source, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (source.Length == 0 || oldValue.Length == 0)
                return source;

            var result = new System.Text.StringBuilder();
            int startingPos = 0;
            int nextMatch;
            while ((nextMatch = source.IndexOf(oldValue, startingPos, comparisonType)) > -1)
            {
                result.Append(source, startingPos, nextMatch - startingPos);
                result.Append(newValue);
                startingPos = nextMatch + oldValue.Length;
            }
            result.Append(source, startingPos, source.Length - startingPos);

            return result.ToString();
        }
    }
}
