using System.Reflection;
using Amplitude;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Interop;
using Amplitude.Mercury.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public static partial class R
    {
        public static class Methods
        {
            public static readonly MethodInfo GainMoneyMethod =
                GetMethod<DepartmentOfTheTreasury>("GainMoney", PublicInstance, new[] {typeof(FixedPoint)});

            public static readonly MethodInfo GainResearchMethod = GetMethod<DepartmentOfScience>("GainResearch",
                NonPublicInstance, new[] {typeof(FixedPoint), typeof(bool), typeof(bool)});

            public static readonly MethodInfo GainInfluenceMethod =
                GetMethod<DepartmentOfCulture>("GainInfluence", NonPublicInstance, new[] {typeof(FixedPoint)});

            public static MethodInfo ProcessOrderChangeArchetypesMethod =
                GetMethod<DepartmentOfDevelopment>("ProcessOrderChangeArchetypes", NonPublicInstance,
                    new[] {typeof(OrderChangeArchetypes)});
            
            public static readonly MethodInfo ValidateOrderDeclareAnyWarMethod = 
                GetMethod<DepartmentOfForeignAffairs>("ValidateOrderDeclareAnyWar", NonPublicInstance,
                    new[] {typeof(OrderDeclareAnyWar)});
            
            public static readonly MethodInfo ProcessOrderDeclareAnyWarMethod = 
                GetMethod<DepartmentOfForeignAffairs>("ProcessOrderDeclareAnyWar", NonPublicInstance,
                    new[] {typeof(OrderDeclareAnyWar)});

            public static readonly MethodInfo ValidateOrderDiplomaticActionMethod =
                GetMethod<DepartmentOfForeignAffairs>("ValidateOrderDiplomaticAction", NonPublicInstance,
                    new[] {typeof(OrderDiplomaticAction)});
            
            public static readonly MethodInfo ProcessOrderDiplomaticActionMethod =
                GetMethod<DepartmentOfForeignAffairs>("ProcessOrderDiplomaticAction", NonPublicInstance,
                    new[] {typeof(OrderDiplomaticAction)});
            
            public static MethodInfo ProcessOrderEnableFogOfWarMethod =
                GetMethod<DepartmentOfTheInterior>("ProcessOrderEnableFogOfWar", NonPublicInstance,
                    new[] {typeof(OrderEnableFogOfWar)});

            public static readonly MethodInfo AddCostModifierToMajorEmpireMethod = 
                GetMethod<DepartmentOfTheTreasury>("AddCostModifierToMajorEmpire", PublicInstance,
                    new[] {typeof(CostModifierDefinition)});
            
            public static readonly MethodInfo RemoveCostModifierFromMajorEmpireMethod = 
                GetMethod<DepartmentOfTheTreasury>("RemoveCostModifierFromMajorEmpire", PublicInstance,
                    new[] {typeof(CostModifierDefinition)});

            public static readonly MethodInfo AddOrRemovePopulationToSettlementMethod = 
                GetMethod<DepartmentOfTheInterior>("AddOrRemovePopulationToSettlement", NonPublicInstance,
                    new[] {typeof(FixedPoint), typeof(Settlement), typeof(bool)});
            
            public static readonly MethodInfo BuildUnitAtMethod = 
                GetMethod<DepartmentOfDefense>("BuildUnitAt", NonPublicInstance,
                    new[] {typeof(UnitDefinition), typeof(Settlement)});
        }
    }
}