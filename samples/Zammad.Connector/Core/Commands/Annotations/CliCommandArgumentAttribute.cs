using System;
using System.Reflection;

namespace Zammad.Connector.Core.Commands.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class CliCommandArgumentAttribute : Attribute
    {
        public CliCommandArgumentAttribute(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        public string Name { get; }
        public bool Required { get; }
        public string Help { get; set; }

        public static CliCommandArgumentAttribute Extract(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<CliCommandArgumentAttribute>();
        }
    }
}