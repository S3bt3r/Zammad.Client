using System;
using System.Reflection;

namespace Zammad.Connector.Core.IO.Annotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SerializerAttribute : Attribute
    {
        public SerializerAttribute(string fileExtension)
        {
            FileExtension = fileExtension;
        }

        public string FileExtension { get; }

        public static SerializerAttribute Extract(Type commandType)
        {
            return commandType.GetCustomAttribute<SerializerAttribute>();
        }
    }
}
