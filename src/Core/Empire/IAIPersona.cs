using Amplitude.Mercury.Data.AI;

namespace Modding.Humankind.DevTools.Core
{
    public interface IAIPersona
    {
        string PersonaName { get; }
        int PersonaQuality { get; }
        Archetype Archetypes { get; }
        Archetype[] ArchetypesArray { get; }
        bool HasArchetype(Archetype target);
        void SetArchetype(Archetype target, bool remove = false);
    }
}
