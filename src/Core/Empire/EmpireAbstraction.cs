using Amplitude;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury.Data.AI;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Interop;
using Amplitude.Mercury.Simulation;
using UnityEngine;

namespace Modding.Humankind.DevTools.Core
{
    public abstract class EmpireAbstraction : GameEmpireBase
    {
        // Protected method calls that make changes to the current state of the game

        # region Protected method calls

        protected void GainMoney(int value)
        {
            R.Methods.GainMoneyMethod.Invoke(DepartmentOfTheTreasury, new object[] {FixedPoint.Zero + value});
        }

        protected void GainResearch(int value, bool raiseSimulationEvents = true, bool bypassEra0Lock = false)
        {
            R.Methods.GainResearchMethod.Invoke(DepartmentOfScience,
                new object[] {FixedPoint.Zero + value, raiseSimulationEvents, bypassEra0Lock});
        }

        protected void GainInfluence(int value)
        {
            R.Methods.GainInfluenceMethod.Invoke(DepartmentOfCulture, new object[] {FixedPoint.Zero + value});
        }

        protected void ProcessOrderChangeArchetypes(Archetype archetypes)
        {
            R.Methods.ProcessOrderChangeArchetypesMethod
                .Invoke(DepartmentOfDevelopment, new object[] {new OrderChangeArchetypes {Archetypes = archetypes}});
        }

        // Diplomacy
        protected bool DeclareWarTo(int otherEmpireIndex)
        {
            OrderDeclareAnyWar order = new OrderDeclareAnyWar() {OtherEmpireIndex = otherEmpireIndex};

            if ((bool) R.Methods.ValidateOrderDeclareAnyWarMethod.Invoke(DepartmentOfForeignAffairs,
                new object[] {order}))
            {
                R.Methods.ProcessOrderDeclareAnyWarMethod.Invoke(DepartmentOfForeignAffairs, new object[] {order});
                return true;
            }

            return false;
        }

        protected bool CanDeclareWarTo(int otherEmpireIndex)
        {
            OrderDeclareAnyWar order = new OrderDeclareAnyWar() {OtherEmpireIndex = otherEmpireIndex};

            return (bool) R.Methods.ValidateOrderDeclareAnyWarMethod.Invoke(DepartmentOfForeignAffairs,
                new object[] {order});
        }

        protected DiplomaticRelation DiplomaticRelationTo(int otherEmpireIndex)
        {
            return ((DiplomaticRelation[]) R.Fields.DiplomaticRelationByOtherEmpireIndexField
                .GetValue(MajorEmpireSimulation))[otherEmpireIndex];
        }

        protected DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex) =>
            (DiplomaticStateType) R.Fields.CurrentStateField.GetValue(DiplomaticRelationTo(otherEmpireIndex));

        protected bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) =>
            (bool) R.Methods.ValidateOrderDiplomaticActionMethod.Invoke(DepartmentOfForeignAffairs,
                new object[]
                    {new OrderDiplomaticAction() {OtherEmpireIndex = otherEmpireIndex, DiplomaticAction = action}});

        protected void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) =>
            R.Methods.ProcessOrderDiplomaticActionMethod.Invoke(DepartmentOfForeignAffairs,
                new object[]
                    {new OrderDiplomaticAction() {OtherEmpireIndex = otherEmpireIndex, DiplomaticAction = action}});

        protected void EnableFogOfWar(bool enable) =>
            R.Methods.ProcessOrderEnableFogOfWarMethod.Invoke(DepartmentOfTheInterior,
                new object[] {new OrderEnableFogOfWar {Enable = enable}});
                
        // Cost modifiers

        protected void AddResearchCostModifier(float costModifierValue,
            CostModifierDefinition.OperationTypes operationType)
        {
            ResearchCostModifierDefinition orderInstance =
                ScriptableObject.CreateInstance<ResearchCostModifierDefinition>();

            orderInstance.TargetType = ResearchCostModifierDefinition.TechnologyTargetTypes.AllTechnologies;
            orderInstance.ApplyIfCostZero = true;
            orderInstance.CanBeCumulated = true;
            orderInstance.CostModifierValue = FixedPoint.Zero + costModifierValue;
            orderInstance.CostType = CostModifierDefinition.CostTypes.Research;
            orderInstance.IsObsolete = false;
            orderInstance.OperationType = operationType;

            R.Methods.AddCostModifierToMajorEmpireMethod.Invoke(DepartmentOfTheTreasury,
                new object[] {orderInstance});
        }
        
        protected void AddConstructibleCostModifier(float costModifierValue,
            CostModifierDefinition.OperationTypes operationType)
        {
            ConstructibleCostModifierDefinition orderInstance =
                ScriptableObject.CreateInstance<ConstructibleCostModifierDefinition>();

            orderInstance.TargetType = ConstructibleCostModifierDefinition.TargetTypes.AllConstructibles;
            orderInstance.ApplyIfCostZero = true;
            orderInstance.CanBeCumulated = true;
            orderInstance.CostModifierValue = FixedPoint.Zero + costModifierValue;
            orderInstance.CostType = CostModifierDefinition.CostTypes.Production;
            orderInstance.IsObsolete = false;
            orderInstance.OperationType = operationType;

            R.Methods.AddCostModifierToMajorEmpireMethod.Invoke(DepartmentOfTheTreasury,
                new object[] {orderInstance});
        }

        # endregion Protected method calls

        
        // Protected field accessors

        # region Protected fields

        // Agencies
        protected DepartmentOfTheTreasury DepartmentOfTheTreasury =>
            (DepartmentOfTheTreasury) R.Fields.DepartmentOfTheTreasuryField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfScience DepartmentOfScience =>
            (DepartmentOfScience) R.Fields.DepartmentOfScienceField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfCulture DepartmentOfCulture =>
            (DepartmentOfCulture) R.Fields.DepartmentOfCultureField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfForeignAffairs DepartmentOfForeignAffairs =>
            (DepartmentOfForeignAffairs) R.Fields.DepartmentOfForeignAffairsField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfDevelopment DepartmentOfDevelopment =>
            (DepartmentOfDevelopment) R.Fields.DepartmentOfDevelopmentField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfTheInterior DepartmentOfTheInterior =>
            (DepartmentOfTheInterior) R.Fields.DepartmentOfTheInteriorField.GetValue(MajorEmpireSimulation);

        protected DepartmentOfDefense DepartmentOfDefense =>
            (DepartmentOfDefense) R.Fields.DepartmentOfDefenseField.GetValue(MajorEmpireSimulation);

        
        // Other empire's values
        protected string PersonaName => (string) R.Fields.PersonaNameField.GetValue(MajorEmpireSimulation);
        
        protected int PersonaQuality => (int) R.Fields.PersonaQualityField.GetValue(MajorEmpireSimulation);

        protected Archetype Archetypes => (Archetype) R.Fields.ArchetypesField.GetValue(MajorEmpireSimulation);

        protected int TradeNodesCount =>
            ((ReferenceCollection<TradeNode>) R.Fields.TradeNodesField.GetValue(MajorEmpireSimulation)).Count;

        # endregion Protected fields
    }
}