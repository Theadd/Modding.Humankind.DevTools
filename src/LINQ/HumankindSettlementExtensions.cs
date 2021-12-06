
using System.Collections.Generic;
using System.Linq;
using Amplitude.Mercury.Data.Simulation;
using Modding.Humankind.DevTools.Core;

namespace Modding.Humankind.DevTools
{
    public static class HumankindSettlementExtensions
    {
        public static IEnumerable<HumankindSettlement> IsCapital(this IEnumerable<HumankindSettlement> sequence) =>
            sequence.Where(city => city.IsCapital);

        /// <summary>
        ///     Spawns a unit to the <c>Settlement</c>'s assigned spawn point for every <c>HumankindSettlement</c> in the sequence.
        /// </summary>
        /// <seealso cref="QuickAccess.UnitDefinitions"/>
        /// <seealso cref="HumankindSettlement.BuildUnit"/>
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
        /// <param name="unitDefinitionName">The name of the <c>UnitDefinition</c> to spawn a <c>Unit</c> from.</param>
        public static IEnumerable<HumankindSettlement> BuildUnitByName(this IEnumerable<HumankindSettlement> sequence,
            string unitDefinitionName) => sequence.BuildUnit(QuickAccess.UnitDefinitions
            .Where(unit => unit.name == unitDefinitionName).ElementAtOrDefault(0));

    }
}