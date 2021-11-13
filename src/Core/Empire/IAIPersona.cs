using Amplitude.Mercury.Data.AI;

namespace Modding.Humankind.DevTools.Core
{
    public interface IAIPersona
    {
        new string PersonaName { get; }
        new int PersonaQuality { get; }
        new Archetype Archetypes { get; }
        Archetype[] ArchetypesArray { get; }
        bool HasArchetype(Archetype target);
        void SetArchetype(Archetype target, bool remove = false);
    }
}
