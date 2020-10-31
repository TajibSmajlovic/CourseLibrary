using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace CourseLibrary.Common.Extensions
{
    public static class IListExtensions
    {
        public static IList<ExpandoObject> ShapeData<TSource>(this IList<TSource> source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var expandoObjectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                foreach (var field in fields.Split(','))
                {
                    var propertyName = field.Trim();

                    var propertyInfo = typeof(TSource)
                        .GetProperty(propertyName, BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                    //if (propertyInfo = null)
                    //    throw new Exception($"Property {propertyName} wasn't found on" + $"{typeof(TSource)}");

                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource sourceObject in source)
            {
                var dataShapeObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    ((IDictionary<string, object>)dataShapeObject)
                        .Add(propertyInfo.Name, propertyValue);
                }

                expandoObjectList.Add(dataShapeObject);
            }

            return expandoObjectList;
        }
    }
}