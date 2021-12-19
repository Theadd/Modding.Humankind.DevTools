using System.Collections.Generic;
using System.Linq;
using Amplitude.Framework;
using Amplitude.Mercury.Data.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public static class QuickAccess
    {
        private static UnitDefinition[] _unitDefinitions = null;

        public static UnitDefinition[] UnitDefinitions =>
            _unitDefinitions ?? (_unitDefinitions = Databases
                .GetDatabase<ConstructibleDefinition>(false)
                .OfType<UnitDefinition>()
                .ToArray());
        
        // UITooltipClassDefinition
    }
}