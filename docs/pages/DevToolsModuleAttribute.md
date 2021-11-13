# **DevToolsModuleAttribute Class**

In order to use any other DevTool's attribute in a class member, that class must be annotated with this  
attribute for them to work as they're only searched in classes with the `DevToolsModule` annotation.

```csharp
public class DevToolsModuleAttribute : Attribute
```