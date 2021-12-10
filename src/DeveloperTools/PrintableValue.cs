using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amplitude;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools.DeveloperTools
{

    public static class PrintableValue
    {
        public static class ColorType
        {
            public static string NotFound = "%Red%";
            public static string HeadingType = "%DarkCyan%";
            public static string FullType = "%DarkGray%";
            public static string Default = "%White%";
        }

        private static string MergeValueAndType(string valueString, string typeString, int lenMod)
        {
            var padRight = lenMod > 0 ? new string(' ', lenMod) : "";
            return $"{valueString,-64}" + padRight + ColorType.FullType + "// " + typeString;
            // return valueString + "\t\t" + ColorType.FullType + "// " + typeString;
        }
        
        public static string AsString(object objectValue, Type objectType)
        {
            string result;
            string fullType;
            int lenMod;

            if (TryGetFromTypeName(objectValue, objectType, out result, out fullType, out lenMod))
                return MergeValueAndType(result, fullType, lenMod);
            
            return ColorType.NotFound + objectType.Name;
        }

        private static bool TryGetFromTypeName(object objectValue, Type objectType, out string result, out string fullType, out int lenMod)
        {
            fullType = objectType.FullName;
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
                    result = (string) objectValue;
                    fullType = "string";
                    break;
                case "CityFlags":
                    result = ((Amplitude.Mercury.Simulation.CityFlags) objectValue).ToString();
                    fullType += " Enum";
                    break;
                case "AwakeState":
                    result = ((Amplitude.Mercury.Interop.AwakeState) objectValue).ToString();
                    fullType += " Enum";
                    break;
                case "SettlementStatuses":
                    result = ((Amplitude.Mercury.Data.Simulation.SettlementStatuses) objectValue).ToString();
                    fullType += " Enum";
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
                    result = ((Amplitude.StaticString) objectValue).ToString();
                    fullType = "StaticString";
                    break;
                case "UInt64":
                    result = ((UInt64) objectValue).ToString();
                    fullType = "UInt64";
                    break;
                case "List`1":
                    result = fullType;
                    break;
                default:
                    result = null;
                    return false;
            }

            return true;
        }
    }
}
