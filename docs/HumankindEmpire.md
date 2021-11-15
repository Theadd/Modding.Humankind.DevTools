# **HumankindEmpire Class**

A simplified interface (Facade) that masks a complex underlying structure of game classes  single, simplified and well documented class to access and, where possible, edit most ificant values related to a game Empire.

```csharp
public class HumankindEmpire : EmpireAbstraction, IEmpireDiplomacy, IMilitary, IResearch, IEmpireEconomy, IAIPersona, IEmpireExpansion
```

<table width="100%"><caption>

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public int EmpireIndex
```
</td><td align="left" valign="top">

### EmpireIndex
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Position of this empire within current game's array of empires.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int EraLevel
```
</td><td align="left" valign="top">

### EraLevel
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
This empire's current Era as number, where 1 is Neolithic.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int Fame
```
</td><td align="left" valign="top">

### Fame
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Total Fame currently accumulated by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsAIActivated
```
</td><td align="left" valign="top">

### IsAIActivated
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this empire is being controlled by the AI or by a human player.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsControlledByHuman
```
</td><td align="left" valign="top">

### IsControlledByHuman
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this empire is being controlled by a human player or by the AI.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsInBattle
```
</td><td align="left" valign="top">

### IsInBattle
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this empire has an ongoing battle active.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int Stability
```
</td><td align="left" valign="top">

### Stability
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Equals to the sum of public order of cities controlled by this empire divided by the number of cities controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int SumOfEraStars
```
</td><td align="left" valign="top">

### SumOfEraStars
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Equals to the sum of stars obtained by this empire in all eras.</td></tr>
<tr><td align="left" valign="top">

```csharp
void EnableFogOfWar(bool enable)
```
</td><td align="left" valign="top">

### EnableFogOfWar
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Enable or disable this empire's FogOfWar. This action may take several seconds to apply effects.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>enable</kbd> → Whether to enable or disable it.</li>
</ul></details></td></tr>
</tbody></table>

<table width="100%"><caption>

## AI PERSONA  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public Archetype Archetypes
```
</td><td align="left" valign="top">

### Archetypes
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Gets this empire's `Archetype` bitmask.</td></tr>
<tr><td align="left" valign="top">

```csharp
public Archetype[] ArchetypesArray
```
</td><td align="left" valign="top">

### ArchetypesArray
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
From this empire's `Archetype` bitmask, extracts each assigned `Archetype` as an element of the returned `Archetype` array.</td></tr>
<tr><td align="left" valign="top">

```csharp
public string PersonaName
```
</td><td align="left" valign="top">

### PersonaName
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
This empire's AI persona.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int PersonaQuality
```
</td><td align="left" valign="top">

### PersonaQuality
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Quantity of points which determine the score (and difficulty level) of this empire's AI persona.</td></tr>
<tr><td align="left" valign="top">

```csharp
bool HasArchetype(Archetype target)
```
</td><td align="left" valign="top">

### HasArchetype
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Whether this empire's `Archetype` bitmask contains the given `Archetype`.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>target</kbd> → The `Archetype` to look for</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
void SetArchetype(Archetype target, bool remove)
```
</td><td align="left" valign="top">

### SetArchetype
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Add or remove given `Archetype` from this empire's `Archetype` bitmask.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>target</kbd> → The `Archetype` to add or remove from the `Archetype` bitmask</li>
<li>
<kbd>remove</kbd> → Whether to add or remove it from the bitmask</li>
</ul></details></td></tr>
</tbody></table>

<table width="100%"><caption>

## MILITARY  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public int ArmyCount
```
</td><td align="left" valign="top">

### ArmyCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of armies controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int CombatStrength
```
</td><td align="left" valign="top">

### CombatStrength
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Computed empire's current military power.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int MilitaryUpkeep
```
</td><td align="left" valign="top">

### MilitaryUpkeep
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Sum of armies upkeep amount of money.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int RentedArmyCount
```
</td><td align="left" valign="top">

### RentedArmyCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of armies controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int UnitCount
```
</td><td align="left" valign="top">

### UnitCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of units among all empire's armies.</td></tr>
</tbody></table>

<table width="100%"><caption>

## RESEARCH  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public int AvailableTechnologiesCount
```
</td><td align="left" valign="top">

### AvailableTechnologiesCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Available technologies.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int CompletedTechnologiesCount
```
</td><td align="left" valign="top">

### CompletedTechnologiesCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of technologies already researched by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int ResearchNet
```
</td><td align="left" valign="top">

### ResearchNet
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Research net income per turn.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int ResearchStock
```
</td><td align="left" valign="top">

