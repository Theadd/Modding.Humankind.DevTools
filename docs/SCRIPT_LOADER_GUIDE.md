# Hot Reloading with BepInEx.ScriptLoader

BepInEx.ScriptLoader can be found [here](https://github.com/ghorsington/BepInEx.ScriptLoader).

Here's a simple example script (`<HumankindGameDirectory>/scripts/MyScript.cs`).

```csharp
// #ref ${BepInExRoot}/plugins/Modding.Humankind.DevTools.dll
using UnityEngine;
using Modding.Humankind.DevTools;

[DevToolsModule]
public static class MyScript {
	
	public static string Name => !Unloaded ? "MyScript" : "--invalid--";
	public static bool Unloaded = false;
	
    public static void Main() {
        HumankindDevTools.LoadModule(typeof(MyScript));
    }

    public static void Unload() {
        // Unload and unpatch everything before reloading the script
		HumankindDevTools.UnloadModule(typeof(MyScript));
		Unloaded = true;
    }
	
	[InGameKeyboardShortcut("Execute", KeyCode.E, KeyCode.LeftAlt)]
    public static void Execute()
    {
        // Your code goes here...
		// 
		// EXAMPLE: Spawns a Knight cavalry unit in the capital of each human player.
		// HumankindGame.Empires
		// 	.IsControlledByHuman()
		// 	.Settlements()
		// 	.IsCapital()
		// 	.BuildUnitByName("LandUnit_Era3_Common_Knights");
    }
}
```

---

Another example script with different structure.

```csharp
// #ref ${BepInExRoot}/plugins/Modding.Humankind.DevTools.dll
using UnityEngine;
using Modding.Humankind.DevTools;

public static class MyScript {
	public static bool Unloaded = false;
	
    public static void Main() {
        HumankindDevTools.LoadModule(typeof(MyCustomModule));
    }

    public static void Unload() {
        // Unload and unpatch everything before reloading the script
		HumankindDevTools.UnloadModule(typeof(MyCustomModule));
		Unloaded = true;
    }
}

[DevToolsModule]
public static class MyCustomModule
{
    public static string Name => !MyScript.Unloaded ? "MyCustomModule" : "--invalid--";

    [OnGameHasLoaded]
    public static void OnGameHasLoaded()
    {
        // Register OnNewTurnStart actions here.
    }

    [OnGameHasUnloaded]
    public static void OnGameHasUnloaded()
    {
        // Unregister OnNewTurnStart actions here.
    }

    [InGameKeyboardShortcut("Run", KeyCode.R, KeyCode.LeftAlt)]
    public static void Run()
    {
        // Your code goes here...
		// 
		// EXAMPLE: Spawns a Knight cavalry unit in the capital of each human player.
		// HumankindGame.Empires
		// 	.IsControlledByHuman()
		// 	.Settlements()
		// 	.IsCapital()
		// 	.BuildUnitByName("LandUnit_Era3_Common_Knights");
    }
}
```
