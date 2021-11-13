# HumankindEmpire Class

A simplified interface (Facade) that masks a complex underlying structure of game classes  
in a single, simplified and well documented class to access and, where possible, edit most  
significant values related to a game Empire.

```csharp
public class HumankindEmpire
```

| <h2>**GENERAL**</h2> | | |
| :--- | :--- | :--- |
| <a id="Archetypes" class="anchor" aria-hidden="true"></a><h4><code> Archetype Archetypes { get; } </code></h4> |  | Gets this empire's `Archetype` bitmask. |
| <a id="ArchetypesArray" class="anchor" aria-hidden="true"></a><h4><code> Archetype[] ArchetypesArray { get; } </code></h4> |  | From this empire's `Archetype` bitmask, extracts each assigned `Archetype` as an element of the returned `Archetype` array. |
| <a id="CityCap" class="anchor" aria-hidden="true"></a><h4><code> int CityCap { get; } </code></h4> |  | Maximum number of cities this empire can currently control without negative effects. |
| <a id="CityCount" class="anchor" aria-hidden="true"></a><h4><code> int CityCount { get; } </code></h4> |  | Number of cities currently controlled by this empire. |
| <a id="EmpireIndex" class="anchor" aria-hidden="true"></a><h4><code> int EmpireIndex { get; } </code></h4> |  | Position of this empire within current game's array of empires. |
| <a id="EmpirePopulation" class="anchor" aria-hidden="true"></a><h4><code> int EmpirePopulation { get; } </code></h4> |  | `EmpirePopulation` equals to the sum of `SettlementsPopulation` with `UnitCount`. |
| <a id="EraLevel" class="anchor" aria-hidden="true"></a><h4><code> int EraLevel { get; } </code></h4> |  | This empire's current Era as number, where 1 is Neolithic. |
| <a id="Fame" class="anchor" aria-hidden="true"></a><h4><code> int Fame { get; } </code></h4> |  | Total Fame currently accumulated by this empire. |
| <a id="IsAIActivated" class="anchor" aria-hidden="true"></a><h4><code> bool IsAIActivated { get; } </code></h4> |  | Whether this empire is being controlled by the AI or by a human player. |
| <a id="IsControlledByHuman" class="anchor" aria-hidden="true"></a><h4><code> bool IsControlledByHuman { get; } </code></h4> |  | Whether this empire is being controlled by a human player or by the AI. |
| <a id="IsInBattle" class="anchor" aria-hidden="true"></a><h4><code> bool IsInBattle { get; } </code></h4> |  | Whether this empire has an ongoing battle active. |
| <a id="OccupiedCityCount" class="anchor" aria-hidden="true"></a><h4><code> int OccupiedCityCount { get; } </code></h4> |  | Empire's number of occupied cities. |
| <a id="OutpostCount" class="anchor" aria-hidden="true"></a><h4><code> int OutpostCount { get; } </code></h4> |  | Number of outposts currently controlled by this empire. |
| <a id="PersonaName" class="anchor" aria-hidden="true"></a><h4><code> string PersonaName { get; } </code></h4> |  | This empire's AI persona. |
| <a id="PersonaQuality" class="anchor" aria-hidden="true"></a><h4><code> int PersonaQuality { get; } </code></h4> |  | Quantity of points which determine the score (and difficulty level) of this empire's AI persona. |
| <a id="ResearchNet" class="anchor" aria-hidden="true"></a><h4><code> int ResearchNet { get; } </code></h4> |  | Research net income per turn. |
| <a id="ResearchStock" class="anchor" aria-hidden="true"></a><h4><code> int ResearchStock { get; set; } </code></h4> | ![Setter](../resources/setter.svg) | Gets or sets the *accumulated* research of this empire.<br/><sup><details open><summary><code>REMARKS</code></summary><ul><li><h3>This will always return 0 since science doesn't get accumulated anywhere, it is automatically<br/>consumed by the technology research queue.</h3></li></ul></details></sup> |
| <a id="SettlementsPopulation" class="anchor" aria-hidden="true"></a><h4><code> int SettlementsPopulation { get; } </code></h4> |  | Population in cities or outposts (Citizens). |
| <a id="Stability" class="anchor" aria-hidden="true"></a><h4><code> int Stability { get; } </code></h4> |  | Equals to the sum of public order of cities controlled by this empire divided by the number of cities controlled by this empire. |
| <a id="SumOfEraStars" class="anchor" aria-hidden="true"></a><h4><code> int SumOfEraStars { get; } </code></h4> |  | Equals to the sum of stars obtained by this empire in all eras. |
| <a id="TerritoryCount" class="anchor" aria-hidden="true"></a><h4><code> int TerritoryCount { get; } </code></h4> |  | Total number of territories controlled by this empire (outposts, cities and their attached territories). |
| <h4><code> void EnableFogOfWar(bool enable) </code></h4> | ![Method](../resources/method.svg) | Enable or disable this empire's FogOfWar. This action may take several seconds to apply effects.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>enable</kbd> → Whether to enable or disable it.</h3></li></ul></details></sup> |
| <h4><code> bool HasArchetype(Archetype target) </code></h4> | ![Method](../resources/method.svg) | Whether this empire's `Archetype` bitmask contains the given `Archetype`.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>target</kbd> → The `Archetype` to look for</h3></li></ul></details></sup> |
| <h4><code> void SetArchetype(Archetype target, bool remove) </code></h4> | ![Method](../resources/method.svg) | Add or remove given `Archetype` from this empire's `Archetype` bitmask.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>target</kbd> → The `Archetype` to add or remove from the `Archetype` bitmask</h3></li><li><h3><kbd>remove</kbd> → Whether to add or remove it from the bitmask</h3></li></ul></details></sup> |

