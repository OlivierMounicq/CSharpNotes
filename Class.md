
####1.Fields

#####1.1 Field modifiers

|Type | Modifier name |
|:----|:--------------|
| static | static |
| Acess modifier | public, internal, private, protected |
| Instance modifier | new |
| Read-only modifier | readonly |
| Threading modifier | volatile |

#####1.2 Filed initialization

Field initializers run __before__ contructor.

For instance:
```cs
public string FirstName = "Olivier";
```

#####1.3 Inheritance modifier _new_


```cs

public class A
{
    public string myStr = "A";
}
public class B : A
{
    new public string myStr = "B";
}
var a = new A();
var b = new B();
Console.WriteLine(a.myStr); //A
Console.WriteLine(b.myStr); //B

```
