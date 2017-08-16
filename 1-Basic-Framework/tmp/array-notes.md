### Initialization by type inference

```cs
public interface IPerson
{
    string GetInfo();    
}
public class Physicist: IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Physicist(string FirstName, string lastName)
    {
        this.FirstName = FirstName;
        this.LastName = lastName;
    }
    
    public string GetInfo()
    {
        return string.Format("Physicist : {0} {1}", this.FirstName, this.LastName);    
    }
}
public class Athlete: IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Athlete(string FirstName, string lastName)
    {
        this.FirstName = FirstName;
        this.LastName = lastName;
    }
    
    public string GetInfo()
    {
        return string.Format("Physicist : {0} {1}", this.FirstName, this.LastName);    
    }    
}
public class Foo
{
    public static void Run()
    {
        var  physicist = new Physicist("Albert","Einstein");
        var athlete = new Athlete("Valentino","Rossi");
        
        var array = new { physicist, athlete };
        
    }
}
```

