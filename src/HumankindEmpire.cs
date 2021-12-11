using System.Collections.Generic;
using Amplitude.Mercury.Data.AI;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Sandbox;
using Amplitude.Mercury.Simulation;
using Modding.Humankind.DevTools.Core;
using MajorEmpire = Amplitude.Mercury.Interop.AI.Entities.MajorEmpire;

namespace Modding.Humankind.DevTools
{
    /// <summary>
    ///     A simplified interface (Facade) that masks a complex underlying structure of game classes
    /// in a single, simplified and well documented class to access and, where possible, edit most
    /// significant values related to a game Empire.
    /// </summary>
    public class HumankindEmpire : EmpireAbstraction, IEmpireDiplomacy, IMilitary, IResearch, IEmpireEconomy, IAIPersona, IEmpireExpansion
    {
        
        /// <summary>
        ///     Position of this empire within current game's array of empires.
        /// </summary>
        public int EmpireIndex => MajorEmpireEntity.EmpireIndex;
        
        /// <summary>
        ///     List of <c>HumankindSettlement</c>s controlled by this empire.
        /// </summary>
        public new IEnumerable<HumankindSettlement> Settlements => base.Settlements;

        /// <summary>
        ///     Armies controlled by this empire.
        /// </summary>
        public IEnumerable<Amplitude.Mercury.Interop.AI.Entities.Army> Armies => MajorEmpireEntity.Armies;

        /// <summary>
        ///     Total Fame currently accumulated by this empire.
        /// </summary>
        public int Fame => (int) MajorEmpireSimulation.FameScore.Value;

        /// <summary>
        ///     Number of technologies already researched by this empire.
        /// </summary>
        public int CompletedTechnologiesCount => MajorEmpireEntity.CompletedTechnologyCount;

        /// <summary>
        ///     Number of technological eras available ahead.
        /// </summary>
        public int TechnologicalEraOffset => (int) MajorEmpireSimulation.TechnologicalEraOffset.Value;

        /// <summary>
        ///     Unlocked technologies.
        /// </summary>
        public int UnlockedTechnologiesCount => (int) MajorEmpireSimulation.NumberOfUnlockedTechnologies.Value;

        /// <summary>
        ///     Available technologies.
        /// </summary>
        public int AvailableTechnologiesCount => (int) MajorEmpireSimulation.NumberOfAvailableTechnologies.Value;
        
        /// <summary>
        ///     This empire's AI persona.
        /// </summary>
        public new string PersonaName => base.PersonaName;

        /// <summary>
        ///     Quantity of points which determine the score (and difficulty level) of this empire's AI persona.
        /// </summary>
        public new int PersonaQuality => base.PersonaQuality;

        /// <summary>
        ///     Total number of territories controlled by this empire (outposts, cities and their attached territories).
        /// </summary>
        public int TerritoryCount => (int) MajorEmpireSimulation.TerritoryCount.Value;

        /// <summary>
        ///     Number of outposts currently controlled by this empire.
        /// </summary>
        public int OutpostCount => MajorEmpireEntity.Camps.Length;

        /// <summary>
        ///     Number of cities currently controlled by this empire.
        /// </summary>
        public int CityCount => (int) MajorEmpireSimulation.CityCount.Value;

        /// <summary>
        ///     Maximum number of cities this empire can currently control without negative effects.
        /// </summary>
        public int CityCap => (int) MajorEmpireSimulation.CityCap.Value;

        /// <summary>
        ///     Empire's number of occupied cities.
        /// </summary>
        public int OccupiedCityCount => (int) MajorEmpireSimulation.OccupiedCityCount.Value;

        /// <summary>
        ///     Number of units among all empire's armies.
        /// </summary>
        public int UnitCount => (int) MajorEmpireSimulation.SumOfUnits.Value;

