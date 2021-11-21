using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Amplitude.Mercury.Interop.AI.Entities;

namespace Modding.Humankind.DevTools.Core
{
    public abstract class GameEmpireBase
    {
        protected MajorEmpire MajorEmpireEntity { get; set; }
        protected Amplitude.Mercury.Simulation.MajorEmpire MajorEmpireSimulation { get; set; }
        public IReadOnlyList<HumankindSettlement> Settlements => new SettlementList(MajorEmpireEntity, MajorEmpireSimulation);
    }
}