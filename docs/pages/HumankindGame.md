# **HumankindGame Class**

Main starting class to work with for anything that deals with in-game state.

```csharp
public static class HumankindGame
```

### Remarks
<ul>
<li>

Most members are unreliable when `IsGameLoaded` is false.</li>
</ul>


<table width="100%"><caption>

### **GENERAL**  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

<a name="Empires" class="anchor"><blockquote>

```csharp
public static HumankindEmpire[] Empires
```
</blockquote></a>
</td><td align="left" valign="top">



An array of `HumankindEmpire`'s present in the current game, if any.</td></tr>
<tr><td align="left" valign="top">

<a name="GameID" class="anchor"><blockquote>

```csharp
public static int GameID
```
</blockquote></a>
</td><td align="left" valign="top">



A unique number for every started new game.</td></tr>
<tr><td align="left" valign="top">

<a name="GameSpeedDefinition" class="anchor"><blockquote>

```csharp
public static GameSpeedDefinition GameSpeedDefinition
```
</blockquote></a>
</td><td align="left" valign="top">



Gets current game's `GameSpeedDefinition` declared in `Amplitude.Mercury.Data.Simulation` namespace.</td></tr>
<tr><td align="left" valign="top">

<a name="GameSpeedLevel" class="anchor"><blockquote>

```csharp
public static int GameSpeedLevel
```
</blockquote></a>
</td><td align="left" valign="top">



Returns an integer representing current's game speed, values range from 1 to 7 where 2 is Endless game speed and 6 is Blitz game speed. 1 and 7 were introduced in case the user is playing with a modded game where speed value multipliers make it faster than blitz (7) or slower than endless (1).</td></tr>
<tr><td align="left" valign="top">

<a name="IsGameLoaded" class="anchor"><blockquote>

```csharp
public static bool IsGameLoaded
```
</blockquote></a>
</td><td align="left" valign="top">



Whether a game is fully loaded and ready to play with.</td></tr>
<tr><td align="left" valign="top">

<a name="IsNewGame" class="anchor"><blockquote>

```csharp
public static bool IsNewGame
```
</blockquote></a>
</td><td align="left" valign="top">



Whether current game (if any) is a continuation of a previously saved game or a newly started one.</td></tr>
<tr><td align="left" valign="top">

<a name="Turn" class="anchor"><blockquote>

```csharp
public static int Turn
```
</blockquote></a>
</td><td align="left" valign="top">



Current game's turn,</td></tr>
<tr><td align="left" valign="top">

<a name="ToString" class="anchor"><blockquote>

```csharp
string ToString()
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/method.svg" alt="Method" height="16px"/>

Returns a string that represents the current HumankindGame in a formatted table with all empires and many of their values found in `HumankindEmpire` class.</td></tr>
<tr><td align="left" valign="top">

<a name="OnNewTurnStart" class="anchor"><blockquote>

```csharp
public static event Action OnNewTurnStart;
```
</blockquote></a>
</td><td align="left" valign="top">

<img src="../resources/event.svg" alt="Event" height="16px"/>

Add/Remove Action handlers to be called at the start of every turn.<br/><sub><details open><summary><code>REMARKS</code></summary><ul>
<li>

All registered actions are automatically removed when game unloads and before a game loads.</li>
</ul></details></sub>
</td></tr>
</tbody></table>
