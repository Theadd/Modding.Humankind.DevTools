using Amplitude.Mercury.Interop.AI.Entities;

namespace Modding.Humankind.DevTools.Core
{
    public abstract class GameEmpireBase
    {
        protected MajorEmpire MajorEmpire { get; set; }
        protected Amplitude.Mercury.Simulation.MajorEmpire MajorEmpireSimulation { get; set; }
    }
}