        /// <summary>
        ///     Sum of armies upkeep amount of money.
        /// </summary>
        public int MilitaryUpkeep => (int) MajorEmpireSimulation.UnitCollectionUpkeep.Value;

        /// <summary>
        ///     Number of armies controlled by this empire.
        /// </summary>
        public int ArmyCount => MajorEmpireEntity.Armies.Length;

        /// <summary>
        ///     Number of armies controlled by this empire.
        /// </summary>
        public int RentedArmyCount => (int) MajorEmpireSimulation.RentedArmyCount.Value;

        /// <summary>
        ///     <c>EmpirePopulation</c> equals to the sum of <c>SettlementsPopulation</c> with <c>UnitCount</c>.
        /// </summary>
        public int EmpirePopulation => (int) MajorEmpireSimulation.SumOfPopulationAndUnits.Value;

        /// <summary>
        ///     Population in cities or outposts (Citizens).
        /// </summary>
        public int SettlementsPopulation => (int) MajorEmpireSimulation.SumOfPopulation.Value;

        /// <summary>
        ///     Whether this empire has an ongoing battle active.
        /// </summary>
        public bool IsInBattle => MajorEmpireSimulation.IsInBattle;

        /// <summary>
        ///     Whether this empire is being controlled by the AI or by a human player.
        /// </summary>
        /// <seealso cref="IsControlledByHuman" />
        public bool IsAIActivated => !MajorEmpireEntity.IsControlledByHuman;

        /// <summary>
        ///     Whether this empire is being controlled by a human player or by the AI.
        /// </summary>
        /// <seealso cref="IsAIActivated" />
        public bool IsControlledByHuman => MajorEmpireEntity.IsControlledByHuman;

        /// <summary>
        ///     Computed empire's current military power.
        /// </summary>
        public int CombatStrength => (int) MajorEmpireEntity.CombatStrength;

        /// <summary>
        ///     Computed empire's current ground military power.
        /// </summary>
        public int GroundCombatStrength => (int) MajorEmpireEntity.GroundCombatStrength;

        /// <summary>
        ///     Computed empire's current naval military power.
        /// </summary>
        public int NavalCombatStrength => (int) MajorEmpireEntity.NavalCombatStrength;

        /// <summary>
        ///     Computed empire's current aerial military power.
        /// </summary>
        public int AerialCombatStrength => (int) MajorEmpireEntity.AerialCombatStrength;

        /// <summary>
        ///     Empire's maximum army size.
        /// </summary>
        public int ArmyMaximumSize => (int) MajorEmpireEntity.ArmyMaximumSize;

        /// <summary>
        ///     Sum of trade nodes.
        /// </summary>
        public new int TradeNodesCount => base.TradeNodesCount;

        /// <summary>
        ///     Equals to the sum of public order of cities controlled by this empire divided by the number of cities controlled by this empire.
        /// </summary>
        public int Stability => (int) MajorEmpireSimulation.Stability.Value;

        /// <summary>
        ///     This empire's current Era as number, where 0 is Neolithic.
        /// </summary>
        public int EraLevel => (int) MajorEmpireSimulation.EraLevel.Value;

        /// <summary>
        ///     Equals to the sum of stars obtained by this empire in all eras.
        /// </summary>
        public int SumOfEraStars => (int) MajorEmpireSimulation.SumOfEraStars.Value;

        /// <summary>
        ///     Number of accesses to Luxury Resources.
        /// </summary>
        public int LuxuryResourcesAccessCount => (int) MajorEmpireSimulation.SumOfLuxuryResourceAccessCount.Value;
        
        /// <summary>
        ///     Number of accesses to Strategic Resources.
        /// </summary>
        public int StrategicResourcesAccessCount => (int) MajorEmpireSimulation.SumOfStrategicResourceAccessCount.Value;

        /// <summary>
        ///     Money net income per turn which is added to <see cref="HumankindEmpire.MoneyStock">MoneyStock</see> at the end of turn phase.
        /// </summary>
        public int MoneyNet => (int) MajorEmpireSimulation.MoneyNet.Value;

