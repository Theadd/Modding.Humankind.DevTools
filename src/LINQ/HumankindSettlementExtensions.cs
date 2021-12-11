
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
        public static IEnumerable<HumankindSettlement> IsCapital(this IEnumerable<HumankindSettlement> sequence, bool isCapital = true) =>
            sequence.Where(city => city.IsCapital == isCapital);
        
        public static IEnumerable<HumankindSettlement> IsCity(this IEnumerable<HumankindSettlement> sequence, bool isCity = true) =>
            sequence.Where(city => city.IsCity == isCity);
        
        public static IEnumerable<HumankindSettlement> IsOutpost(this IEnumerable<HumankindSettlement> sequence, bool isOutpost = true) =>
            sequence.Where(city => city.IsOutpost == isOutpost);

        public static IEnumerable<Territory> Territories(this IEnumerable<HumankindSettlement> sequence) =>
            sequence.SelectMany(settlement => settlement.Territories);
        
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