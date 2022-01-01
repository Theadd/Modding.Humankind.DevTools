# **OnGameHasLoadedAttribute Class**

Any static method with the `OnGameHasLoaded` annotation will be called everytime a game has loaded and it is ready to play.

```csharp
public class OnGameHasLoadedAttribute : Attribute
```

### Remarks
<ul>
<li>

The method's class must be annotated with `DevToolsModule` for this to work.</li>
<li>

Method's signature must be: public static void</li>
</ul>

