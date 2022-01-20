using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amplitude;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury;
using Modding.Humankind.DevTools.Core;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools
{

    public static class PrintableValue
    {
        public static class ColorType
        {
            public static string NotFound = "%Red%";
            public static string HeadingType = "%DarkGreen%";
            public static string FullType = "%DarkGray%";
            public static string NotImportant = "%DarkGray%";
            public static string Error = "%DarkRed%";
            public static string Default = "%White%";
            public static string String = "%DarkYellow%";
        }

        public static bool UseFullTypeNames { get; set; } = false;

        public static string MergeValueAndType(string valueString, string typeString, int lenMod)
        {
            var padRight = lenMod > 0 ? new string(' ', lenMod) : "";
            var realLength = valueString.Length - lenMod;
            if (realLength >= 63) {
                valueString = valueString.Substring(0, 60 + lenMod) + "...";
            }
            return $"{valueString,-64}" + padRight + ColorType.FullType + "// " + typeString;
            // return valueString + "\t\t" + ColorType.FullType + "// " + typeString;
        }

        public static string KeepValueOnly(string valueString, string typeString, int lenMod)
        {
            var padRight = lenMod > 0 ? new string(' ', lenMod) : "";
            var realLength = valueString.Length - lenMod;
            if (realLength >= 123) {
                valueString = valueString.Substring(0, 120 + lenMod) + "...";
            }
            return $"{valueString,-124}" + padRight;    // + ColorType.FullType + "// " + typeString;
            // return valueString + "\t\t" + ColorType.FullType + "// " + typeString;
        }

        public static string AsValueOnlyString(object objectValue, Type objectType)
        {
            string result;
            string fullType;
            int lenMod;

            if (objectValue == null)
            {
                return KeepValueOnly(
                    ColorType.FullType + "NULL",
                    objectType.FullName,
                    ColorType.FullType.Length
                );
            }

            if (TryGetFromEnumType(objectValue, objectType, out result, out fullType, out lenMod))
                return KeepValueOnly(result, fullType, lenMod);

            if (TryGetFromTypeName(objectValue, objectType, out result, out fullType, out lenMod))
                return KeepValueOnly(result, fullType, lenMod);

            return ColorType.NotFound + objectType.Name;
        }
        
        public static string AsString(object objectValue, Type objectType)
        {
            string result;
            string fullType;
            int lenMod;

            if (objectValue == null)
            {
                return MergeValueAndType(
                    ColorType.FullType + "NULL",
                    objectType.FullName,
                    ColorType.FullType.Length
                );
            }

            if (TryGetFromEnumType(objectValue, objectType, out result, out fullType, out lenMod))
                return MergeValueAndType(result, fullType, lenMod);

            if (TryGetFromTypeName(objectValue, objectType, out result, out fullType, out lenMod))
                return MergeValueAndType(result, fullType, lenMod);

            return ColorType.NotFound + objectType.Name;
        }

        private static bool TryGetFromTypeName(object objectValue, Type objectType, out string result, out string fullType, out int lenMod)
        {
            fullType = UseFullTypeNames ? objectType.FullName : objectType.Name;
            lenMod = 0;
            
            switch (objectType.Name)
            {
                case "Int32":
                    result = ((int) objectValue).ToString();
                    fullType = "int";
                    break;
                case "Boolean":
                    result = ((Boolean) objectValue).ToString();
                    fullType = "bool";
                    break;
                case "FixedPoint":
                    result = ((int) ((FixedPoint) objectValue)).ToString();
                    fullType = "FixedPoint";
                    break;
                case "String":
                    result = ColorType.String + "\"" + ((string) objectValue) + "\"";
                    lenMod = ColorType.String.Length;
                    fullType = "string";
                    break;
                case "Single":
                    result = ((float) objectValue).ToString();
                    fullType = "float";
                    break;
                case "Territory[]":
                    var territories = (Amplitude.Mercury.Interop.AI.Entities.Territory[]) objectValue;
                    var territoryIndices = territories.Aggregate("",
                        (total, next) => total += (total.Length > 0 ? ", " : "") + next.TerritoryIndex);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "Territory[" + territories.Length + "]" + ColorType.Default + " { " + territoryIndices + " }";
                    break;
                case "WorldPosition":
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "WorldPosition " + ColorType.Default + ((WorldPosition) objectValue) + ", TileIndex " + ((WorldPosition) objectValue).ToTileIndex();
                    break;
                case "Property":
                    result = ((int) ((Property) objectValue).Value).ToString();
                    fullType = "Property";
                    break;
                case "EditableProperty":
                    result = ((int) ((EditableProperty) objectValue).Value).ToString();
                    fullType = "EditableProperty";
                    break;
                case "EntityNameInfo":
                    result = ((Amplitude.Mercury.Interop.EntityNameInfo) objectValue).ToString();
                    fullType = "EntityNameInfo";
                    break;
                case "StaticString":
                    result = "@" + ColorType.String + "\"" + ((Amplitude.StaticString) objectValue).ToString() + "\"";
                    lenMod = ColorType.String.Length;
                    fullType = "StaticString";
                    break;
                case "UInt64":
                    result = ((UInt64) objectValue).ToString();
                    fullType = "ulong";
                    break;
                case "Int64":
                    result = ((Int64) objectValue).ToString();
                    fullType = "long";
                    break;
                case "List`1":
                    var listLength = ((System.Collections.ICollection) objectValue)?.Count;
                    result = ColorType.NotFound + "List<" + objectType.GetGenericArguments().FirstOrDefault().Name + ">[" + listLength + "]";
                    fullType = "List<" + objectType.GetGenericArguments().FirstOrDefault().FullName + ">";
                    lenMod = ColorType.NotFound.Length;
                    break;
                case "Unit[]":
                    var units = (Amplitude.Mercury.Interop.AI.Data.Unit[]) objectValue;
                    var unitNames = string.Join(", ", units.AsEnumerable().Select(unit => unit.UnitDefinition.Name.ToString().Split('_').LastOrDefault()).ToArray());
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "Unit[" + units.Length + "]" + ColorType.Default + " { " + unitNames + " }";
                    break;
                case "Vector2[]":
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.HeadingType.Length;
                    result = ColorType.HeadingType + "Vector2[" + ColorType.Default + ((Vector2[]) objectValue).Length + ColorType.HeadingType + "]";
                    fullType = "Vector2[]";
                    break;
                case "UInt16[]":
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.HeadingType.Length;
                    result = ColorType.HeadingType + "UInt16[" + ColorType.Default + ((UInt16[]) objectValue).Length + ColorType.HeadingType + "]";
                    fullType = "UInt16[]";
                    break;
                case "UInt64[]":
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.HeadingType.Length;
                    result = ColorType.HeadingType + "UInt64[" + ColorType.Default + ((UInt64[]) objectValue).Length + ColorType.HeadingType + "]";
                    fullType = "UInt64[]";
                    break;
                case "Int64[]":
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.HeadingType.Length;
                    result = ColorType.HeadingType + "Int64[" + ColorType.Default + ((Int64[]) objectValue).Length + ColorType.HeadingType + "]";
                    fullType = "Int64[]";
                    break;
                case "ArmyActionFailureFlags":
                    result = ((Amplitude.Mercury.Interop.ArmyActionFailureFlags) objectValue).ToString();
                    fullType += " Struct";
                    break;
                case "RectOffset":
                    result = ((RectOffset) objectValue).ToString();
                    fullType = "RectOffset";
                    break;
                case "Rect":
                    result = ((Rect) objectValue).ToString();
                    fullType = "Rect";
                    break;
                case "Bounds":
                    result = ((Bounds) objectValue).ToString();
                    fullType = "Bounds";
                    break;
                case "Vector2":
                    result = ((Vector2) objectValue).ToString();
                    fullType = "Vector2";
                    break;
                case "Vector3":
                    result = ((Vector3) objectValue).ToString();
                    fullType = "Vector3";
                    break;
                case "Vector4":
                    result = ((Vector4) objectValue).ToString();
                    fullType = "Vector4";
                    break;
                case "Texture":
                    result = ColorType.HeadingType + "Texture " + ColorType.Default + ((Texture) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Texture";
                    break;
                case "Texture2D":
                    result = ColorType.HeadingType + "Texture2D " + ColorType.Default + ((Texture2D) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Texture2D";
                    break;
                case "Sprite":
                    result = ColorType.HeadingType + "Sprite " + ColorType.Default + ((Sprite) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Sprite";
                    break;
                case "Type":
                    result = ColorType.HeadingType + "Type " + ColorType.Default + ((Type) objectValue).Name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Type";
                    break;
                case "Assembly":
                    result = ColorType.HeadingType + "Assembly " + ColorType.Default + ((Assembly) objectValue).GetName();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Assembly";
                    break;
                case "Module":
                    result = ColorType.HeadingType + "Module " + ColorType.Default + ((Module) objectValue).Name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Module";
                    break;
                case "KeyboardShortcut":
                    result = ColorType.HeadingType + "KeyboardShortcut " + ColorType.Default + ((BepInEx.Configuration.KeyboardShortcut) objectValue).ToString();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    break;
                case "MethodInfo":
                    result = ColorType.HeadingType + "MethodInfo " + ColorType.Default + ((MethodInfo) objectValue).Name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "MethodInfo";
                    break;
                case "FieldInfo":
                    result = ColorType.HeadingType + "FieldInfo " + ColorType.Default + ((FieldInfo) objectValue).Name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "FieldInfo";
                    break;
                case "PropertyInfo":
                    result = ColorType.HeadingType + "PropertyInfo " + ColorType.Default + ((PropertyInfo) objectValue).Name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "PropertyInfo";
                    break;
                case "Color":
                    result = ColorType.HeadingType + "Color " + ColorType.Default + ((Color) objectValue).ToString();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Color";
                    break;
                case "Color32":
                    result = ColorType.HeadingType + "Color32 " + ColorType.Default + ((Color32) objectValue).ToString();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Color32";
                    break;
                case "GUIStyleState":
                    result = ColorType.HeadingType + "GUIStyleState " + ColorType.Default + ((GUIStyleState) objectValue).background.name + ", " + ((GUIStyleState) objectValue).textColor.ToString();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "GUIStyleState";
                    break;
                case "Font":
                    result = ColorType.HeadingType + "Font " + ColorType.Default + ((Font) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Font";
                    break;
                case "GUIStyle":
                    result = ColorType.HeadingType + "GUIStyle " + ColorType.Default + ((GUIStyle) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "GUIStyle";
                    break;
                case "Material":
                    result = ColorType.HeadingType + "Material " + ColorType.Default + ((Material) objectValue).name;
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "Material";
                    break;
                default:
                    result = null;
                    return false;
            }

            return true;
        }

        private static bool TryGetFromEnumType(object objectValue, Type objectType, out string result,
            out string fullType, out int lenMod)
        {
            fullType = UseFullTypeNames ? objectType.FullName : objectType.Name;
            lenMod = 0;

            if (objectType.IsEnum)
            {
                result = Enum.GetName(objectType, objectValue);
                fullType += " Enum";
                
            }
            else
            {
                result = null;
                return false;
            }

            return true;
        }
    }
    
    /*internal static class ObjectExtensions
    {
        public static T CastTo<T>(this object o) => (T)o;

        public static dynamic CastToReflected(this object o, Type type)
        {
            var methodInfo = typeof(ObjectExtensions).GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public);
            var genericArguments = new[] { type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, new[] { o });
        }
    }*/
}
