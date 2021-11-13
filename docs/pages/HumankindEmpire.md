# **HumankindEmpire Class**

A simplified interface (Facade) that masks a complex underlying structure of game classes  
in a single, simplified and well documented class to access and, where possible, edit most  
significant values related to a game Empire.

```csharp
public class HumankindEmpire : EmpireAbstraction, IEmpireDiplomacy, IMilitary, IResearch, IEmpireEconomy, IAIPersona, IEmpireExpansion
```

<table width="100%"><caption>

### **GENERAL**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="EmpireIndex" class="anchor"><blockquote>

```csharp
public int EmpireIndex
```
</blockquote></a>
</td><td align="left" valign="top">



Position of this empire within current game's array of empires.</td></tr>
<tr><td align="left" valign="top">

<a name="EraLevel" class="anchor"><blockquote>

```csharp
public int EraLevel
```
</blockquote></a>
</td><td align="left" valign="top">



This empire's current Era as number, where 1 is Neolithic.</td></tr>
<tr><td align="left" valign="top">

<a name="Fame" class="anchor"><blockquote>

```csharp
public int Fame
```
</blockquote></a>
</td><td align="left" valign="top">



Total Fame currently accumulated by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="IsAIActivated" class="anchor"><blockquote>

```csharp
public bool IsAIActivated
```
</blockquote></a>
</td><td align="left" valign="top">



Whether this empire is being controlled by the AI or by a human player.</td></tr>
<tr><td align="left" valign="top">

<a name="IsControlledByHuman" class="anchor"><blockquote>

```csharp
public bool IsControlledByHuman
```
</blockquote></a>
</td><td align="left" valign="top">



Whether this empire is being controlled by a human player or by the AI.</td></tr>
<tr><td align="left" valign="top">

<a name="IsInBattle" class="anchor"><blockquote>

```csharp
public bool IsInBattle
```
</blockquote></a>
</td><td align="left" valign="top">



Whether this empire has an ongoing battle active.</td></tr>
<tr><td align="left" valign="top">

<a name="Stability" class="anchor"><blockquote>

```csharp
public int Stability
```
</blockquote></a>
</td><td align="left" valign="top">



Equals to the sum of public order of cities controlled by this empire divided by the number of cities controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="SumOfEraStars" class="anchor"><blockquote>

```csharp
public int SumOfEraStars
```
</blockquote></a>
</td><td align="left" valign="top">



Equals to the sum of stars obtained by this empire in all eras.</td></tr>
<tr><td align="left" valign="top">

<a name="EnableFogOfWar" class="anchor"><blockquote>

```csharp
void EnableFogOfWar(bool enable)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Enable or disable this empire's FogOfWar. This action may take several seconds to apply effects.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>enable</kbd> → Whether to enable or disable it.</li>
</ul></details></sub></td></tr>
</tbody></table>

<table width="100%"><caption>

### **AI PERSONA**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="Archetypes" class="anchor"><blockquote>

```csharp
public Archetype Archetypes
```
</blockquote></a>
</td><td align="left" valign="top">



Gets this empire's `Archetype` bitmask.</td></tr>
<tr><td align="left" valign="top">

<a name="ArchetypesArray" class="anchor"><blockquote>

```csharp
public Archetype[] ArchetypesArray
```
</blockquote></a>
</td><td align="left" valign="top">



From this empire's `Archetype` bitmask, extracts each assigned `Archetype` as an element of the returned `Archetype` array.</td></tr>
<tr><td align="left" valign="top">

<a name="PersonaName" class="anchor"><blockquote>

```csharp
public string PersonaName
```
</blockquote></a>
</td><td align="left" valign="top">



This empire's AI persona.</td></tr>
<tr><td align="left" valign="top">

<a name="PersonaQuality" class="anchor"><blockquote>

```csharp
public int PersonaQuality
```
</blockquote></a>
</td><td align="left" valign="top">



Quantity of points which determine the score (and difficulty level) of this empire's AI persona.</td></tr>
<tr><td align="left" valign="top">

<a name="HasArchetype" class="anchor"><blockquote>

```csharp
bool HasArchetype(Archetype target)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Whether this empire's `Archetype` bitmask contains the given `Archetype`.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>target</kbd> → The `Archetype` to look for</li>
</ul></details></sub></td></tr>
<tr><td align="left" valign="top">

<a name="SetArchetype" class="anchor"><blockquote>