| <h2>**MILITARY**</h2> | | |
| :--- | :--- | :--- |
| <a id="ArmyCount" class="anchor" aria-hidden="true"></a><h4><code> int ArmyCount { get; } </code></h4> |  | Number of armies controlled by this empire. |
| <a id="CombatStrength" class="anchor" aria-hidden="true"></a><h4><code> int CombatStrength { get; } </code></h4> |  | Computed empire's current military power. |
| <a id="MilitaryUpkeep" class="anchor" aria-hidden="true"></a><h4><code> int MilitaryUpkeep { get; } </code></h4> |  | Sum of armies upkeep amount of money. |
| <a id="RentedArmyCount" class="anchor" aria-hidden="true"></a><h4><code> int RentedArmyCount { get; } </code></h4> |  | Number of armies controlled by this empire. |
| <a id="UnitCount" class="anchor" aria-hidden="true"></a><h4><code> int UnitCount { get; } </code></h4> |  | Number of units among all empire's armies. |

| <h2>**RESEARCH**</h2> | | |
| :--- | :--- | :--- |
| <a id="AvailableTechnologiesCount" class="anchor" aria-hidden="true"></a><h4><code> int AvailableTechnologiesCount { get; } </code></h4> |  | Available technologies. |
| <a id="CompletedTechnologiesCount" class="anchor" aria-hidden="true"></a><h4><code> int CompletedTechnologiesCount { get; } </code></h4> |  | Number of technologies already researched by this empire. |
| <a id="TechnologicalEraOffset" class="anchor" aria-hidden="true"></a><h4><code> int TechnologicalEraOffset { get; } </code></h4> |  | Number of technological eras available ahead. |
| <a id="UnlockedTechnologiesCount" class="anchor" aria-hidden="true"></a><h4><code> int UnlockedTechnologiesCount { get; } </code></h4> |  | Unlocked technologies. |

