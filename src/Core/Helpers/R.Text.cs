using System.Reflection;
using System.Text.RegularExpressions;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury.AI;
using Amplitude.Mercury.AI.Brain;
using Amplitude.Mercury.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public static partial class R
    {
        public static class Text
        {
        
            /// <summary>
            ///     Returns text formatted with given color in RichText format.
            /// </summary>
            /// <param name="text">String to format specified color.</param>
            /// <param name="color">HEX color string. Optionally, can be followed by two extra hex digits to specify alpha value. Also, preceding <c>#</c> character is optional.</param>
            /// <returns>Text in specified HEX color.</returns>
            public static string Color(string text, string color = "FFFFFF") =>
                "<color=" + NormalizeColor(color) + ">" + text + "</color>";

            /// <summary>
            ///     Text in bold style to use in RichText strings.
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string Bold(string text) => "<b>" + text + "</b>";
            
            /// <summary>
            ///     Change font size of riven RichText string.
            /// </summary>
            /// <param name="text"></param>
            /// <param name="size"></param>
            /// <returns></returns>
            public static string Size(string text, int size = 12) => "<size=" + size + ">" + text + "</size>";

            /// <summary>
            ///     Normalize RichText Hex Color.
            /// </summary>
            /// <param name="color"></param>
            /// <param name="alpha"></param>
            /// <returns>Hex color string in #RRGGBBAA format.</returns>
            public static string NormalizeColor(string color, string alpha = "FF")
            {
                switch (color.Length)
                {
                    case 2:
                        return "#" + color + color + color + alpha;
                    case 3:
                        return "#" + color + color + alpha;
                    case 4:
                        return "#" + color.Substring(1) + color.Substring(1) + alpha;
                    case 6:
                        return "#" + color + alpha;
                    case 7:
                        return "#" + color.Substring(1) + alpha;
                    case 8:
                        return "#" + color;
                    case 9:
                    default:
                        return color;
                }
            }
            
            /// <summary>
            ///     Splits a camelCase string into words.
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string SplitCamelCase(string text)
            {
                string sTemp = Regex.Replace(text, "([A-Z][a-z])", " $1", RegexOptions.Compiled).Trim();
                return Regex.Replace(sTemp, "([A-Z][A-Z])", " $1", RegexOptions.Compiled).Trim();
            }

            public static Amplitude.Mercury.UI.Helpers.DataUtils DataUtils =>
                ((Amplitude.Mercury.UI.Helpers.DataUtils) R.Fields.DataUtilsField.GetValue(null));

            public static string GetLocalizedTitle(Amplitude.StaticString uiMapperName) => DataUtils?.GetLocalizedTitle(uiMapperName) ?? uiMapperName.ToString();
            
            public static string GetLocalizedDescription(Amplitude.StaticString uiMapperName) => DataUtils?.GetLocalizedDescription(uiMapperName) ?? uiMapperName.ToString();
        }
    }
}
