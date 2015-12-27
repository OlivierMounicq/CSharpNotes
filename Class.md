
###1.Fields

####1.1 Field modifiers

|Type | Modifier name |
|:----|:--------------|
| static | static |
| Acess modifier | public |
|                | internal |
|                | private |
|                | protected |
| Instance modifier | new |
| unsafe code modifier | readonly |
| Threading modifier | volatile |




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
