##Covariance / Contravariance

#### Covariance

```cs
public class Person
{
    protected string FirstName;
    
    protected string LastName;
    
    public Person(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
    
    public virtual void GetData()
    {
        Console.WriteLine("Person : {0} {1}", this.FirstName, this.LastName);
    }
}

public class Teacher : Person
{
    protected string Activity;
    
    public Teacher(string firstName, string LastName, string activity)
        :base(firstName, LastName)
    {
        this.Activity = activity;
    }
    
    public override void GetData()
    {
        Console.WriteLine("Teacher : {0} {1} - Activity : {2}", this.FirstName, this.LastName, this.Activity);
    }    
}

public class TennisPlayer : Person
{
    protected int Rank;
    
    public TennisPlayer(string firstName, string LastName, int rank)
        :base(firstName, LastName)
    {
        this.Rank = rank;          
    }
    
    public override void GetData()
    {
        Console.WriteLine("Tennis Player : {0} {1} - ATP Rank : {2}", this.FirstName, this.LastName, this.Rank);
    } 
}

//We define a covariant interface
public interface IPoppable<out T>
{
    T Pop();
}    

//We create a type inherited the previous interface    
public class CovariantStack<T> : Stack<T>, IPoppable<T>
{
}

//A stack with covariance
var teacherStack = new CovariantStack<Teacher>();

teacherStack.Push(new Teacher("Richard","Feynman","Physics"));
teacherStack.Push(new Teacher("Albert", "Einstein","Physics"));

//Because the interface implements
IPoppable<Person> personStack = teacherStack;

personStack.Pop().GetData();
personStack.Pop().GetData();

```










#####Links

[The theory behind covariance and contravariance in C# 4](http://tomasp.net/blog/variance-explained.aspx/)
[Introduction à la Covariance / Contravariance](https://sebastiencourtois.wordpress.com/2010/04/14/nouveauts-c-net-4-introduction-la-covariance-contravariance/)
