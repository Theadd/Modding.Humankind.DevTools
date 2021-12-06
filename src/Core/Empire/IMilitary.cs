namespace Modding.Humankind.DevTools.Core
{
    public interface IMilitary
    {
        int UnitCount { get; }
        int MilitaryUpkeep { get; }
        int ArmyCount { get; }
        int RentedArmyCount { get; }
        int CombatStrength { get; }
        int GroundCombatStrength { get; }
        int NavalCombatStrength { get; }
        int AerialCombatStrength { get; }
        int ArmyMaximumSize { get; }
    }
}
