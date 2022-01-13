using System.Reflection;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury.AI;
using Amplitude.Mercury.AI.Brain;
using Amplitude.Mercury.Simulation;

namespace Modding.Humankind.DevTools.Core
{
    public static partial class R
    {
        public static class Fields
        {
            public static readonly FieldInfo
                LastRunTurnField = GetField<AIController>("lastRunTurn", NonPublicInstance);

            public static readonly FieldInfo ControlledEmpireField =
                GetField<AIPlayer>("controlledEmpire", NonPublicInstance);

            public static readonly FieldInfo AIPlayerByEmpireIndexField =
                GetField<AIController>("aiPlayerByEmpireIndex", NonPublicInstance);

            public static readonly FieldInfo CurrentGameSpeedDefinitionField =
                GetField<GameSpeedController>("CurrentGameSpeedDefinition", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfTheTreasuryField =
                GetField<MajorEmpire>("DepartmentOfTheTreasury", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfScienceField =
                GetField<MajorEmpire>("DepartmentOfScience", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfCultureField =
                GetField<MajorEmpire>("DepartmentOfCulture", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfForeignAffairsField =
                GetField<MajorEmpire>("DepartmentOfForeignAffairs", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfDevelopmentField =
                GetField<MajorEmpire>("DepartmentOfDevelopment", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfTheInteriorField =
                GetField<Empire>("DepartmentOfTheInterior", NonPublicInstance);

            public static readonly FieldInfo DepartmentOfDefenseField =
                GetField<Empire>("DepartmentOfDefense", NonPublicInstance);
            
            public static readonly FieldInfo DepartmentOfTransportationField =
                GetField<Empire>("DepartmentOfTransportation", NonPublicInstance);

            public static readonly FieldInfo PersonaNameField = GetField<MajorEmpire>("PersonaName", NonPublicInstance);

            public static readonly FieldInfo PersonaQualityField =
                GetField<MajorEmpire>("PersonaQuality", NonPublicInstance);

            public static readonly FieldInfo FixedPointRawValueField =
                GetField<Property>("FixedPointRawValue", NonPublicInstance);

            public static readonly FieldInfo ArchetypesField = GetField<MajorEmpire>("Archetypes", NonPublicInstance);

            public static readonly FieldInfo DiplomaticRelationByOtherEmpireIndexField =
                GetField<MajorEmpire>("DiplomaticRelationByOtherEmpireIndex", NonPublicInstance);
            
            public static readonly FieldInfo CurrentStateField = GetField<DiplomaticRelation>("CurrentState", NonPublicInstance);

            public static readonly FieldInfo TradeNodesField = GetField<MajorEmpire>("TradeNodes", NonPublicInstance);

            public static readonly FieldInfo SettlementsField = GetField<Empire>("Settlements", NonPublicInstance);
            
            public static readonly FieldInfo DataUtilsField = 
                typeof(Amplitude.Mercury.UI.Utils).GetField("DataUtils", NonPublicStatic);
        }
    }
}