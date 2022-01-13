# **InGameKeyboardShortcutAttribute Class**

Binds an static method as the action to invoke once the specified `KeyCode`s are pressed.

```csharp
public class InGameKeyboardShortcutAttribute : Attribute
```

### Remarks
<ul>
<li>

The method's class must be annotated with [DevToolsModule](DevToolsModuleAttribute.md 'Modding.Humankind.DevTools.DevToolsModuleAttribute') attribute, otherwise it will be ignored.</li>
<li>

These shortcuts will be available **in-game only**. Not in main menu, neither when game is still loading.</li>
<li>

It is a simplified version of BepInEx's `KeyboardShortcut` which ensures that the action will be triggered only once each time it is pressed.</li>
</ul>

### Constructors

<a name='InGameKeyboardShortcutAttribute(string_KeyCode_KeyCode__)'></a>

## InGameKeyboardShortcutAttribute(string, KeyCode, KeyCode[]) Constructor

Binds an static method as the action to invoke once the specified `KeyCode`s are pressed.

```csharp
public InGameKeyboardShortcutAttribute(string actionName, KeyCode mainKey, params KeyCode[] modifierKeys);
```
#### Parameters

<a name='actionName'></a>

`actionName` [System.String](#System.String 'System.String')

Unique string representing this keyboard shortcut.

<a name='mainKey'></a>

`mainKey` [UnityEngine.KeyCode](#UnityEngine.KeyCode 'UnityEngine.KeyCode')

[UnityEngine.KeyCode](https://docs.unity3d.com/ScriptReference/KeyCode.html 'https://docs.unity3d.com/ScriptReference/KeyCode.html')

<a name='modifierKeys'></a>

`modifierKeys` [UnityEngine.KeyCode](#UnityEngine.KeyCode 'UnityEngine.KeyCode')[[]](#System.Array 'System.Array')

`UnityEngine.KeyCode` representing modifier keys.