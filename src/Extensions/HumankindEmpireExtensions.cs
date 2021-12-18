
using System;
using System.Collections.Generic;
using System.Linq;
using Amplitude.Mercury.Interop.AI.Entities;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{
    public static class HumankindEmpireExtensions
    {
        /// <summary>
        ///     Filters out all non human player empires from a sequence of <c>HumankindEmpire</c>s. Except when <c>false</c> is passed to <c>isControlledByHuman</c>, which will return a sequence with every empire not controlled by human instead.
        ///     See also: <see cref="HumankindEmpire.IsControlledByHuman">HumankindEmpire.IsControlledByHuman</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="isControlledByHuman">Set this to false to reverse the filter, defaults to true.</param>
        /// <returns></returns>
        public static IEnumerable<HumankindEmpire> IsControlledByHuman(this IEnumerable<HumankindEmpire> sequence, bool isControlledByHuman = true) =>
            sequence.Where(empire => empire.IsControlledByHuman == isControlledByHuman);

        /// <summary>
        ///     Selects all <c>HumankindSettlement</c>s from the <c>HumankindEmpire</c>s sequence.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IEnumerable<HumankindSettlement> Settlements(this IEnumerable<HumankindEmpire> sequence) =>
            sequence.SelectMany(empire => empire.Settlements);
        
        /// <summary>
        ///     Aggregates the Armies of each <c>HumankindEmpire</c> in the sequence into a new sequence of armies.
        /// See also: <see cref="HumankindEmpire.Armies">HumankindEmpire.Armies</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <returns></returns>
        public static IEnumerable<Army> Armies(this IEnumerable<HumankindEmpire> sequence) =>
            sequence.SelectMany(empire => empire.Armies);
    }
}