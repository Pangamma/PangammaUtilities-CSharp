using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestExamples.models;
using System.ComponentModel;
using PangammaUtilities.Extensions;

namespace TestExamples
{
    [TestClass]
    public class TypeConvertTests
    {
        [TestMethod]
        public void SmokeTest()
        {
            var v = "Henry;9".To<TypeConvertableObject>();
            Assert.AreEqual("Henry", v.Name);
            Assert.AreEqual(9, v.Age);

            TypeConvertableObject obj = new TypeConvertableObject()
            {
                Name = "BOB",
                Age = 23
            };

            var converter = TypeDescriptor.GetConverter(typeof(TypeConvertableObject));
            string s = "";
            if(converter.CanConvertTo(typeof(string))){
                s = (string) converter.ConvertTo(obj, typeof(string));
            }

            var converted = (TypeConvertableObject)(converter.ConvertFromInvariantString(s));
            converted = (TypeConvertableObject)(converter.ConvertFromString(s));
        }
    }
}
