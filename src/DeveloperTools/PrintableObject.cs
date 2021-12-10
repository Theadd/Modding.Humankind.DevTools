using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modding.Humankind.DevTools.DeveloperTools
{
    public class PrintableObject
    {
        public object instance;
        public Type type;

        public PrintableObject(Type type)
        {
            this.type = type;
            instance = null;
        }

        public PrintableObject(object instance)
        {
            this.instance = instance;
            type = instance.GetType();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var propertyMembers = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                     BindingFlags.Static | BindingFlags.Instance);
            var fieldMembers = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                              BindingFlags.Instance);

            sb.AppendLine("%White%Type%DarkCyan% " + type.Name + " %DarkGray%(" + type.FullName + ") %White%{ ");

            if (propertyMembers.Length > 0)
            {
                sb.AppendLine("%DarkYellow%    [PROPERTIES]%Default%");
                AppendPropertyMembers(propertyMembers, ref sb);
            }

            if (fieldMembers.Length > 0)
            {
                sb.AppendLine("%DarkYellow%    [FIELDS]%Default%");
                AppendFieldMembers(fieldMembers, ref sb);
            }

            sb.AppendLine("%White%};");
            
            return sb.ToString();
        }

        private void AppendPropertyMembers(IEnumerable<PropertyInfo> propertyMembers, ref StringBuilder sb)
        {
            foreach (var propInfo in propertyMembers)
            {
                MethodInfo getter = propInfo.GetGetMethod(true);

                if (getter == null)
                {
                    sb.AppendLine("%Red%\tCAN'T GET PROPERTY GETTER OF: " + propInfo.Name);
                    continue;
                }
                
                var propValue = PrintableValue.AsString(getter.Invoke(getter.IsStatic ? null : instance, null),
                    propInfo.PropertyType);
                sb.Append(propInfo.CanRead ? "%Blue%\t" : "%Gray%\t");
                AppendMemberAsFormattedString(propInfo.Name, ref sb);
                sb.AppendLine(propValue);
            }
        }

        private void AppendFieldMembers(IEnumerable<FieldInfo> fieldMembers, ref StringBuilder sb)
        {
            foreach (var fieldInfo in fieldMembers)
            {
                var fieldValue = PrintableValue.AsString(fieldInfo.GetValue(fieldInfo.IsStatic ? null : instance),
                    fieldInfo.FieldType);
                sb.Append(fieldInfo.IsPublic ? "%Blue%\t" : "%Gray%\t");
                AppendMemberAsFormattedString(fieldInfo.Name, ref sb);
                sb.AppendLine(fieldValue);
            }
        }

        private static int baseSpan = 26;
        private static int spanIncrement = 8;
        
        private static void AppendMemberAsFormattedString(string memberName, ref StringBuilder sb)
        {
            int span = baseSpan;

            while (span < memberName.Length)
                span += spanIncrement;

            sb.Append(memberName + "%Gray%");
            sb.Append(' ', (span - memberName.Length) + 2);
            sb.Append("=  ");
        }
    }
}