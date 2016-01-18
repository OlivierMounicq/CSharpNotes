##Covariance / Contravariance

#### Covariance

##### Definition

Let B is convertible to A, X has a covariant type parameter if X<B> is convertible to X<A>.


##### The modifier _out_

To decalare a type parameter as covariant, you must ass the modifier _out_. (See the below example).

##### Example

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

##### IEnumerable

The interface IEnumerable is convariant. So, for instance: 

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

var teachersList = new List<Teacher>();
teachersList.Add(new Teacher("Richard","Feynmann","Physics"));

//The interface Ienumerable is covariant
IEnumerable<Person> personList = teachersList;

var enumerator = personList.GetEnumerator();

while(enumerator.MoveNext())
{
    enumerator.Current.GetData();
}

```



##### Interface & Covariance

In the .net framework 4.0, the following interfaces are _covariant_ : 


| Interface |
|:---------|
| ``` Converter<TInput, TOuput> ```|
| ``` Func<TResult> ``` |
| ``` Func<T1, TResult> ```|
| ... |
| ``` Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> ```|
| ``` IEnumerable<T> ```|
| ``` IEnumerator<T> ```|
| ``` IGrouping<TKey, TElement> ```|
| ``` IOrderedEnumerable<TElement> ```|
| ``` IOrderedQueryable<T> ```|
| ``` IQueryable<T> ```|









#####Links

[The theory behind covariance and contravariance in C# 4](http://tomasp.net/blog/variance-explained.aspx/)

[Introduction à la Covariance / Contravariance](https://sebastiencourtois.wordpress.com/2010/04/14/nouveauts-c-net-4-introduction-la-covariance-contravariance/)

[Covariance and Contravariance in Generics](https://msdn.microsoft.com/fr-fr/library/dd799517%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396#InterfaceCovariantTypeParameters)
