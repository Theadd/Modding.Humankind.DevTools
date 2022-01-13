# **HumankindEmpireExtensions Class**

```csharp
public static class HumankindEmpireExtensions
```

<table width="100%"><caption>

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
IEnumerable<Army> Armies(this IEnumerable<HumankindEmpire> sequence)
```
</td><td align="left" valign="top">

### Armies
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Aggregates the Armies of each `HumankindEmpire` in the sequence into a new sequence of armies. also: [HumankindEmpire.Armies](HumankindEmpire.md#Armies 'Modding.Humankind.DevTools.HumankindEmpire.Armies').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindEmpire> IsControlledByHuman(this IEnumerable<HumankindEmpire> sequence, bool isControlledByHuman)
```
</td><td align="left" valign="top">

### IsControlledByHuman
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Filters out all non human player empires from a sequence of `HumankindEmpire`s. Except when `false` is passed to `isControlledByHuman`, which will return a sequence with every empire not controlled by human instead. See also: [HumankindEmpire.IsControlledByHuman](HumankindEmpire.md#IsControlledByHuman 'Modding.Humankind.DevTools.HumankindEmpire.IsControlledByHuman').<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> → this</li>
<li>
<kbd>isControlledByHuman</kbd> → Set this to false to reverse the filter, defaults to true.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
IEnumerable<HumankindSettlement> Settlements(this IEnumerable<HumankindEmpire> sequence)
```
</td><td align="left" valign="top">

### Settlements
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Selects all `HumankindSettlement`s from the `HumankindEmpire`s sequence.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>sequence</kbd> →</li>
</ul></details></td></tr>
</tbody></table>
