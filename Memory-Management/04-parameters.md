#Parameters in C#

###Reference type and paramter

```cs
public class Person
{
    public string FirstName {get; set;}
    
    public string LastName {get; set;}
    
    public Person(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
    public override string ToString()
    {
        return string.Format("{0} {1}", this.FirstName, this.LastName);    
    }
}

public class MyClass
{
    public void Change(Person p)
    {
        p.FirstName = "Richard";
        Console.WriteLine(p.ToString());
    }
    
    public void ChangeNew(Person p)
    {
        p = new Person("Richard","Feynman");
        Console.WriteLine(p.ToString());
    }
    
    public void ChangeNewRef(ref Person p)
    {
        p = new Person("Marie","Curie");
        Console.WriteLine(p.ToString());
    }
    
}

var myClass = new MyClass();

var p1 = new Person("Albert","Einstein");
var p2 = new Person("Albert","Einstein");
var p3 = new Person("Albert","Einstein");

Console.WriteLine("====================================================");

Console.WriteLine(p1.ToString());
myClass.Change(p1);
Console.WriteLine(p1.ToString());

Console.WriteLine("====================================================");

Console.WriteLine(p2.ToString());
myClass.ChangeNew(p2);
Console.WriteLine(p2.ToString());

Console.WriteLine("====================================================");

Console.WriteLine(p3.ToString());
myClass.ChangeNewRef(p3);
Console.WriteLine(p3.ToString());
```

[csharppad.com](http://csharppad.com/gist/4bd34d5e7f991ef4657786f531712d6e)

