using Amplitude.Mercury.Interop.AI.Entities;

namespace Modding.Humankind.DevTools.Core
{
    public abstract class GameSettlementBase
    {
        protected Settlement SettlementEntity { get; set; }
        protected Amplitude.Mercury.Simulation.Settlement SettlementSimulation { get; set; }
    }
}
