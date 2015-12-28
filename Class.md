
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

####2.Methods

#####2.1 Method modifiers

|Type | Modifier name |
|:----|:--------------|
| Static modifier | static |
| Access modifier | public, internal, private, protected |
| Inheritance modifier | new, virtual, abstract, override, sealed |
| Partial method modifie | partial |
| Unmanaged code modifiers | unsafe, extern |
| Asynchronous code modifier | async | 

######2.1.1 Extern

The extern modifier is used to declare a method that is implemented externally.A common use of the extern modifier is with the DllImport attribute when you are using Interop services to call into unmanaged code.In this case, the method must also be declared as static, as shown in the following example:

```cs
//using System.Runtime.InteropServices;
class ExternTest
{
    [DllImport("User32.dll", CharSet=CharSet.Unicode)] 
    public static extern int MessageBox(IntPtr h, string m, string c, int type);

    static int Main()
    {
        string myString;
        Console.Write("Enter your message: ");
        myString = Console.ReadLine();
        return MessageBox((IntPtr)0, myString, "My Message Box", 0);
    }

}
```

######2.1.1 new



####3. Access modifier by default

|Type | Access modifier by default |
|:----|:--------------|
| members of an interface | public |
| members of a structure | public |
| non-nested types | internal |
| members of class | private |
| members of struct | private |


