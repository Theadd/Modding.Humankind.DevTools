using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amplitude;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury;
using Amplitude.UI;
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
            public static string EnumType = "%Green%";
            public static string FullType = "%DarkGray%";
            public static string NotImportant = "%DarkGray%";
            public static string Error = "%DarkRed%";
            public static string Default = "%White%";
            public static string String = "%DarkYellow%";
            public static string AdditionalInfo = "%DarkYellow%";
        }

        public static bool UseFullTypeNames { get; set; } = false;

        public static string MergeValueAndType(string valueString, string typeString, int lenMod)
        {
            var padRight = lenMod > 0 ? new string(' ', lenMod) : "";
            var realLength = valueString.Length - lenMod;
            if (realLength >= 83) {
                valueString = valueString.Substring(0, 80 + lenMod) + "...";
            }
            return $"{valueString,-84}" + padRight + ColorType.FullType + "// " + typeString;
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
                    ColorType.FullType + "null",
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
                    ColorType.FullType + "null",
                    objectType.FullName,
                    ColorType.FullType.Length
                );
            }

            if (TryGetFromEnumType(objectValue, objectType, out result, out fullType, out lenMod))
                return MergeValueAndType(result, fullType, lenMod);

            if (TryGetFromTypeName(objectValue, objectType, out result, out fullType, out lenMod))
                return MergeValueAndType(result, fullType, lenMod);

            if (TryGetGameObjectRelatedTypes(objectValue, objectType, out result, out fullType, out lenMod))
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
                case "IndexRange":
                    result = ColorType.HeadingType + "IndexRange " + ColorType.Default + ((IndexRange) objectValue).ToString();
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    fullType = "IndexRange";
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
                lenMod = ColorType.EnumType.Length;
                result = ColorType.EnumType + objectType.Name + "." + Enum.GetName(objectType, objectValue);
                fullType += " Enum";
                
            }
            else
            {
                result = null;
                return false;
            }

            return true;
        }
        
        private static bool TryGetGameObjectRelatedTypes(object objectValue, Type objectType, out string result,
            out string fullType, out int lenMod)
        {
            fullType = UseFullTypeNames ? objectType.FullName : objectType.Name;
            lenMod = 0;

            switch (objectType.Name)
            {
                case "UITransform":
                    var uiT = ((UITransform) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "UITransform @ " + ColorType.Default + uiT.Rect;
                    break;
                case "UITooltipData":
                    lenMod = ColorType.String.Length;
                    result = ColorType.String + $"\"{((Amplitude.UI.Interactables.UITooltipData) objectValue).Message}\"";
                    break;
                case "UITooltipClassDefinition":
                    lenMod = ColorType.String.Length;
                    result = ColorType.String + $"@\"{((Amplitude.UI.Tooltips.UITooltipClassDefinition) objectValue).Name.ToString()}\"";
                    break;
                case "RectMargins":
                    var r = ((RectMargins) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "RectMargins " + ColorType.Default + $"(l: {r.Left}, r: {r.Right}, t: {r.Top}, b: {r.Bottom})";
                    break;
                case "UIBorderAnchor":
                    var anchor = ((UIBorderAnchor) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "UIBorderAnchor " + ColorType.Default + $"(Attach: {anchor.Attach}, Percent: {anchor.Percent}, Margin: {anchor.Margin}, Offset: {anchor.Offset})";
                    break;
                case "UIPivotAnchor":
                    var pivot = ((UIPivotAnchor) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "UIPivotAnchor " + ColorType.Default + $"(Attach: {pivot.Attach}, Percent: {pivot.Percent}, MinMargin: {pivot.MinMargin}, MaxMargin: {pivot.MaxMargin}, Offset: {pivot.Offset})";
                    break;
                case "UIAtomId":
                    var atom = ((UIAtomId) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "UIAtomId " + ColorType.Default + $"(Index: {atom.Index}, Allocator: {atom.Allocator}, IsValid: {atom.IsValid})";
                    break;
                case "UIStamp":
                    var stamp = ((UIStamp) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "UIStamp " + ColorType.Default + $"(RegistrationId: {stamp.RegistrationId}, KeyGuid: {stamp.KeyGuid}, IsLoaded: {stamp.IsLoaded})";
                    break;
                case "AffineTransform2d":
                    var at = ((AffineTransform2d) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length;
                    result = ColorType.HeadingType + "AffineTransform2d " + ColorType.Default + $"Translation: {at.Translation.ToString()}, Rotation: {at.Rotation.ToString()}, Scale: {at.Scale.ToString()}";
                    break;
                case "UIMaterialId":
                    lenMod = ColorType.HeadingType.Length + ColorType.String.Length;
                    result = ColorType.HeadingType + "UIMaterialId " + ColorType.String + $"@\"{((UIMaterialId)objectValue).Id.ToString()}\"";
                    break;
                case "UITexture":
                    var uiTex = ((UITexture) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.AdditionalInfo.Length;
                    result = ColorType.HeadingType + "UITexture" + ColorType.Default + " " + uiTex.AssetPath 
                                + " " + ColorType.AdditionalInfo + uiTex.WidthHeight.x + "x" + uiTex.WidthHeight.y + "px";
                    break;
                case "Transform":
                    var t = ((Transform) objectValue);
                    lenMod = ColorType.HeadingType.Length + ColorType.Default.Length + ColorType.AdditionalInfo.Length;
                    result = ColorType.HeadingType + "Transform @ " + ColorType.Default + t.position +
                             (t.childCount > 0 ? ColorType.AdditionalInfo + " [+" + t.childCount + "]" : "");
                    break;
                case "GameObject":
                    lenMod = ColorType.HeadingType.Length + ColorType.String.Length;
                    result = ColorType.HeadingType + "GameObject " + ColorType.String + "\"" + ((GameObject) objectValue).name + "\"";
                    break;
                default:
                    if (objectValue is MonoBehaviour)
                    {
                        lenMod = ColorType.EnumType.Length + ColorType.NotFound.Length;
                        result = ColorType.EnumType + "<MonoBehaviour> " + ColorType.NotFound + objectType.Name;
                    }
                    else
                    {
                        result = null;
                        return false;
                    }

                    break;
            }

            return true;
        }
    }

}
