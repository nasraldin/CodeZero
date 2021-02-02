using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CodeZero.Shared.Extensions
{
    /// <summary>
    /// This class is copied from here:
    /// https://gist.github.com/Flatlineato/373743
    /// Extension methods for Enum class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if the value contains the provided type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the value is only the provided type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Appends a value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not append value from enumerated type '{0}'.", typeof(T).Name), ex);
            }
        }

        /// <summary>
        /// Completely removes the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Could not remove value from enumerated type '{0}'.", typeof(T).Name), ex);
            }
        }

        /// <summary>
        /// Returns true if enum matches any of the given values
        /// </summary>
        /// <param name="value">Value to match</param>
        /// <param name="values">Values to match against</param>
        /// <returns>Return true if matched</returns>
        public static bool In(this Enum value, params Enum[] values)
        {
            return values.Any(v => v == value);
        }

        ///// <summary>
        ///// Gets the description of any Enum value.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="e"></param>
        ///// <returns></returns>
        //public static string GetDescription<T>(this T e) where T : IConvertible
        //{
        //    //DescriptionAttribute attribute = value.GetType()
        //    //    .GetField(value.ToString())
        //    //    .GetCustomAttributes(typeof(DescriptionAttribute), false)
        //    //    .SingleOrDefault() as DescriptionAttribute;
        //    //return attribute == null ? value.ToString() : attribute.Description;

        //    try
        //    {
        //        if (e is Enum)
        //        {
        //            Type type = e.GetType();
        //            Array values = System.Enum.GetValues(type);

        //            foreach (int val in values)
        //            {
        //                if (val == e.ToInt32(CultureInfo.InvariantCulture))
        //                {
        //                    var memInfo = type.GetMember(type.GetEnumName(val));

        //                    if (memInfo[0]
        //                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
        //                        .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
        //                    {
        //                        return descriptionAttribute.Description;
        //                    }
        //                }
        //            }
        //        }

        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(string.Format("Could not get description value from enumerated type '{0}'.", typeof(T).Name), ex);
        //    }
        //}

        /// <summary>
        /// Gets the description of any Enum value through DiscriptionAttribute.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Get the Display annotation value of any Enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayValue(this Enum value)
        {
            if (value == null)
            {
                return null;
            }

            var memberInfo = value.GetType().GetMember(value.ToString()).FirstOrDefault();

            return memberInfo?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? string.Empty;
        }
    }
}