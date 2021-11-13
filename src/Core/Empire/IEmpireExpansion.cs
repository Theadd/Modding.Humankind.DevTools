namespace Modding.Humankind.DevTools.Core
{
    public interface IEmpireExpansion
    {
        int TerritoryCount { get; }
        int OutpostCount { get; }
        int CityCount { get; }
        int CityCap { get; }
        int OccupiedCityCount { get; }
        int EmpirePopulation { get; }
        int SettlementsPopulation { get; }
    }
}
