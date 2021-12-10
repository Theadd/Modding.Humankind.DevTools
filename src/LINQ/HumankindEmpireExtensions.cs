
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modding.Humankind.DevTools
{
    public static class HumankindEmpireExtensions
    {
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
        ///     Executes an action for each <c>HumankindEmpire</c> in the sequence.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="action">Action to execute, where first parameter is each <c>HumankindEmpire</c>.</param>
        public static IEnumerable<HumankindEmpire> Execute(this IEnumerable<HumankindEmpire> sequence,
            Action<HumankindEmpire> action)
        {
            foreach (var humankindEmpire in sequence)
            {
                action(humankindEmpire);
            }

            return sequence;
        }
    }
}