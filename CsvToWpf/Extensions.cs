using CsvToWpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsvToWpf;

public static class Extensions
{
    public static List<string> GetSortablePropertyNames(this Type type)
    {
        PropertyInfo[] properties = type.GetProperties();

        var sortablePropertyNames = properties
            .Where(property => Attribute.IsDefined(property, typeof(SortableAttribute)) &&
                               ((SortableAttribute)Attribute.GetCustomAttribute(property, typeof(SortableAttribute))!).IsSortable)
            .Select(property => property.Name)
            .ToList();

        return sortablePropertyNames;
    }
}

