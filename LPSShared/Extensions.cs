using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace LPS
{
    public static class Extensions
    {
        /// <summary>
        /// Returns true if string is null or equals String.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this string s)
        {
            return (s == null || s == String.Empty);
        }

        /// <summary>
        /// Returns true if string is null or string.Trim() equals String.Empty
        /// </summary>
        public static bool IsNullOrTrimEmpty(this string s)
        {
            return (s == null || s.Trim() == String.Empty);
        }

        /// <summary>
        /// Email address validation througt regular expression
        /// </summary>
        /// <returns>true if string represents valid email address</returns>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new
              Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

		/// <summary>
		/// If object is null or DBNull
		/// </summary>
		public static bool IsNull(this object o)
		{
			return (o == null) || (o.Equals(DBNull.Value));
		}

		/// <summary>
		/// If object is not null and not DBNull
		/// </summary>
		public static bool IsNotNull(this object o)
		{
			return ! o.IsNull();
		}

		/// <summary>
		/// If object is not null and not DBNull,
		/// convert it to val via Convert.ChangeType
		/// </summary>
		public static bool IsNotNull<T>(this object o, out T val)
		{
			bool result = o.IsNotNull();
			if(result)
				val = (T)Convert.ChangeType(o, typeof(T));
			else
				val = default(T);
			return result;
		}
    }
}