### ResearchStock
<img src="./resources/getter-setter.svg" alt="Getter/Setter" height="16px"/><br/>
Gets or sets the *accumulated* research of this empire.<details open><summary><code>REMARKS</code></summary><ul>
<li>

This will always return 0 since science doesn't get accumulated anywhere, it is automatically consumed by the technology research queue.</li>
</ul></details>
</td></tr>
<tr><td align="left" valign="top">

```csharp
public int TechnologicalEraOffset
```
</td><td align="left" valign="top">

### TechnologicalEraOffset
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of technological eras available ahead.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int UnlockedTechnologiesCount
```
</td><td align="left" valign="top">

### UnlockedTechnologiesCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Unlocked technologies.</td></tr>
</tbody></table>

<table width="100%"><caption>

## EMPIRE EXPANSION  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public int CityCap
```
</td><td align="left" valign="top">

### CityCap
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Maximum number of cities this empire can currently control without negative effects.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int CityCount
```
</td><td align="left" valign="top">

### CityCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of cities currently controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int EmpirePopulation
```
</td><td align="left" valign="top">

### EmpirePopulation
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
`EmpirePopulation` equals to the sum of `SettlementsPopulation` with `UnitCount`.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int OccupiedCityCount
```
</td><td align="left" valign="top">

### OccupiedCityCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Empire's number of occupied cities.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int OutpostCount
```
</td><td align="left" valign="top">

### OutpostCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of outposts currently controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int SettlementsPopulation
```
</td><td align="left" valign="top">

### SettlementsPopulation
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Population in cities or outposts (Citizens).</td></tr>
<tr><td align="left" valign="top">

```csharp
public int TerritoryCount
```
</td><td align="left" valign="top">

### TerritoryCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Total number of territories controlled by this empire (outposts, cities and their attached territories).</td></tr>
</tbody></table>

<table width="100%"><caption>

## EMPIRE ECONOMY  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public int InfluenceNet
```
</td><td align="left" valign="top">

### InfluenceNet
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Influence net income per turn.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int InfluenceStock
```
</td><td align="left" valign="top">

### InfluenceStock
<img src="./resources/getter-setter.svg" alt="Getter/Setter" height="16px"/><br/>
Gets or sets the accumulated influence of this empire.<details open><summary><code>REMARKS</code></summary><ul>
<li>

If you set this to another value, remember that it is the absolute value to be expected for this empire after this action takes effect. Tip: Use `+=` operator.</li>
</ul></details>
</td></tr>
<tr><td align="left" valign="top">

```csharp
public int LuxuryResourcesAccessCount
```
</td><td align="left" valign="top">

### LuxuryResourcesAccessCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of accesses to Luxury Resources.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int MoneyNet
```
</td><td align="left" valign="top">

### MoneyNet
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Money net income per turn which is added to [MoneyStock](HumankindEmpire_MoneyStock.md 'Modding.Humankind.DevTools.HumankindEmpire.MoneyStock') at the end of turn phase.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int MoneyStock
```
</td><td align="left" valign="top">

### MoneyStock
<img src="./resources/getter-setter.svg" alt="Getter/Setter" height="16px"/><br/>
Gets or sets the amount of money for this empire.<details open><summary><code>REMARKS</code></summary><ul>
<li>

If you set this to another value, remember that it is the absolute value to be expected for this empire after this action takes effect. Tip: Use `+=` operator to avoid loosing money.</li>
</ul></details>
</td></tr>
<tr><td align="left" valign="top">

```csharp
public int StrategicResourcesAccessCount
```
</td><td align="left" valign="top">

### StrategicResourcesAccessCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Number of accesses to Strategic Resources.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int TradeNodesCount
```
</td><td align="left" valign="top">

### TradeNodesCount
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Sum of trade nodes.</td></tr>
</tbody></table>

<table width="100%"><caption>

## EMPIRE DIPLOMACY  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
bool CanDeclareWarTo(int otherEmpireIndex)
```
</td><td align="left" valign="top">

### CanDeclareWarTo
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Whether war can be declared or not to the given empire.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex)
```
</td><td align="left" valign="top">

### CanExecuteDiplomaticAction
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Validates if given `DiplomaticAction` can be executed against given empire's `EmpireIndex`.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>action</kbd> → `DiplomaticAction` to validate.</li>
<li>
<kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</li>
</ul></details><details><summary><code>EXAMPLES</code></summary><blockquote>

List of available `DiplomaticAction`s.

