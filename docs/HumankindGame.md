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

## GENERAL  
</caption><thead><tr><th>MEMBER</th><th>DOCUMENTATION</th></tr></thead>
<tbody>
<tr><td align="left" valign="top">

```csharp
public static HumankindEmpire[] Empires
```
</td><td align="left" valign="top">

### Empires

An array of `HumankindEmpire`'s present in the current game, if any.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static int GameID
```
</td><td align="left" valign="top">

### GameID

A unique number for every started new game.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static GameSpeedDefinition GameSpeedDefinition
```
</td><td align="left" valign="top">

### GameSpeedDefinition

Gets current game's `GameSpeedDefinition` declared in `Amplitude.Mercury.Data.Simulation` namespace.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static int GameSpeedLevel
```
</td><td align="left" valign="top">

### GameSpeedLevel

Returns an integer representing current's game speed, values range from 1 to 7 where 2 is Endless game speed and 6 is Blitz game speed. 1 and 7 were introduced in case the user is playing with a modded game where speed value multipliers make it faster than blitz (7) or slower than endless (1). Test: [GameSpeedLevel](GameSpeedLevel.md 'GameSpeedLevel.md') and [GameSpeedLevel](HumankindGame_GameSpeedLevel.md 'Modding.Humankind.DevTools.HumankindGame.GameSpeedLevel').</td></tr>
<tr><td align="left" valign="top">

```csharp
public static bool IsGameLoaded
```
</td><td align="left" valign="top">

### IsGameLoaded

Whether a game is fully loaded and ready to play with.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static bool IsNewGame
```
</td><td align="left" valign="top">

### IsNewGame

Whether current game (if any) is a continuation of a previously saved game or a newly started one.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static int Turn
```
</td><td align="left" valign="top">

### Turn

Current game's turn,</td></tr>
<tr><td align="left" valign="top">

```csharp
string ToString()
```
</td><td align="left" valign="top">

### ToString
<img src="./resources/method.svg" alt="Method" height="16px"/><br/>
Returns a string that represents the current HumankindGame in a formatted table with all empires and many of their values found in `HumankindEmpire` class.</td></tr>
<tr><td align="left" valign="top">

```csharp
public static event Action OnNewTurnStart;
```
</td><td align="left" valign="top">

### OnNewTurnStart
<img src="./resources/event.svg" alt="Event" height="16px"/><br/>
Add/Remove Action handlers to be called at the start of every turn.<br/><sub><details open><summary><code>REMARKS</code></summary><ul>
<li>

All registered actions are automatically removed when game unloads and before a game loads.</li>
</ul></details></sub>
</td></tr>
</tbody></table>