| <h2>**ECONOMY**</h2> | | |
| :--- | :--- | :--- |
| <a id="InfluenceNet" class="anchor" aria-hidden="true"></a><h4><code> int InfluenceNet { get; } </code></h4> |  | Influence net income per turn. |
| <a id="InfluenceStock" class="anchor" aria-hidden="true"></a><h4><code> int InfluenceStock { get; set; } </code></h4> | ![Setter](../resources/setter.svg) | Gets or sets the accumulated influence of this empire.<br/><sup><details open><summary><code>REMARKS</code></summary><ul><li><h3>If you set this to another value, remember that it is the absolute value to be expected for<br/>this empire after this action takes effect. Tip: Use `+=` operator.</h3></li></ul></details></sup> |
| <a id="LuxuryResourcesAccessCount" class="anchor" aria-hidden="true"></a><h4><code> int LuxuryResourcesAccessCount { get; } </code></h4> |  | Number of accesses to Luxury Resources. |
| <a id="MoneyNet" class="anchor" aria-hidden="true"></a><h4><code> int MoneyNet { get; } </code></h4> |  | Money net income per turn which is added to `MoneyStock` at the end of turn phase. |
| <a id="MoneyStock" class="anchor" aria-hidden="true"></a><h4><code> int MoneyStock { get; set; } </code></h4> | ![Setter](../resources/setter.svg) | Gets or sets the amount of money for this empire.<br/><sup><details open><summary><code>REMARKS</code></summary><ul><li><h3>If you set this to another value, remember that it is the absolute value to be expected for<br/>this empire after this action takes effect. Tip: Use `+=` operator to avoid loosing money.</h3></li></ul></details></sup> |
| <a id="StrategicResourcesAccessCount" class="anchor" aria-hidden="true"></a><h4><code> int StrategicResourcesAccessCount { get; } </code></h4> |  | Number of accesses to Strategic Resources. |
| <a id="TradeNodesCount" class="anchor" aria-hidden="true"></a><h4><code> int TradeNodesCount { get; } </code></h4> |  | Sum of trade nodes. |

| <h2>**DIPLOMACY**</h2> | | |
| :--- | :--- | :--- |
| <h4><code> bool CanDeclareWarTo(int otherEmpireIndex) </code></h4> | ![Method](../resources/method.svg) | Whether war can be declared or not to the given empire.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</h3></li></ul></details></sup> |
| <h4><code> bool CanExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) </code></h4> | ![Method](../resources/method.svg) | Validates if given `DiplomaticAction` can be executed against given empire's `EmpireIndex`.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>action</kbd> → `DiplomaticAction` to validate.</h3></li><li><h3><kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</h3></li></ul></details></sup> |
| <h4><code> bool DeclareWarTo(int otherEmpireIndex) </code></h4> | ![Method](../resources/method.svg) | Declare war to another empire if possible. Gives priority to a surprise war before a formal war.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</h3></li></ul></details></sup> |
| <h4><code> DiplomaticStateType DiplomaticStateTypeTo(int otherEmpireIndex) </code></h4> | ![Method](../resources/method.svg) | Empire's current `DiplomaticStateType` with the given empire. Amplitude's `DiplomaticStateType` source code:<br/>`public enum DiplomaticStateType { Unknown, PartialyKnown, Peace, Alliance, VassalToLiege, VassalToFellowVassal, VassalToExternal, War, PartialyEliminated, BothEliminated }`<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>otherEmpireIndex</kbd> → Other empire's `EmpireIndex`.</h3></li></ul></details></sup> |
| <h4><code> void ExecuteDiplomaticAction(DiplomaticAction action, int otherEmpireIndex) </code></h4> | ![Method](../resources/method.svg) | Executes given `DiplomaticAction` against another empire.<br/><sup><details><summary><code>PARAMETERS</code></summary><ul><li><h3><kbd>action</kbd> → `DiplomaticAction` to execute.</h3></li><li><h3><kbd>otherEmpireIndex</kbd> → Target empire's `EmpireIndex`.</h3></li></ul></details></sup> |