```csharp
void SetArchetype(Archetype target, bool remove)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Add or remove given `Archetype` from this empire's `Archetype` bitmask.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>target</kbd> → The `Archetype` to add or remove from the `Archetype` bitmask</li>
<li>
<kbd>remove</kbd> → Whether to add or remove it from the bitmask</li>
</ul></details></sub></td></tr>
</tbody></table>

<table width="100%"><caption>

### **MILITARY**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="ArmyCount" class="anchor"><blockquote>

```csharp
public int ArmyCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of armies controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="CombatStrength" class="anchor"><blockquote>

```csharp
public int CombatStrength
```
</blockquote></a>
</td><td align="left" valign="top">



Computed empire's current military power.</td></tr>
<tr><td align="left" valign="top">

<a name="MilitaryUpkeep" class="anchor"><blockquote>

```csharp
public int MilitaryUpkeep
```
</blockquote></a>
</td><td align="left" valign="top">



Sum of armies upkeep amount of money.</td></tr>
<tr><td align="left" valign="top">

<a name="RentedArmyCount" class="anchor"><blockquote>

```csharp
public int RentedArmyCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of armies controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="UnitCount" class="anchor"><blockquote>

```csharp
public int UnitCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of units among all empire's armies.</td></tr>
</tbody></table>

<table width="100%"><caption>

### **RESEARCH**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="AvailableTechnologiesCount" class="anchor"><blockquote>

```csharp
public int AvailableTechnologiesCount
```
</blockquote></a>
</td><td align="left" valign="top">



Available technologies.</td></tr>
<tr><td align="left" valign="top">

<a name="CompletedTechnologiesCount" class="anchor"><blockquote>

```csharp
public int CompletedTechnologiesCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of technologies already researched by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="ResearchNet" class="anchor"><blockquote>

```csharp
public int ResearchNet
```
</blockquote></a>
</td><td align="left" valign="top">



Research net income per turn.</td></tr>
<tr><td align="left" valign="top">

<a name="ResearchStock" class="anchor"><blockquote>

```csharp
public int ResearchStock
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/getter-setter.svg" alt="Getter/Setter" height="16px"/>

Gets or sets the *accumulated* research of this empire.<br/><sub><details open><summary><code>REMARKS</code></summary><ul>
<li>

This will always return 0 since science doesn't get accumulated anywhere, it is automatically consumed by the technology research queue.</li>
</ul></details></sub>
</td></tr>
<tr><td align="left" valign="top">

<a name="TechnologicalEraOffset" class="anchor"><blockquote>

```csharp
public int TechnologicalEraOffset
```
</blockquote></a>
</td><td align="left" valign="top">



Number of technological eras available ahead.</td></tr>
<tr><td align="left" valign="top">

<a name="UnlockedTechnologiesCount" class="anchor"><blockquote>

```csharp
public int UnlockedTechnologiesCount
```
</blockquote></a>
</td><td align="left" valign="top">



Unlocked technologies.</td></tr>
</tbody></table>

<table width="100%"><caption>

### **EMPIRE EXPANSION**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="CityCap" class="anchor"><blockquote>

```csharp
public int CityCap
```
</blockquote></a>
</td><td align="left" valign="top">



Maximum number of cities this empire can currently control without negative effects.</td></tr>
<tr><td align="left" valign="top">

<a name="CityCount" class="anchor"><blockquote>

```csharp
public int CityCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of cities currently controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="EmpirePopulation" class="anchor"><blockquote>

```csharp
public int EmpirePopulation
```
</blockquote></a>
</td><td align="left" valign="top">



`EmpirePopulation` equals to the sum of `SettlementsPopulation` with `UnitCount`.</td></tr>
<tr><td align="left" valign="top">

<a name="OccupiedCityCount" class="anchor"><blockquote>

```csharp
public int OccupiedCityCount
```
</blockquote></a>
</td><td align="left" valign="top">



Empire's number of occupied cities.</td></tr>
<tr><td align="left" valign="top">

<a name="OutpostCount" class="anchor"><blockquote>

```csharp
public int OutpostCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of outposts currently controlled by this empire.</td></tr>
<tr><td align="left" valign="top">

<a name="SettlementsPopulation" class="anchor"><blockquote>

```csharp
public int SettlementsPopulation
```
</blockquote></a>
</td><td align="left" valign="top">



Population in cities or outposts (Citizens).</td></tr>
<tr><td align="left" valign="top">

<a name="TerritoryCount" class="anchor"><blockquote>

```csharp
public int TerritoryCount
```
</blockquote></a>
</td><td align="left" valign="top">



