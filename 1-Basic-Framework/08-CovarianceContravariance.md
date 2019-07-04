# Covariance / Contravariance

## 1/ Covariance

### 1.1 Definition

Let B is convertible to A, X has a covariant type parameter if X&lt;B&gt; is convertible to X&lt;A&gt;.


### 1.2 The modifier _out_

To decalare a type parameter as covariant, you must ass the modifier _out_. (See the below example).

### 1.3 Example 1

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

### 1.4 IEnumerable and the covariance

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

### 1.5 Exemple 2

```cs
public class A
{
    public override string ToString()
    {
        return "class A";    
    }
}
public class B : A
{
    public override string ToString()
    {
        return "class B";    
    }
}

var listB = new List<B>();
listB.Add(new B());
listB.Add(new B());

var enumB = listB.AsEnumerable<B>();

IEnumerable<A> enumA = enumB;

foreach(var a in enumA)
{
    Console.WriteLine(a.ToString());        
}

//The output
//class B
//class B
```

### 1.5 Exemple 3

```
public class A
{
    public override string ToString()
    {
        return "class A";    
    }
}
public class B : A
{
    public override string ToString()
    {
        return "class B";    
    }
}
public void PrintA(IEnumerable<A> enumA)
{
    foreach(var a in enumA)
    {
        Console.WriteLine(a.ToString());    
    }
}
var listB = new List<B>();
listB.Add(new B());
listB.Add(new B());

PrintA(listB);

//The output
//class B
//class B
```cs



### 1.5 Interface & Covariance

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


## 2/ The contravariance






## 3/ Links

[The theory behind covariance and contravariance in C# 4](http://tomasp.net/blog/variance-explained.aspx/)

[Introduction à la Covariance / Contravariance](https://sebastiencourtois.wordpress.com/2010/04/14/nouveauts-c-net-4-introduction-la-covariance-contravariance/)

[Covariance and Contravariance in Generics](https://msdn.microsoft.com/fr-fr/library/dd799517%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396#InterfaceCovariantTypeParameters)
