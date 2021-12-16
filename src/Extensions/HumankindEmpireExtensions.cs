
using System;
using System.Collections.Generic;
using System.Linq;
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
        ///     Executes an action for each <c>HumankindEmpire</c> in the sequence.
        /// </summary>
        /// <param name="sequence">this</param>
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

        /// <summary>
        ///     This extension provides an easy way to iterate the sequence of <c>HumankindEmpire</c> one by one when pressing <c>[F3]</c> key while in-game.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="action">The action to be executed in every iteration, having <c>HumankindEmpire</c> as first parameter.</param>
        /// <returns></returns>
        public static IEnumerable<HumankindEmpire> Interactive(this IEnumerable<HumankindEmpire> sequence, Action<HumankindEmpire> action)
        {
            IEnumerator<HumankindEmpire> allEmpiresEnumerator = sequence.GetEnumerator();
            Action wrappedAction = null;
            
            wrappedAction = () =>
            {
                if (allEmpiresEnumerator.MoveNext())
                {
                    if (allEmpiresEnumerator.Current != null)
                    {
                        try
                        {
                            action(allEmpiresEnumerator.Current);
                        }
                        catch (Exception e)
                        {
                            Loggr.LogError(e.ToString());
                        }
                    }
                    else
                        Loggr.LogWarning("IEnumerator<HumankindEmpire>.Current is null in IEnumerable<HumankindEmpire> .Interactive(Action<HumankindEmpire> action)");
                }
                else
                {
                    HumankindDevTools.OnIterateNext -= wrappedAction;
                }
            };

            HumankindDevTools.OnIterateNext -= wrappedAction;
            HumankindDevTools.OnIterateNext += wrappedAction;

            return sequence;
        }
    }
}