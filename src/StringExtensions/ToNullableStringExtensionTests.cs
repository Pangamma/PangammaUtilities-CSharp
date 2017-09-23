using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Pangamma.Utilities
{
    [TestClass]
    public class ToNullableStringExtensionTests
    {
        [TestMethod]
        public void ParsesIntegerEvenIfExtraSpaces()
        {
            // Note the extra space right here. Normally you'd check for
            // null /empty strings before parsing.
            string s = " 9 ";
            int? n = s.ToNullable<int>(); // --> 9
            Assert.AreEqual(9, n);
        }

        [TestMethod]
        public void ParsesValidInputOkay()
        {
            string s = "true";  // this will parse to true
            bool? b = s.ToNullable<bool>(); // --> true
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void ParsesInvalidInputOkay()
        {
            string s = "this will not parse";
            bool? b = s.ToNullable<bool>(); // --> null
            Assert.AreEqual(null, b);
        }

        [TestMethod]
        public void NoNullExceptions()
        {
            string s = null;
            int n = s.ToNullable<int>() ?? 2;   // --> null --> 2
            Assert.AreEqual(2, n);
        }

        [TestMethod]
        public void EnumParsing_WithoutExtensions()
        {
            // Note that you can't parse directly to the property. You need
            // to use temporary variables. Consider this method an example of
            // steps you'd normally take when parsing enums.
            RandomModelObject obj = new RandomModelObject();
            AnimalTypeEnum tmpAnimalEnum = AnimalTypeEnum.Bird;
            Enum.TryParse<AnimalTypeEnum>("Cat", out tmpAnimalEnum);
            obj.NonNullableEnum = tmpAnimalEnum;
            Assert.AreEqual(AnimalTypeEnum.Cat, obj.NonNullableEnum, "Should have parsed to Cat type. ('Cat')");

            
            Enum.TryParse<AnimalTypeEnum>("3", out tmpAnimalEnum);  // we can also parse with values.
            obj.NonNullableEnum = tmpAnimalEnum;
            Assert.AreEqual(AnimalTypeEnum.Dog, obj.NonNullableEnum, "Should have parsed to Dog type.('3')");


            // For nullable values this can get annoying. A one-liner would be nice.
            AnimalTypeEnum tmpAnimalEnum2;
            if (Enum.TryParse<AnimalTypeEnum>("Dog", out tmpAnimalEnum))
            {
                obj.NullableEnum = tmpAnimalEnum;
            }
            else
            {
                obj.NullableEnum = null;
            }
            Assert.AreEqual(AnimalTypeEnum.Dog, obj.NullableEnum,"Should have parsed to nullable Dog type.");
        }

        [TestMethod]
        public void EnumParsing_WithExtensions()
        {
            // Thanks to the one-liner, you can parse right to the property.
            RandomModelObject obj = new RandomModelObject()
            {
                NonNullableEnum = "Cat".ToNullable<AnimalTypeEnum>() ?? AnimalTypeEnum.Bird,
                NullableEnum = "3".ToNullable<AnimalTypeEnum>() // Should be the dog type.
            };
            Assert.AreEqual(AnimalTypeEnum.Cat, obj.NonNullableEnum, "Should be parsed to Cat type. ('Cat')");
            Assert.AreEqual(AnimalTypeEnum.Dog, obj.NullableEnum, "Should be parsed to Dog type.('Dog')");

            // These will fail to parse and will return null.
            Assert.IsNull("This won't parse".ToNullable<AnimalTypeEnum>(), "Note that bad inputs from forms will be handled just fine.");
            Assert.IsNull("".ToNullable<AnimalTypeEnum>(), "Note that bad inputs from forms will be handled just fine.");
        }

        [TestMethod]
        public void IsFasterThanTryParse_GoodInput()
        {
            string someDecimalValue = "1024.30";
            var sw = new Stopwatch();
            int numIterations = 10000;

            //-----------------------------------------------------------------

            sw.Start();
            for (int i = 0; i < numIterations; i++)
            {
                decimal? targetType = someDecimalValue.ToNullable<decimal>();
            }
            sw.Stop();
            long elapsedForToNullable = sw.ElapsedTicks;

            //-----------------------------------------------------------------

            sw.Start();
            for (int i = 0; i < numIterations; i++)
            {
                decimal? targetType = null;
                decimal tmp = 0;
                if (decimal.TryParse(someDecimalValue, out tmp))
                {
                    targetType = tmp;
                }
            }
            sw.Stop();
            long elapsedForTryParse = sw.ElapsedTicks;

            //-----------------------------------------------------------------

            sw.Reset();
            Console.WriteLine("ToNullable: " + elapsedForToNullable + " ticks.");
            Console.WriteLine("TryParse: " + elapsedForTryParse + " ticks.");
            Assert.IsTrue(elapsedForToNullable < elapsedForTryParse,"The ToNullable string extension should be faster.");
        }
        
        [TestMethod]
        public void IsFasterThanTryParse_BadInput()
        {
            string someDecimalValue = "lol 1024.30";
            var sw = new Stopwatch();
            int numIterations = 10000;

            //-----------------------------------------------------------------

            sw.Start();
            for (int i = 0; i < numIterations; i++)
            {
                decimal? targetType = someDecimalValue.ToNullable<decimal>();
            }
            sw.Stop();
            long elapsedForToNullable = sw.ElapsedTicks;

            //-----------------------------------------------------------------

            sw.Start();
            for (int i = 0; i < numIterations; i++)
            {
                decimal? targetType = null;
                decimal tmp = 0;
                if (decimal.TryParse(someDecimalValue, out tmp))
                {
                    targetType = tmp;
                }
            }
            sw.Stop();
            long elapsedForTryParse = sw.ElapsedTicks;

            //-----------------------------------------------------------------

            sw.Reset();
            Console.WriteLine("ToNullable: " + elapsedForToNullable + " ticks.");
            Console.WriteLine("TryParse: " + elapsedForTryParse + " ticks.");
            Assert.IsTrue(elapsedForToNullable < elapsedForTryParse, "The ToNullable string extension should be faster.");
        }
    }
}
