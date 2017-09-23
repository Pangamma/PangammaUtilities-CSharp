using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangamma.Utilities
{
    /// <summary>
    /// Just a proof of concept for type conversions. This shows you can use custom classes
    /// for TypeConverter based conversions. Code isn't robust or complete. It is purely
    /// proof of concept.
    /// </summary>
    [TypeConverter(typeof(TypeConvertableObjectTypeConverter))]
    public class TypeConvertableObject
    {
        public string Name { get; set; }
        public int? Age { get; set; }
    }

    public class TypeConvertableObjectTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(TypeConvertableObject))
            {
                return true;
            }
            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type dt)//destination type = dt
        {
            return (dt == typeof(string) || dt == typeof(TypeConvertableObject));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                string[] s = ((string)value).Split(new char[] { ';' });
                return new TypeConvertableObject()
                {
                    Name = s[0],
                    Age = int.Parse(s[1])
                };
            }
            return null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value.GetType() == typeof(TypeConvertableObject))
            {
                var o = (TypeConvertableObject)value;
                if (destinationType == typeof(TypeConvertableObject))
                {
                    return o;
                }
                else if (typeof(string) == destinationType)
                {
                    return o.Name + ";" + o.Age;
                }
            }
            return null;
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return true;
        }
    }
}
