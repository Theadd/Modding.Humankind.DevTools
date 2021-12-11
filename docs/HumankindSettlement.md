# **HumankindSettlement Class**

```csharp
public class HumankindSettlement : SettlementAbstraction, IEquatable<HumankindSettlement>
```

<table width="100%"><caption>

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public IEnumerable<Army> Armies
```
</td><td align="left" valign="top">

### Armies
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
All armies found within all territories annexed/controlled by this `Settlement`.</td></tr>
<tr><td align="left" valign="top">

```csharp
public HumankindEmpire Empire
```
</td><td align="left" valign="top">

### Empire
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
The [HumankindEmpire](HumankindEmpire.md 'Modding.Humankind.DevTools.HumankindEmpire') controlling this `Settlement`.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsCapital
```
</td><td align="left" valign="top">

### IsCapital
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this settlement is the capital city of the `Empire`.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsCity
```
</td><td align="left" valign="top">

### IsCity
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this `Settlement` is a city or not.</td></tr>
<tr><td align="left" valign="top">

```csharp
public bool IsOutpost
```
</td><td align="left" valign="top">

### IsOutpost
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
Whether this `Settlement` is an **Outpost** or not.</td></tr>
<tr><td align="left" valign="top">

```csharp
public int Population
```
</td><td align="left" valign="top">

### Population
<img src="./resources/getter-setter.svg" alt="Getter/Setter" height="16px"/><br/>
Get or set `Settlement`'s total population.</td></tr>
<tr><td align="left" valign="top">

```csharp
public Territory[] Territories
```
</td><td align="left" valign="top">

### Territories
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
All territories that are part of this `Settlement`.</td></tr>
<tr><td align="left" valign="top">

```csharp
public WorldPosition WorldPosition
```
</td><td align="left" valign="top">

### WorldPosition
<img src="./resources/getter.svg" alt="Getter" height="16px"/><br/>
`Settlement`'s WorldPosition.</td></tr>
<tr><td align="left" valign="top">

```csharp
Unit BuildUnit(UnitDefinition unitDefinition)
```
</td><td align="left" valign="top">

### BuildUnit
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Spawns a unit to `Settlement`'s assigned spawn point based on `UnitDefinition`'s `UnitSpawnType`.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>unitDefinition</kbd> → The `UnitDefinition` to spawn a `Unit` from.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
void CenterToCamera()
```
</td><td align="left" valign="top">

### CenterToCamera
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Center main camera view to this `Settlement`'s TileIndex.   [Territories](HumankindSettlement.md#Territories 'Modding.Humankind.DevTools.HumankindSettlement.Territories') ___ [string](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/string 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/string') ___ [SimulationEntityGUID](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/SimulationEntityGUID 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/SimulationEntityGUID') ___ [EmpireIndex](#Modding.Humankind.DevTools.HumankindSettlement.EmpireIndex 'Modding.Humankind.DevTools.HumankindSettlement.EmpireIndex') ___ [EmpireIndex](#Modding.Humankind.DevTools.HumankindSettlement.EmpireIndex 'Modding.Humankind.DevTools.HumankindSettlement.EmpireIndex') ___ [EmpireIndex](HumankindEmpire.md#EmpireIndex 'Modding.Humankind.DevTools.HumankindEmpire.EmpireIndex') ___ [HumankindSettlement](HumankindSettlement.md 'Modding.Humankind.DevTools.HumankindSettlement') ___ [HumankindEmpire](HumankindEmpire.md 'Modding.Humankind.DevTools.HumankindEmpire') ___</td></tr>
</tbody></table>
