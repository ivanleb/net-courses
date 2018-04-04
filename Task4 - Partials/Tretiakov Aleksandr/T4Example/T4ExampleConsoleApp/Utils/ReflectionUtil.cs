using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4ExampleConsoleApp.Partials
{
    class ReflectionUtil
    {
        private static readonly string nullString = "is null";
        
        public static string GetObjectStringRepresentation(object targetObject)
        {
            var objectType = targetObject.GetType();
            if (objectType.IsPrimitive)
                return $"{targetObject}";
            var properties = objectType.GetProperties();
            if (properties.Length == 0)
                return $"is empty";
            return $"\n{GetPropertiesStringRepresentation(properties, targetObject)}";
        }
        
        private static string GetPropertiesStringRepresentation(IEnumerable<System.Reflection.PropertyInfo> properties, object targetObject)
        {
            var builder = new StringBuilder();
            foreach (var property in properties)
            {
                builder.Append($"{property.Name}: ")
                    .Append( property.GetValue(targetObject)?.ToString() ?? nullString )
                    .Append("\n");
            }
            return builder.ToString();
        }
    }
}