        /// <summary>
        ///     Research net income per turn.
        /// </summary>
        public int ResearchNet => (int) MajorEmpireSimulation.ResearchNet.Value;

        /// <summary>
        ///     Influence net income per turn.
        /// </summary>
        public int InfluenceNet => (int) MajorEmpireSimulation.InfluenceNet.Value;

        /// <summary>
        ///     Gets or sets the amount of money for this empire.
        /// </summary>
        /// <remarks>
        ///     If you set this to another value, remember that it is the absolute value to be expected for
        ///     this empire after this action takes effect. Tip: Use `+=` operator to avoid loosing money.
        /// </remarks>
        public int MoneyStock
        {
            get => (int) MajorEmpireSimulation.MoneyStock.Value;
            set => GainMoney(value - MoneyStock);
        }

        /// <summary>
        ///     Gets or sets the *accumulated* research of this empire.
        /// </summary>
        /// <remarks>
        ///     This will always return 0 since science doesn't get accumulated anywhere, it is automatically consumed by the technology research queue.
        /// </remarks>
        public int ResearchStock
        {
            get => 0;
            set => GainResearch(value);
        }

        /// <summary>
        ///     Gets or sets the accumulated influence of this empire.
        /// </summary>
        /// <remarks>
        ///     If you set this to another value, remember that it is the absolute value to be expected for this empire after this action takes effect. Tip: Use `+=` operator.
        /// </remarks>
        public int InfluenceStock
        {
            get => (int) MajorEmpireSimulation.InfluenceStock.Value;
            set => GainInfluence(value - InfluenceStock);
        }

        /// <summary>
        ///     Gets this empire's <c>Archetype</c> bitmask.
        /// </summary>
        public new Archetype Archetypes => base.Archetypes;

        /// <summary>
        ///     Whether this empire's <c>Archetype</c> bitmask contains the given <c>Archetype</c>.
        /// </summary>
        /// <param name="target">The <c>Archetype</c> to look for</param>
        /// <returns>Boolean that indicates if given <c>Archetype</c> was found</returns>
        public bool HasArchetype(Archetype target) => (uint) (base.Archetypes & target) == (uint) target;

        /// <summary>
        ///     Add or remove given <c>Archetype</c> from this empire's <c>Archetype</c> bitmask.
        /// </summary>
        /// <param name="target">The <c>Archetype</c> to add or remove from the <c>Archetype</c> bitmask</param>
        /// <param name="remove">Whether to add or remove it from the bitmask</param>
        public void SetArchetype(Archetype target, bool remove = false) =>
            ProcessOrderChangeArchetypes(remove ? Archetypes & ~target : Archetypes | target);

        /// <summary>
        ///     From this empire's <c>Archetype</c> bitmask, extracts each assigned <c>Archetype</c> as an element of the returned <c>Archetype</c> array.
        /// </summary>
        /// <returns><c>Archetype[]</c></returns>
        public Archetype[] ArchetypesArray => GameEmpireHelper.ArchetypesToArray(Archetypes);

        /// <summary>
        ///     Declare war to another empire if possible. Gives priority to a surprise war before a formal war.
        /// </summary>
        /// <param name="otherEmpireIndex">Other empire's <c>EmpireIndex</c>.</param>
        /// <returns>Whether war was declared or not.</returns>
        public new bool DeclareWarTo(int otherEmpireIndex) => base.DeclareWarTo(otherEmpireIndex);
        
        /// <summary>
        ///     Whether war can be declared or not to the given empire.
        /// </summary>
        /// <param name="otherEmpireIndex">Other empire's <c>EmpireIndex</c>.</param>
        /// <returns>Whether war can be declared or not.</returns>
        public new bool CanDeclareWarTo(int otherEmpireIndex) => base.CanDeclareWarTo(otherEmpireIndex);

