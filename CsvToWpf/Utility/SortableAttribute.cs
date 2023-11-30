using System;

namespace CsvToWpf.Utility;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class SortableAttribute : Attribute
{
    public bool IsSortable { get; }

    public SortableAttribute(bool isSortable)
    {
        IsSortable = isSortable;
    }
}