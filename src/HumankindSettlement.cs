using System;
using System.Collections.Generic;
using System.Linq;
using Amplitude.Mercury;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Simulation;
using Modding.Humankind.DevTools.Core;
using Army = Amplitude.Mercury.Interop.AI.Entities.Army;
using Settlement = Amplitude.Mercury.Interop.AI.Entities.Settlement;
using Territory = Amplitude.Mercury.Interop.AI.Entities.Territory;

namespace Modding.Humankind.DevTools
{
    public class HumankindSettlement : SettlementAbstraction, IEquatable<HumankindSettlement>
    {
        /// <summary>
        ///     <c>Settlement</c>'s WorldPosition.
        /// </summary>
        public WorldPosition WorldPosition => SettlementSimulation.WorldPosition;

        /// <summary>
        ///     Whether this settlement is the capital city of the <c>Empire</c>.
        /// </summary>
        public bool IsCapital => SettlementEntity.IsCapital;

        /// <summary>
        ///     Whether this <c>Settlement</c> is a city or not.
        /// </summary>
        public bool IsCity => SettlementEntity.SettlementStatus == SettlementStatuses.City;
        
        /// <summary>
        ///     Whether this <c>Settlement</c> is an **Outpost** or not.
        /// </summary>
        public bool IsOutpost => SettlementEntity.SettlementStatus == SettlementStatuses.Camp;

        public new int EmpireIndex => base.EmpireIndex;

        /// <summary>
        ///     All territories that are part of this <c>Settlement</c>.
        /// </summary>
        public Territory[] Territories => SettlementEntity.Territories;

        /// <summary>
        ///     All armies found within all territories annexed/controlled by this <c>Settlement</c>.
        /// </summary>
        public IEnumerable<Army> Armies => Territories.SelectMany(territory => territory.Armies);

        /// <summary>
        ///     The <see cref="HumankindEmpire">HumankindEmpire</see> controlling this <c>Settlement</c>.
        /// </summary>
        public new HumankindEmpire Empire => base.Empire;

        /// <summary>
        ///     Get or set <c>Settlement</c>'s total population.
        /// </summary>
        public int Population
        {
            get => (int)SettlementSimulation.Population.Value;
            set => AddOrRemovePopulation(value - ((int) SettlementSimulation.Population.Value));
        }

        /// <summary>
        ///     Spawns a unit to <c>Settlement</c>'s assigned spawn point based on <c>UnitDefinition</c>'s <c>UnitSpawnType</c>.
        /// </summary>
        /// <seealso cref="QuickAccess.UnitDefinitions"/>
        /// <param name="unitDefinition">The <c>UnitDefinition</c> to spawn a <c>Unit</c> from.</param>
        /// <returns><c>Unit</c></returns>
        public new Unit BuildUnit(UnitDefinition unitDefinition) => base.BuildUnit(unitDefinition);

        /// <summary>
        ///     Center main camera view to this <c>Settlement</c>'s TileIndex.
        /// </summary>
        public void CenterToCamera() => HumankindGame.CenterCameraAt(WorldPosition.ToTileIndex());
        
        public static HumankindSettlement Create(Settlement settlementEntity, Amplitude.Mercury.Simulation.Settlement settlement) =>
            new HumankindSettlement { SettlementEntity = settlementEntity, SettlementSimulation = settlement };

        public bool Equals(HumankindSettlement other) =>
            other != null && WorldPosition.ToTileIndex() == other.WorldPosition.ToTileIndex();
    }
}
