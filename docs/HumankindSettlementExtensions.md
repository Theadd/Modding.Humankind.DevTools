# **HumankindSettlementExtensions Class**

```csharp
public static class HumankindSettlementExtensions
```

<table width="100%"><caption>

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
IEnumerable<Army> Armies(this IEnumerable<HumankindSettlement> sequence)
```
</td><td align="left" valign="top">

### Armies
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Aggregates the Armies of each `HumankindSettlement` in the sequence into a new sequence of armies. also: [HumankindSettlement.Armies](HumankindSettlement.md#Armies 'Modding.Humankind.DevTools.HumankindSettlement.Armies').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> →</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> BuildUnit(this IEnumerable<HumankindSettlement> sequence, UnitDefinition unitDefinition)
```
</td><td align="left" valign="top">

### BuildUnit
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Spawns a unit to the `Settlement`'s assigned spawn point for every `HumankindSettlement` in the sequence.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> →</li>
<li>
<kbd>unitDefinition</kbd> → The `UnitDefinition` to spawn a `Unit` from.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> BuildUnitByName(this IEnumerable<HumankindSettlement> sequence, string unitDefinitionName)
```
</td><td align="left" valign="top">

### BuildUnitByName
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Spawns a unit to the `Settlement`'s assigned spawn point for every `HumankindSettlement` in the sequence.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> →</li>
<li>
<kbd>unitDefinitionName</kbd> → The name of the `UnitDefinition` to spawn a `Unit` from.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> Interactive(this IEnumerable<HumankindSettlement> sequence, Action<HumankindSettlement> action)
```
</td><td align="left" valign="top">

### Interactive
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
This extension provides an easy way to iterate the sequence of `HumankindSettlements` one by one when pressing `[F3]` key while in-game.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> →</li>
<li>
<kbd>action</kbd> → The action to be executed in every iteration, having `HumankindSettlement` as first parameter.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> IsCapital(this IEnumerable<HumankindSettlement> sequence, bool isCapital)
```
</td><td align="left" valign="top">

### IsCapital
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Returns a new sequence of `HumankindSettlement`s containing only the capital cities from the given sequence. `isCapital` to false to invert the results.  also: [HumankindSettlement.IsCapital](HumankindSettlement.md#IsCapital 'Modding.Humankind.DevTools.HumankindSettlement.IsCapital').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
<li>
<kbd>isCapital</kbd> → Set to false in order to get only those `HumankindSettlement`s which are not a capital, defaults to true.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> IsCity(this IEnumerable<HumankindSettlement> sequence, bool isCity)
```
</td><td align="left" valign="top">

### IsCity
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Returns a new sequence that only contain settlements evolved to city. Set `isCity` to false to get the opposite results. also: [HumankindSettlement.IsCity](HumankindSettlement.md#IsCity 'Modding.Humankind.DevTools.HumankindSettlement.IsCity').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
<li>
<kbd>isCity</kbd> → When false, returns a sequence containing those `HumankindSettlement` that were not cities, defaults to true.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> IsOutpost(this IEnumerable<HumankindSettlement> sequence, bool isOutpost)
```
</td><td align="left" valign="top">

### IsOutpost
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Returns a new sequence of `HumankindSettlement` only containing Outpost settlements from this sequence. `isOutpost` to false for the opposite results. also: [HumankindSettlement.IsOutpost](HumankindSettlement.md#IsOutpost 'Modding.Humankind.DevTools.HumankindSettlement.IsOutpost').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
<li>
<kbd>isOutpost</kbd> → Set to false to get the opposite results.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<Territory> Territories(this IEnumerable<HumankindSettlement> sequence)
```
</td><td align="left" valign="top">

### Territories
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Aggregates the territories of each `HumankindSettlement` in the sequence into a new sequence of territories. also: [HumankindSettlement.Territories](HumankindSettlement.md#Territories 'Modding.Humankind.DevTools.HumankindSettlement.Territories').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
</ul></details></td></tr>
</tbody></table>
