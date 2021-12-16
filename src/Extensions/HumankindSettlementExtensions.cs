
using System.Collections.Generic;
using System.Linq;
using System;
using Amplitude.Mercury.Data.Simulation;
using Modding.Humankind.DevTools.Core;
using Territory = Amplitude.Mercury.Interop.AI.Entities.Territory;
using Army = Amplitude.Mercury.Interop.AI.Entities.Army;

namespace Modding.Humankind.DevTools
{
    public static class HumankindSettlementExtensions
    {
        /// <summary>
        ///     Returns a new sequence of <c>HumankindSettlement</c>s containing only the capital cities from the given sequence.
        /// Set <c>isCapital</c> to false to invert the results. 
        /// See also: <see cref="HumankindSettlement.IsCapital">HumankindSettlement.IsCapital</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="isCapital">Set to false in order to get only those <c>HumankindSettlement</c>s which are not a capital, defaults to true.</param>
        /// <returns></returns>
        public static IEnumerable<HumankindSettlement> IsCapital(this IEnumerable<HumankindSettlement> sequence, bool isCapital = true) =>
            sequence.Where(city => city.IsCapital == isCapital);
        
        /// <summary>
        ///     Returns a new sequence that only contain settlements evolved to city. Set <c>isCity</c> to false to get the opposite results.
        /// See also: <see cref="HumankindSettlement.IsCity">HumankindSettlement.IsCity</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="isCity">When false, returns a sequence containing those <c>HumankindSettlement</c> that were not cities, defaults to true.</param>
        /// <returns></returns>
        public static IEnumerable<HumankindSettlement> IsCity(this IEnumerable<HumankindSettlement> sequence, bool isCity = true) =>
            sequence.Where(city => city.IsCity == isCity);

        /// <summary>
        ///     Returns a new sequence of <c>HumankindSettlement</c> only containing Outpost settlements from this sequence.
        /// Set <c>isOutpost</c> to false for the opposite results.
        /// See also: <see cref="HumankindSettlement.IsOutpost">HumankindSettlement.IsOutpost</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="isOutpost">Set to false to get the opposite results.</param>
        /// <returns></returns>        
        public static IEnumerable<HumankindSettlement> IsOutpost(this IEnumerable<HumankindSettlement> sequence, bool isOutpost = true) =>
            sequence.Where(city => city.IsOutpost == isOutpost);

        /// <summary>
        ///     Aggregates the territories of each <c>HumankindSettlement</c> in the sequence into a new sequence of territories.
        /// See also: <see cref="HumankindSettlement.Territories">HumankindSettlement.Territories</see>.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <returns><c>Territory</c> sequence.</returns>
        public static IEnumerable<Territory> Territories(this IEnumerable<HumankindSettlement> sequence) =>
            sequence.SelectMany(settlement => settlement.Territories);
        
        /// <summary>
        ///     Aggregates the Armies of each <c>HumankindSettlement</c> in the sequence into a new sequence of armies.
        /// See also: <see cref="HumankindSettlement.Armies">HumankindSettlement.Armies</see>.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IEnumerable<Army> Armies(this IEnumerable<HumankindSettlement> sequence) =>
            sequence.SelectMany(settlement => settlement.Armies);

        /// <summary>
        ///     Spawns a unit to the <c>Settlement</c>'s assigned spawn point for every <c>HumankindSettlement</c> in the sequence.
        /// </summary>
        /// <seealso cref="QuickAccess.UnitDefinitions"/>
        /// <seealso cref="HumankindSettlement.BuildUnit"/>
        /// <param name="sequence"></param>
        /// <param name="unitDefinition">The <c>UnitDefinition</c> to spawn a <c>Unit</c> from.</param>
        public static IEnumerable<HumankindSettlement> BuildUnit(this IEnumerable<HumankindSettlement> sequence, UnitDefinition unitDefinition)
        {
            foreach (var city in sequence)
                city.BuildUnit(unitDefinition);

            return sequence;
        }

        /// <summary>
        ///     Spawns a unit to the <c>Settlement</c>'s assigned spawn point for every <c>HumankindSettlement</c> in the sequence.
        /// </summary>
        /// <seealso cref="QuickAccess.UnitDefinitions"/>
        /// <seealso cref="HumankindSettlement.BuildUnit"/>
        /// <param name="sequence"></param>
        /// <param name="unitDefinitionName">The name of the <c>UnitDefinition</c> to spawn a <c>Unit</c> from.</param>
        public static IEnumerable<HumankindSettlement> BuildUnitByName(this IEnumerable<HumankindSettlement> sequence,
            string unitDefinitionName) => sequence.BuildUnit(QuickAccess.UnitDefinitions
            .Where(unit => unit.name == unitDefinitionName).ElementAtOrDefault(0));
        

        /// <summary>
        ///     This extension provides an easy way to iterate the sequence of <c>HumankindSettlements</c> one by one when pressing <c>[F3]</c> key while in-game.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="action">The action to be executed in every iteration, having <c>HumankindSettlement</c> as first parameter.</param>
        /// <returns></returns>
        public static IEnumerable<HumankindSettlement> Interactive(this IEnumerable<HumankindSettlement> sequence, Action<HumankindSettlement> action)
        {
            IEnumerator<HumankindSettlement> allSettlementsEnumerator = sequence.GetEnumerator();
            Action wrappedAction = null;
            
            wrappedAction = () =>
            {
                if (allSettlementsEnumerator.MoveNext())
                {
                    if (allSettlementsEnumerator.Current != null)
                    {
                        try
                        {
                            action(allSettlementsEnumerator.Current);
                        }
                        catch (Exception e)
                        {
                            Loggr.LogError(e.ToString());
                        }
                    }
                    else
                        Loggr.LogWarning("IEnumerator<HumankindSettlement>.Current is null in IEnumerable<HumankindSettlement> .Interactive(Action<HumankindSettlement> action)");
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
