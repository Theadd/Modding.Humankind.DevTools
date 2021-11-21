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
        public WorldPosition WorldPosition
        {
            get
            {
                if (Settlement.WorldPosition != SettlementEntity.Position)
                    Loggr.LogError("WORLD POSITIONS DIFFER!!");

                return Settlement.WorldPosition;
            }
        }

        public bool IsCapital => SettlementEntity.IsCapital;

        public bool IsCity => SettlementEntity.SettlementStatus == SettlementStatuses.City;
        
        public bool IsOutpost => SettlementEntity.SettlementStatus == SettlementStatuses.Camp;

        public new int EmpireIndex => base.EmpireIndex;

        public Territory[] Territories => SettlementEntity.Territories;

        public IEnumerable<Army> Armies => Territories.SelectMany(territory => territory.Armies);

        public new HumankindEmpire Empire => base.Empire;

        public int Population
        {
            get => (int)Settlement.Population.Value;
            set => AddOrRemovePopulation(value - ((int) Settlement.Population.Value));
        }

        public new Unit BuildUnit(UnitDefinition unitDefinition) => base.BuildUnit(unitDefinition);
        
        public static HumankindSettlement Create(Settlement settlementEntity, Amplitude.Mercury.Simulation.Settlement settlement) =>
            new HumankindSettlement { SettlementEntity = settlementEntity, Settlement = settlement };

        public bool Equals(HumankindSettlement other) =>
            other != null && WorldPosition.ToTileIndex() == other.WorldPosition.ToTileIndex();
    }
}
