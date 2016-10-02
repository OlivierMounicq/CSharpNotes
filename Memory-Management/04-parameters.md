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
        p.ToString();
    }
    
    public void ChangeNew(Person p)
    {
        p = new Person("Richard","Feynman");
        p.ToString();
    }
    
    public void ChangeNewRef(ref Person p)
    {
        p = new Person("Marie","Curie");
        p.ToString();
    }
    
}

var myClass = new MyClass();

var p1 = new Person("Albert","Einstein");
var p2 = new Person("Albert","Einstein");
var p3 = new Person("Albert","Einstein");

p1.ToString();
myClass.Change(p1);
p1.ToString();

p2.ToString();
myClass.Change(p2);
p2.ToString();

p3.ToString();
myClass.Change(p3);
p3.ToString();
````