        /// <summary>
        ///     Empire's current <c>DiplomaticStateType</c> with the given empire. See included example for Amplitude's <c>DiplomaticStateType</c> implementation.
        /// </summary>
        /// <example>
        ///     Implementation code of <c>Amplitude.Mercury.Data.Simulation.DiplomaticStateType</c>.
        /// <code>
        /// public enum DiplomaticStateType
        /// {
        ///     Unknown,
        ///     PartialyKnown,
        ///     Peace,
        ///     Alliance,
        ///     VassalToLiege,
        ///     VassalToFellowVassal,
        ///     VassalToExternal,
        ///     War,
        ///     PartialyEliminated,
        ///     BothEliminated
        /// }
        /// </code>
        /// </example>
        /// <param name="otherEmpireIndex">Other empire's <c>EmpireIndex</c>.</param>
        /// <returns><c>DiplomaticStateType</c></returns>
        public new DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex) =>
            base.DiplomaticStateTypeTo(otherEmpireIndex);

        /// <summary>
        ///     Validates if given <c>DiplomaticAction</c> can be executed against given empire's <c>EmpireIndex</c>.
        /// </summary>
        /// <example>
        ///     List of available <c>DiplomaticAction</c>s.
        /// <code>
        /// namespace Amplitude.Mercury.Data.Simulation
        /// {
        ///     public enum DiplomaticAction
        ///     {
        ///         DeclareSurpriseWar,
        ///         DeclareFormalWar,
        ///         DeclareEndOfAlliance,
        ///         DeclareSurrender,
        ///         RefuseDemands,
        ///         WithdrawDemands,
        ///         AcceptDemands,
        ///         IntroduceYourself,
        ///         FreeVassal,
        ///         StallForTime,
        ///         ProposeAllianceTreaty,
        ///         ProposeEndWarTreaty,
        ///         ProposeEndCrisisTreaty,
        ///         ProposeEndRebellionTreaty,
        ///         SignTreaty,
        ///         CounterTreaty,
        ///         IgnoreTreaty,
        ///         InsultTreaty,
        ///         ProposeEconomicalAgreement,
        ///         ProposeInformationAgreement,
        ///         ProposeCulturalAgreement,
        ///         ProposeMilitaryAgreement,
        ///         SignAgreement,
        ///         CounterAgreement,
        ///         IgnoreAgreement,
        ///         InsultAgreement,
        ///         BreakEconomicalAgreement,
        ///         BreakInformationAgreement,
        ///         BreakCulturalAgreement,
        ///         BreakMilitaryAgreement,
        ///         StartToFillSurrenderProposition,
        ///         CancelSurrenderProposition,
        ///         ProposeToSurrender,
        ///         RefuseSurrender,
        ///         AcceptSurrender,
        ///         FirstMeet,
        ///         ForceWhitePeace,
        ///         AllowToForceOtherToSurrender,
        ///         AllowToForceOtherToSurrenderToAlly,
        ///         DeclareForcedWar,
        ///         ForceSignAlliance,
        ///         ForceSignEndWar,
        ///         ForceSignEndCrisis,
        ///         ForceSignCulturalAgreement,
        ///         ForceSignInformationAgreement,
        ///         ForceSignEconomicalAgreement,
        ///         ForceSignMilitaryAgreement
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <param name="action"><c>DiplomaticAction</c> to validate.</param>
        /// <param name="otherEmpireIndex">Target empire's <c>EmpireIndex</c>.</param>
        /// <returns>Whether the action can be executed or not.</returns>
        public new bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) =>
            base.CanExecuteDiplomaticAction(action, otherEmpireIndex);
        
        /// <summary>
        ///     Executes given <c>DiplomaticAction</c> against another empire.
        /// </summary>
        /// <example>
        ///     List of available <c>DiplomaticAction</c>s.
        /// <code>
        /// namespace Amplitude.Mercury.Data.Simulation
        /// {
        ///     public enum DiplomaticAction
        ///     {
        ///         DeclareSurpriseWar,
        ///         DeclareFormalWar,
        ///         DeclareEndOfAlliance,
        ///         DeclareSurrender,
        ///         RefuseDemands,
        ///         WithdrawDemands,
        ///         AcceptDemands,
        ///         IntroduceYourself,
        ///         FreeVassal,
        ///         StallForTime,
        ///         ProposeAllianceTreaty,
        ///         ProposeEndWarTreaty,
        ///         ProposeEndCrisisTreaty,
        ///         ProposeEndRebellionTreaty,
        ///         SignTreaty,
        ///         CounterTreaty,
        ///         IgnoreTreaty,
        ///         InsultTreaty,
        ///         ProposeEconomicalAgreement,
        ///         ProposeInformationAgreement,
        ///         ProposeCulturalAgreement,
        ///         ProposeMilitaryAgreement,
        ///         SignAgreement,
        ///         CounterAgreement,
        ///         IgnoreAgreement,
        ///         InsultAgreement,
        ///         BreakEconomicalAgreement,
        ///         BreakInformationAgreement,
        ///         BreakCulturalAgreement,
        ///         BreakMilitaryAgreement,
        ///         StartToFillSurrenderProposition,
        ///         CancelSurrenderProposition,
        ///         ProposeToSurrender,
        ///         RefuseSurrender,
        ///         AcceptSurrender,
        ///         FirstMeet,
        ///         ForceWhitePeace,
        ///         AllowToForceOtherToSurrender,
        ///         AllowToForceOtherToSurrenderToAlly,
        ///         DeclareForcedWar,
        ///         ForceSignAlliance,
        ///         ForceSignEndWar,
        ///         ForceSignEndCrisis,
        ///         ForceSignCulturalAgreement,
        ///         ForceSignInformationAgreement,
        ///         ForceSignEconomicalAgreement,
        ///         ForceSignMilitaryAgreement
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <param name="action"><c>DiplomaticAction</c> to execute.</param>
        /// <param name="otherEmpireIndex">Target empire's <c>EmpireIndex</c>.</param>
        public new void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) =>
            base.ExecuteDiplomaticAction(action, otherEmpireIndex);
        
        /// <summary>
        ///     Enable or disable this empire's FogOfWar. This action may take several seconds to apply effects. 
        /// </summary>
        /// <param name="enable">Whether to enable or disable it.</param>
        public new void EnableFogOfWar(bool enable) => base.EnableFogOfWar(enable);


        /// <summary>
        ///     Adds a <c>ResearchCostModifierDefinition</c> to this empire based on provided parameters and returns it for later removing it with <see cref="RemoveResearchCostModifier">RemoveResearchCostModifier</see>.
        /// </summary>
        /// <remarks>
        ///     If user saves the game while one or more CostModifierDefinition is still active, that saved game file will fail to load, throwing an Exception. Make sure to remove them using <see cref="RemoveResearchCostModifier">RemoveResearchCostModifier</see> before saving the game.
        /// </remarks>
        /// <param name="value">Value modifier.</param>
        /// <param name="isOperationTypeMult">If true, refers to <c>Mult</c> from <c>CostModifierDefinition.OperationTypes</c> enum in namespace <c>Amplitude.Mercury.Data.Simulation</c>, otherwise <c>Add</c>.</param>
        /// <returns>The <c>ResearchCostModifierDefinition</c> added so you can remove it later.</returns>
        public new ResearchCostModifierDefinition AddResearchCostModifier(float value,
            bool isOperationTypeMult) =>
            base.AddResearchCostModifier(value, isOperationTypeMult);

        /// <summary>
        ///     Removes a <c>ResearchCostModifierDefinition</c> from this empire, see <see cref="AddConstructibleCostModifier">AddResearchCostModifier</see>.
        /// </summary>
        /// <param name="modifier">The <c>ResearchCostModifierDefinition</c> to remove.</param>
        public new void RemoveResearchCostModifier(ResearchCostModifierDefinition modifier) =>
            base.RemoveResearchCostModifier(modifier);
        
        /// <summary>
        ///     Adds a <c>ConstructibleCostModifierDefinition</c> to this empire based on provided parameters and returns it for later removing it with <see cref="RemoveConstructibleCostModifier">RemoveConstructibleCostModifier</see>.
        /// </summary>
        /// <remarks>
        ///     If user saves the game while one or more CostModifierDefinition is still active, that saved game file will fail to load, throwing an Exception. Make sure to remove them using <see cref="RemoveConstructibleCostModifier">RemoveConstructibleCostModifier</see> before saving the game.
        /// </remarks>
        /// <param name="value">Value modifier.</param>
        /// <param name="isOperationTypeMult">If true, refers to <c>Mult</c> from <c>CostModifierDefinition.OperationTypes</c> enum in namespace <c>Amplitude.Mercury.Data.Simulation</c>, otherwise <c>Add</c>.</param>
        /// <returns>The <c>ConstructibleCostModifierDefinition</c> added so you can remove it later.</returns>
        public new ConstructibleCostModifierDefinition AddConstructibleCostModifier(float value,
            bool isOperationTypeMult) =>
            base.AddConstructibleCostModifier(value, isOperationTypeMult);
        
        /// <summary>
        ///     Removes a <c>ConstructibleCostModifierDefinition</c> from this empire, see <see cref="AddConstructibleCostModifier">AddConstructibleCostModifier</see>.
        /// </summary>
        /// <param name="modifier">The <c>ConstructibleCostModifierDefinition</c> to remove.</param>
        public new void RemoveConstructibleCostModifier(ConstructibleCostModifierDefinition modifier) =>
            base.RemoveConstructibleCostModifier(modifier);


        // TODO: What is this for?
        public int IndustryWorkplaceBonusGain
        {
            get => (int) R.Fields.FixedPointRawValueField.GetValue(MajorEmpireSimulation.IndustryWorkplaceBonusGain) /
                   1000;
            set => R.Fields.FixedPointRawValueField.SetValue(MajorEmpireSimulation.IndustryWorkplaceBonusGain,
                value * 1000);
        }
        
        public MajorEmpire Entity => base.MajorEmpireEntity;
        public Amplitude.Mercury.Simulation.MajorEmpire Simulation => base.MajorEmpireSimulation;
        public new DepartmentOfTheTreasury DepartmentOfTheTreasury => base.DepartmentOfTheTreasury;
        public new DepartmentOfScience DepartmentOfScience => base.DepartmentOfScience;
        public new DepartmentOfCulture DepartmentOfCulture => base.DepartmentOfCulture;
        public new DepartmentOfForeignAffairs DepartmentOfForeignAffairs => base.DepartmentOfForeignAffairs;
        public new DepartmentOfDevelopment DepartmentOfDevelopment => base.DepartmentOfDevelopment;
        public new DepartmentOfTheInterior DepartmentOfTheInterior => base.DepartmentOfTheInterior;
        public new DepartmentOfDefense DepartmentOfDefense => base.DepartmentOfDefense;
        public new DepartmentOfTransportation DepartmentOfTransportation => base.DepartmentOfTransportation;
        
        public static HumankindEmpire Create(MajorEmpire fromMajorEmpire)
        {
            return new HumankindEmpire
            {
                MajorEmpireEntity = fromMajorEmpire,
                MajorEmpireSimulation = Sandbox.MajorEmpires[fromMajorEmpire.EmpireIndex]
            };
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        protected HumankindEmpire() { }

        public override string ToString() => PersonaName + " (#" + EmpireIndex + ")";
    }
}
