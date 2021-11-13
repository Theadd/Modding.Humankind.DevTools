using Amplitude.Mercury.Data.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public interface IEmpireDiplomacy
    {
        new bool DeclareWarTo(int otherEmpireIndex);
        new bool CanDeclareWarTo(int otherEmpireIndex);
        new DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex);
        new bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex);
        new void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex);
    }
}
