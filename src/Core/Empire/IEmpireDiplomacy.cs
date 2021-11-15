using Amplitude.Mercury.Data.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public interface IEmpireDiplomacy
    {
        bool DeclareWarTo(int otherEmpireIndex);
        bool CanDeclareWarTo(int otherEmpireIndex);
        DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex);
        bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex);
        void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex);
    }
}
