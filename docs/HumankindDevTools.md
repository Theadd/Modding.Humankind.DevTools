# **HumankindDevTools Class**

Development related tools.

```csharp
public static class HumankindDevTools
```

<table width="100%"><caption>

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
void LoadModule(Type moduleType)
```
</td><td align="left" valign="top">

### LoadModule
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Registers all members of a class Type with DevTool's attribute annotations.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>moduleType</kbd> → A class Type with members annotated with DevTools attributes.</li>
</ul></details></td></tr>
<tr><td align="left" valign="top">

```csharp
void ReloadAllModules()
```
</td><td align="left" valign="top">

### ReloadAllModules
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Reloads all registered modules.</td></tr>
<tr><td align="left" valign="top">

```csharp
void UnloadModule(Type moduleType)
```
</td><td align="left" valign="top">

### UnloadModule
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Unregisters all members of a class Type after invoking any method with `[OnGameHasUnloaded]` attribute.<details><summary><code>PARAMETERS</code></summary><ul><li>
<kbd>moduleType</kbd> →</li>
</ul></details></td></tr>
</tbody></table>
