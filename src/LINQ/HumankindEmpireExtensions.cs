
using System.Collections.Generic;
using System.Linq;

namespace Modding.Humankind.DevTools
{
    public static class HumankindEmpireExtensions
    {
        public static IEnumerable<HumankindEmpire> IsControlledByHuman(this IEnumerable<HumankindEmpire> sequence) =>
            sequence.Where(empire => empire.IsControlledByHuman);

        public static IEnumerable<HumankindSettlement> Settlements(this IEnumerable<HumankindEmpire> sequence) =>
            sequence.SelectMany(empire => empire.Settlements);
    }
}