namespace Modding.Humankind.DevTools.Core
{
    public interface Research
    {
        int CompletedTechnologiesCount { get; }
        int TechnologicalEraOffset { get; }
        int UnlockedTechnologiesCount { get; }
        int AvailableTechnologiesCount { get; }
    }
}
