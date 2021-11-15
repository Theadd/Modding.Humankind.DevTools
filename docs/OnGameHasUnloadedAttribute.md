# **OnGameHasUnloadedAttribute Class**

Any static method with the `OnGameHasUnloaded` annotation will be called everytime a running game ends.

```csharp
public class OnGameHasUnloadedAttribute : Attribute
```

### Remarks
<ul>
<li>

The method's class must be annotated with `DevToolsModule` for this to work.</li>
</ul>

