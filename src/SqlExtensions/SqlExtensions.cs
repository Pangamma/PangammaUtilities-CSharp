using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangamma.Utilities
{
    public static class SqlExtensions
    {
        /// <summary>
        /// Works like AddWithValue, except it properly handles null values
        /// by adding DBNull.Value if the object is null.
        /// </summary>
        /// <param name="p_collection"></param>
        /// <param name="p_paramName"></param>
        /// <param name="p_paramValue"></param>
        /// <returns></returns>
        /// Added by Taylor Love
        public static SqlParameter AddNullSafe(this SqlParameterCollection collection,string paramName, object paramValue)
        {
            if (paramValue != null)
            {
                return collection.AddWithValue(paramName, paramValue);
            }
            else
            {
                return collection.AddWithValue(paramName, DBNull.Value);
            }
        }
    }
}
