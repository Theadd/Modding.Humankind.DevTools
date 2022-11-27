using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Modding.Humankind.DevTools.DeveloperTools
{
    public class PrintableObject
    {
        public bool NonPublicProperties { get; set; } = false;
        public bool Properties { get; set; } = true;
        public bool NonPublicFields { get; set; } = false;
        public bool Fields { get; set; } = true;
        public bool Methods { get; set; } = false;

        public object instance;
        public Type type;

        private bool isNull = false;

        private bool isEnum = false;

        public PrintableObject(Type type)
        {
            this.type = type;
            instance = null;
        }

        public PrintableObject(object instance)
        {
            if (instance == null)
                isNull = true;

            this.instance = instance;
            type = instance?.GetType();
        }

        public override string ToString()
        {
            if (isNull)
                return "%DarkGray%null";

            if (type.IsArray)
            {
                return StringifyArray();
            }

            isEnum = type.IsEnum;   // TODO

            return StringifyObject();
        }

        private string StringifyArray()
        {
            var baseTypeName = type.BaseType?.Name ?? "";
            var arrayLength = ((Array)instance).Length;
            baseTypeName = baseTypeName.Length > 0 ? " %White%:%DarkBlue% " + baseTypeName + 
                "%White%(" + arrayLength + ") { " + (arrayLength > 0 ? "" : "} ") : " %White%{ " + (arrayLength > 0 ? "" : "} ");

            var sb = new StringBuilder();
            // sb.AppendLine("%White%Type%DarkBlue% " + type.Name + " %DarkGray%(" + type.FullName + ") %White%{ ");
            sb.AppendLine("%White%Type%DarkBlue% " + type.Name + baseTypeName);

            if (arrayLength > 0)
            {
                var count = arrayLength < 200 ? arrayLength : 32;

                for(var i = 0; i < count; i++)
                {
                    sb.AppendLine(
                        "    " + 
                        PrintableValue.AsValueOnlyString(
                            ((Array)instance).GetValue(i),
                            ((Array)instance).GetValue(i).GetType()
                        )
                    );
                }

                if (arrayLength - count > 0)
                {
                    sb.AppendLine("    " + PrintableValue.ColorType.NotImportant + "... " + PrintableValue.ColorType.Default + 
                        (arrayLength - count) + " items remaining.");
                }

                sb.AppendLine("%White%} ");
            }

            // AsValueOnlyString
            //return StringifyObject();   // TODO
            return sb.ToString();
        }

        private string StringifyObject()
        {
            
            var sb = new StringBuilder();
            var publicPropertyMembers = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            var nonPublicPropertyMembers = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var fieldMembers = type
                .GetFields(
                    NonPublicFields ? (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance) 
                        : (BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                .AsEnumerable()
                .Where(field => field.Name.Length > 0 && field.Name[0] != '<')
                .ToArray();
            var publicMethods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .AsEnumerable()
                .Where(m => !(m.Name.Length > 3 && m.Name[3] == '_'));

            var baseTypeName = type.BaseType?.Name ?? "";
            baseTypeName = baseTypeName.Length > 0 ? " %White%:%DarkBlue% " + baseTypeName + " %White%{ " : " %White%{ ";

            // sb.AppendLine("%White%Type%DarkBlue% " + type.Name + " %DarkGray%(" + type.FullName + ") %White%{ ");
            sb.AppendLine("%White%Type%DarkBlue% " + type.Name + baseTypeName);
            
            if (Properties)
            {
                if (publicPropertyMembers.Length > 0)
                {
                    sb.AppendLine("\n%Blue%    public properties%Default%");
                    AppendPropertyMembers(publicPropertyMembers, ref sb);
                }

                if (NonPublicProperties && nonPublicPropertyMembers.Length > 0)
                {
                    sb.AppendLine("\n%Blue%    non-public properties%Default%");
                    AppendPropertyMembers(nonPublicPropertyMembers, ref sb);
                }
            }
            
            if (Fields && fieldMembers.Length > 0)
            {
                sb.AppendLine("\n%Blue%    accessible fields%Default%");
                AppendFieldMembers(fieldMembers, ref sb);
            }

            if (Methods && publicMethods.Count() > 0)
            {
                sb.AppendLine("\n%Blue%    public methods%Default%");
                AppendMethodMembers(publicMethods, ref sb);
            }

            sb.AppendLine("%White%};");
            
            return sb.ToString();
        }

        private void AppendMethodMembers(IEnumerable<MethodInfo> methodMembers, ref StringBuilder sb)
        {
            sb.AppendLine(string.Join("\n",
                methodMembers.Select(m => (m.IsStatic ? " %Yellow%static" : "       ") +
                "%DarkMagenta% " + m.Name + "%DEFAULT%(" + 
                        (string.Join("%DEFAULT%, ", m.GetParameters().Select(p => 
                            PrintableValue.ColorType.HeadingType + p.ParameterType.Name + "%Cyan% " + p.Name).ToArray())) + 
                        "%DEFAULT%) => " + PrintableValue.ColorType.FullType + m.ReturnType?.Name)
            ));
        }

        private void AppendPropertyMembers(IEnumerable<PropertyInfo> propertyMembers, ref StringBuilder sb)
        {
            foreach (var propInfo in propertyMembers)
            {
                MethodInfo getter = propInfo.GetGetMethod(true);

                if (getter == null)
                {
                    sb.AppendLine("%Red%\tCAN'T GET PROPERTY GETTER FOR: " + propInfo.Name);
                    continue;
                }
                string propValue;
                try {
                    propValue = PrintableValue.AsString(getter.Invoke(getter.IsStatic ? null : (instance != null ? instance : type), null),
                        propInfo.PropertyType);
                }
                catch (Exception e)
                {
                    propValue = PrintableValue.MergeValueAndType(
                        PrintableValue.ColorType.NotImportant + "<" + e.GetType().Name + "> " + PrintableValue.ColorType.Error + "FAIL"
                        , propInfo.PropertyType.Name, PrintableValue.ColorType.NotImportant.Length + PrintableValue.ColorType.Error.Length);
                }
                
                sb.Append((getter.IsStatic ? " %Yellow%static" : "       ") + (propInfo.CanWrite ? "%DarkCyan% " : "%Cyan% "));
                AppendMemberAsFormattedString(propInfo.Name, ref sb);
                sb.AppendLine(propValue);
            }
        }

        private void AppendFieldMembers(IEnumerable<FieldInfo> fieldMembers, ref StringBuilder sb)
        {
            foreach (var fieldInfo in fieldMembers)
            {
                string fieldValue;

                try {
                    fieldValue = PrintableValue.AsString(fieldInfo.GetValue(fieldInfo.IsStatic ? null : (instance != null ? instance : type)),
                        fieldInfo.FieldType);
                }
                catch (Exception e)
                {
                    fieldValue = PrintableValue.MergeValueAndType(
                        PrintableValue.ColorType.NotImportant + "<" + e.GetType().Name + "> " + PrintableValue.ColorType.Error + "FAIL"
                        , fieldInfo.FieldType.Name, PrintableValue.ColorType.NotImportant.Length + PrintableValue.ColorType.Error.Length);
                }
                
                sb.Append((fieldInfo.IsStatic ? " %Yellow%static" : "       ") + ((!fieldInfo.IsPrivate && fieldInfo.IsPublic && !fieldInfo.IsInitOnly) ? "%DarkCyan% " : "%Cyan% "));
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