```csharp
namespace Amplitude.Mercury.Data.Simulation
{
    public enum DiplomaticAction
    {
        DeclareSurpriseWar,
        DeclareFormalWar,
        DeclareEndOfAlliance,
        DeclareSurrender,
        RefuseDemands,
        WithdrawDemands,
        AcceptDemands,
        IntroduceYourself,
        FreeVassal,
        StallForTime,
        ProposeAllianceTreaty,
        ProposeEndWarTreaty,
        ProposeEndCrisisTreaty,
        ProposeEndRebellionTreaty,
        SignTreaty,
        CounterTreaty,
        IgnoreTreaty,
        InsultTreaty,
        ProposeEconomicalAgreement,
        ProposeInformationAgreement,
        ProposeCulturalAgreement,
        ProposeMilitaryAgreement,
        SignAgreement,
        CounterAgreement,
        IgnoreAgreement,
        InsultAgreement,
        BreakEconomicalAgreement,
        BreakInformationAgreement,
        BreakCulturalAgreement,
        BreakMilitaryAgreement,
        StartToFillSurrenderProposition,
        CancelSurrenderProposition,
        ProposeToSurrender,
        RefuseSurrender,
        AcceptSurrender,
        FirstMeet,
        ForceWhitePeace,
        AllowToForceOtherToSurrender,
        AllowToForceOtherToSurrenderToAlly,
        DeclareForcedWar,
        ForceSignAlliance,
        ForceSignEndWar,
        ForceSignEndCrisis,
        ForceSignCulturalAgreement,
        ForceSignInformationAgreement,
        ForceSignEconomicalAgreement,
        ForceSignMilitaryAgreement
    }
}
```
</blockquote></details>
</td></tr>
<tr><td align="left" valign="top">

```csharp
bool DeclareWarTo(int otherEmpireIndex)
```
</td><td align="left" valign="top">

### DeclareWarTo
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Declare war to another empire if possible. Gives priority to a surprise war before a formal war.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex)
```
</td><td align="left" valign="top">

### DiplomaticStateTypeTo
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Empire's current `DiplomaticStateType` with the given empire. See included example for Amplitude's `DiplomaticStateType` implementation.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details><details><summary><code>EXAMPLES</code></summary><blockquote>

Implementation code of `Amplitude.Mercury.Data.Simulation.DiplomaticStateType`.

```csharp
public enum DiplomaticStateType
{
    Unknown,
    PartialyKnown,
    Peace,
    Alliance,
    VassalToLiege,
    VassalToFellowVassal,
    VassalToExternal,
    War,
    PartialyEliminated,
    BothEliminated
}
```
</blockquote></details>
</td></tr>
<tr><td align="left" valign="top">

```csharp
void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex)
```
</td><td align="left" valign="top">

### ExecuteDiplomaticAction
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Executes given `DiplomaticAction` against another empire.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>action</kbd> → `DiplomaticAction` to execute.</li>
<li>
<kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</li>
</ul></details><details><summary><code>EXAMPLES</code></summary><blockquote>

List of available `DiplomaticAction`s.

```csharp
namespace Amplitude.Mercury.Data.Simulation
{
    public enum DiplomaticAction
    {
        DeclareSurpriseWar,
        DeclareFormalWar,
        DeclareEndOfAlliance,
        DeclareSurrender,
        RefuseDemands,
        WithdrawDemands,
        AcceptDemands,
        IntroduceYourself,
        FreeVassal,
        StallForTime,
        ProposeAllianceTreaty,
        ProposeEndWarTreaty,
        ProposeEndCrisisTreaty,
        ProposeEndRebellionTreaty,
        SignTreaty,
        CounterTreaty,
        IgnoreTreaty,
        InsultTreaty,
        ProposeEconomicalAgreement,
        ProposeInformationAgreement,
        ProposeCulturalAgreement,
        ProposeMilitaryAgreement,
        SignAgreement,
        CounterAgreement,
        IgnoreAgreement,
        InsultAgreement,
        BreakEconomicalAgreement,
        BreakInformationAgreement,
        BreakCulturalAgreement,
        BreakMilitaryAgreement,
        StartToFillSurrenderProposition,
        CancelSurrenderProposition,
        ProposeToSurrender,
        RefuseSurrender,
        AcceptSurrender,
        FirstMeet,
        ForceWhitePeace,
        AllowToForceOtherToSurrender,
        AllowToForceOtherToSurrenderToAlly,
        DeclareForcedWar,
        ForceSignAlliance,
        ForceSignEndWar,
        ForceSignEndCrisis,
        ForceSignCulturalAgreement,
        ForceSignInformationAgreement,
        ForceSignEconomicalAgreement,
        ForceSignMilitaryAgreement
    }
}
```
</blockquote></details>
</td></tr>
</tbody></table>
