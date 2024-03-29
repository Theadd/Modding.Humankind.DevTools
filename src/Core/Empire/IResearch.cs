﻿using Amplitude.Mercury.Data.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public interface IResearch
    {
        int CompletedTechnologiesCount { get; }
        int TechnologicalEraOffset { get; }
        int UnlockedTechnologiesCount { get; }
        int AvailableTechnologiesCount { get; }
        int ResearchNet { get; }
        int ResearchStock { get; set; }
        ResearchCostModifierDefinition AddResearchCostModifier(float value, bool isOperationTypeMult);
        void RemoveResearchCostModifier(ResearchCostModifierDefinition modifier);
    }
}
