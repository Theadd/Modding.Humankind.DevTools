using Amplitude;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public abstract class SettlementAbstraction : GameSettlementBase
    {
        protected int EmpireIndex => SettlementEntity.Empire.EmpireIndex;
        protected HumankindEmpire Empire => HumankindGame.Empires[EmpireIndex];
        
        protected void AddOrRemovePopulation(int population, bool raiseSimulationEvents = true)
        {
            R.Methods.AddOrRemovePopulationToSettlementMethod.Invoke(Empire.DepartmentOfTheInterior,
                new object[] {FixedPoint.Zero + population, SettlementSimulation, raiseSimulationEvents});
        }

        protected Unit BuildUnit(UnitDefinition unitDefinition) =>
            (Unit) R.Methods.BuildUnitAtMethod.Invoke(Empire.DepartmentOfDefense,
                new object[] {unitDefinition, SettlementSimulation});
    }
}