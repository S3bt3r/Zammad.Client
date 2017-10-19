using System;
using System.Reflection;

namespace Zammad.Connector.Core.Commands.Annotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CliCommandAttribute : Attribute
    {
        public CliCommandAttribute(string verb, string noun)
        {
            Verb = verb;
            Noun = noun;
        }

        public string Verb { get; }
        public string Noun { get; }        
        public string Help { get; set; }

        public string Name => $"{Verb}-{Noun}";

        public static CliCommandAttribute Extract(Type commandType)
        {
            return commandType.GetCustomAttribute<CliCommandAttribute>();
        }
    }
}