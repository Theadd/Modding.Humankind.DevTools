namespace Modding.Humankind.DevTools.Core
{
    public interface Military
    {
        int UnitCount { get; }
        int MilitaryUpkeep { get; }
        int ArmyCount { get; }
        int RentedArmyCount { get; }
        int CombatStrength { get; }
    }
}
