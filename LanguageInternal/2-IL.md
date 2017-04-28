# Intermediate Language

### Hello world program

```cs
public static 


```

So the intermediate language of a such program is :

```cil
.method private hidebysig static void Main() cil managed
{
  .entrypoint
  //Code size
  .maxstack 8
  
  IL_0000: nop //non-optimized build : by default, there is no optimization until you set a compilation flag
  IL_0001: ldstr "Hello, world"
  IL_0006: call void [mscorlib]System.Console::WriteLine(string)
  IL_000b: nop
  IL_000c: ret
} //end of method Program::Main
```