Total number of territories controlled by this empire (outposts, cities and their attached territories).</td></tr>
</tbody></table>

<table width="100%"><caption>

### **EMPIRE ECONOMY**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="InfluenceNet" class="anchor"><blockquote>

```csharp
public int InfluenceNet
```
</blockquote></a>
</td><td align="left" valign="top">



Influence net income per turn.</td></tr>
<tr><td align="left" valign="top">

<a name="InfluenceStock" class="anchor"><blockquote>

```csharp
public int InfluenceStock
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/getter-setter.svg" alt="Getter/Setter" height="16px"/>

Gets or sets the accumulated influence of this empire.<br/><sub><details open><summary><code>REMARKS</code></summary><ul>
<li>

If you set this to another value, remember that it is the absolute value to be expected for this empire after this action takes effect. Tip: Use `+=` operator.</li>
</ul></details></sub>
</td></tr>
<tr><td align="left" valign="top">

<a name="LuxuryResourcesAccessCount" class="anchor"><blockquote>

```csharp
public int LuxuryResourcesAccessCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of accesses to Luxury Resources.</td></tr>
<tr><td align="left" valign="top">

<a name="MoneyNet" class="anchor"><blockquote>

```csharp
public int MoneyNet
```
</blockquote></a>
</td><td align="left" valign="top">



Money net income per turn which is added to [MoneyStock](HumankindEmpire_MoneyStock.md 'Modding.Humankind.DevTools.HumankindEmpire.MoneyStock') at the end of turn phase.</td></tr>
<tr><td align="left" valign="top">

<a name="MoneyStock" class="anchor"><blockquote>

```csharp
public int MoneyStock
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/getter-setter.svg" alt="Getter/Setter" height="16px"/>

Gets or sets the amount of money for this empire.<br/><sub><details open><summary><code>REMARKS</code></summary><ul>
<li>

If you set this to another value, remember that it is the absolute value to be expected for this empire after this action takes effect. Tip: Use `+=` operator to avoid loosing money.</li>
</ul></details></sub>
</td></tr>
<tr><td align="left" valign="top">

<a name="StrategicResourcesAccessCount" class="anchor"><blockquote>

```csharp
public int StrategicResourcesAccessCount
```
</blockquote></a>
</td><td align="left" valign="top">



Number of accesses to Strategic Resources.</td></tr>
<tr><td align="left" valign="top">

<a name="TradeNodesCount" class="anchor"><blockquote>

```csharp
public int TradeNodesCount
```
</blockquote></a>
</td><td align="left" valign="top">



Sum of trade nodes.</td></tr>
</tbody></table>

<table width="100%"><caption>

### **EMPIRE DIPLOMACY**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="CanDeclareWarTo" class="anchor"><blockquote>

```csharp
bool CanDeclareWarTo(int otherEmpireIndex)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Whether war can be declared or not to the given empire.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details></sub></td></tr>
<tr><td align="left" valign="top">

<a name="CanExecuteDiplomaticAction" class="anchor"><blockquote>

```csharp
bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Validates if given `DiplomaticAction` can be executed against given empire's `EmpireIndex`.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>action</kbd> → `DiplomaticAction` to validate.</li>
<li>
<kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</li>
</ul></details></sub><br/><sub><details><summary><code>EXAMPLES</code></summary><blockquote>

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
</blockquote></details></sub>
</td></tr>
<tr><td align="left" valign="top">

<a name="DeclareWarTo" class="anchor"><blockquote>

```csharp
bool DeclareWarTo(int otherEmpireIndex)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Declare war to another empire if possible. Gives priority to a surprise war before a formal war.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details></sub></td></tr>
<tr><td align="left" valign="top">

<a name="DiplomaticStateTypeTo" class="anchor"><blockquote>

```csharp
DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Empire's current `DiplomaticStateType` with the given empire. See included example for Amplitude's `DiplomaticStateType` implementation.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</li>
</ul></details></sub><br/><sub><details><summary><code>EXAMPLES</code></summary><blockquote>

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
</blockquote></details></sub>
</td></tr>
<tr><td align="left" valign="top">

<a name="ExecuteDiplomaticAction" class="anchor"><blockquote>

```csharp
void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex)
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Executes given `DiplomaticAction` against another empire.<br/><sub><details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>action</kbd> → `DiplomaticAction` to execute.</li>
<li>
<kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</li>
</ul></details></sub><br/><sub><details><summary><code>EXAMPLES</code></summary><blockquote>

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
</blockquote></details></sub>
</td></tr>
</tbody></table>
