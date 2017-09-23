using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangamma.Utilities
{
    public class RandomModelObject
    {
        public int? ID { get; set; } // non created ModelObjects have null ID's.
        public string Label { get; set; }

        public bool NonNullableBoolean { get; set; }
        public decimal NonNullableDecimal { get; set; }
        public DateTime NonNullableDateTime { get; set; }
        public int NonNullableInteger { get; set; }
        public AnimalTypeEnum NonNullableEnum { get; set; }

        public bool? NullableBoolean { get; set; }
        public decimal? NullableDecimal { get; set; }
        public DateTime? NullableDateTime { get; set; }
        public int? NullableInteger { get; set; }
        public AnimalTypeEnum? NullableEnum { get; set; }
    }
}
