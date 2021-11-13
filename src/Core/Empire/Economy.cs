namespace Modding.Humankind.DevTools.Core
{
    public interface Economy
    {
        new int TradeNodesCount { get; }
        int LuxuryResourcesAccessCount { get; }
        int StrategicResourcesAccessCount { get; }
        int MoneyNet { get; }
        int InfluenceNet { get; }
        int MoneyStock { get; set; }
        int InfluenceStock { get; set; }
    }
}
