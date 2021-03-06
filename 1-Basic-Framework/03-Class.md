
#### 1.Fields

##### 1.1 Field modifiers

|Type | Modifier name |
|:----|:--------------|
| static | static |
| Acess modifier | public, internal, private, protected |
| Instance modifier | new |
| Read-only modifier | readonly |
| Threading modifier | volatile |

##### 1.2 Filed initialization

Field initializers run __before__ contructor. (The same mechanism for the static class)

For instance:
```cs
public string FirstName = "Olivier";
```

##### 1.3 Get/Set & CLR

During the compilation, the CLR transforms the getter and the setter into two methods called get_XXX and set_XXX where XXX is the name of the field:

```cs
public string FirstName {get; set;}

//CLR compiles into:

public string get_FirstName{...}
public void set_FirstName(string value){...}
```


#####1.4 Inheritance modifier _new_


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

##### 1.5 Constants

###### 1.5.1 Properties of the constants

The properties of a constant are : 
- A _constant_ is a static field whose value can never change. 
- A constant is evaluated statically at compile time and the compiler literally substitutes its value whenever used.
- A constant has to have a value at the compile time
- A constant can be any of the built-in nulmeric types, bool, char, string or an enum type.
- A constant is more restrictive than a _static readonly_ field.
- A constant can not be static (see the allowed access modifiers)

###### 1.5.2 Access modifiers

| Access modifiers | Keywords |
|:-----------------|:---------|
| Access modifiers | public, internal, private, protected |
| Inheritance modifiers | new |


###### 1.5.3 Difference between const and readonly

- A readonly variable could be set in the constructor
- A readonly variable could be a reference type
- A readonly variable could be set at the run time
- A readonly variable could be static

#### 2.Methods

##### 2.1 Method modifiers

|Type | Modifier name |
|:----|:--------------|
| Static modifier | static |
| Access modifier | public, internal, private, protected |
| Inheritance modifier | new, virtual, abstract, override, sealed |
| Partial method modifie | partial |
| Unmanaged code modifiers | unsafe, extern |
| Asynchronous code modifier | async | 

##### 2.2 Access modifiers

|Access modifier | Purpose |
|:----|:--------------|
| public | The type or member can be accessed by any other code in the same assembly or another assembly that references it |
| private | The type or member can be accessed only by code in the same class or struct. |
| protected | The type or member can be accessed only by code in the same class or struct, or in a class that is derived from that class.|
| internal | The type or member can be accessed by any code in the same assembly, but not from another assembly.|
| protected internal | The type or member can be accessed by any code in the assembly in which it is declared, or from within a derived class in another assembly.Access from another assembly must take place within a class declaration that derives from the class in which the protected internal element is declared, and it must take place through an instance of the derived class type |

##### 2.3 Unmanaged code modifiers
###### 2.3.1 Extern

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

##### 2.4 Method overloading

In fact, the return type and the _params_ modifier are not part of a method's signature.

```cs
void MyMethod(int x)
string MyMethod(int x) //Compile-time error

void FirstMethod(int[] array)
void FirstMethod(params int[] array) //Compile-time error

```
But the way used to pass the parameter (either pass-by-value or pass-by-reference) is also a part of the signature:

```cs
void MyMetod(int x){...}
void MyMethod(ref int x){...} //OK
void MyMethod(out int x){...} //Compile-time error
```

##### 2.5 Method parameters

###### 2.5.1 Keywords

| Keyword | Goal |
|:--------|:-----|
| ref     | The ref keyword causes an argument to be passed by reference, not by value|
| out     | The out keyword causes arguments to be passed by reference. This is like the ref keyword, except that ref requires that the variable be initialized before it is passed.| 
| params  | A method parameter can take a variable number of arguments.|


###### 2.5.2 Examples

```cs
public class MyClass
{
    public static int Add(params int[] list)
    {
        int res = 0;
        
        foreach(int i in list)
        {
            res += i;
        }
        
        return res;
    }
}
var res1 = MyClass.Add(1,2);
var res2 = MyClass.Add(1,2,3,4,5);

Console.WriteLine("Result 1 : {0}", res1); //Result 1 : 3
Console.WriteLine("Result 2 : {0}", res2); //Result 2 : 15
```

#### 3 Constructor

##### 3.1 Constructor of the instances

###### 3.1.1 Constructor modifiers

| Modifiers | Keywords |
|:----------|----------|
| Access modifiers | public, internal, private, proctected |
| Unmanaged code modifiers | unsafe extern|

###### 3.1.2 Implicit parameterless constructor

For classes, the compiler automatically generates a _parameterless public constructor_ if and only if you do not define any constructor.
However, as soon as you define at least one constructor, the parameterless constructor is no longer automatically generated.

```cs
public class A
{
    public string GetClassName()
    {
        return "A";
    }
}

public class B
{
    private string className;
    
    public B(string param)
    {
        this.className = param;
    }
    
    public string GetClassName()
    {
        return "B";
    }
}

var a =  new A(); //OK
var b= new B(); //Compile-time error
```

##### 3.2 Static Constructor

###### 3.2.1 Properties

The properties of a static constructor are:
- a static constructor executes once per type.
- A type can define only one static constructor
- The only static constructor must be parameterless
- The constructeur must have the same name of the type
- The runtime automatically invokes the constructor just prior to the type being used.
- There are two ways to invoke the static constructor:
- - Instantiate the type
- - Accessing a static member in the type

###### 3.2.2 The static constructor modifiers

| Modifiers | Keywords |
|:----------|----------|
| Unmanaged code modifiers | unsafe extern|

###### 3.2.3 Examples

In this example, the static constructor has been invoked when we want to get the value of the field Count.

```cs
public class Person
{
    public static int Count;
    
    private string FirstName {get;set;}
    private string LastName {get;set;}
    private int Age {get;set;}
    
    
    public Person(string firstName, string lastName, int age)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
    }
    
    //No access modifier must be add otherwise a compile-time error will be raised
    static Person()
    {
        Count = -10;
    }
}

Console.WriteLine(Person.Count); //-10
```

Now, the static constructor will be invoked when we will instantiate a new person:

```cs
public class Person
{
    public static int Count;
    
    private string FirstName {get;set;}
    private string LastName {get;set;}
    private int Age {get;set;}
    
    
    public Person(string firstName, string lastName, int age)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
    }
    
    
    static Person()
    {
        Count = -10;
        Console.WriteLine("The static constructor is invoked");
    }
}

var person = new Person("Olivier", "Mounicq", 39);

//Output : The static constructor is invoked
```

And the static constructor is only called one time:

```cs
public class Person
{
    public static int Count;
    private string FirstName {get;set;}
    private string LastName {get;set;}
    private int Age {get;set;}
    public Person(string firstName, string lastName, int age)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        Console.WriteLine("The instance constructor is invoked");
    }
    static Person()
    {
        Count = -10;
        Console.WriteLine("The static constructor is invoked");
    }
}
var albertEinstein = new Person("Albert","Einstein",137);
var richardFeynman = new Person("Richard","Feynmann",98);

//The output:
The static constructor is invoked
The instance constructor is invoked
The instance constructor is invoked
```

#### 4. Access modifier by default

|Type | Access modifier by default |
|:----|:--------------|
| members of an interface | public |
| members of a structure | public |
| non-nested types | internal |
| members of class | private |
| members of struct | private |

Non-nested types, enumeration and delegate accessibilities (may only have internal or public)

| Type | Default access modifier | Permitted declared accessibilities |
|:-----|:------------------------|:-----------------------------------|
|namespace            | public    | none (always implicitly public)   |
|enum                 | public    | none (always implicitly public)   |
|interface            | internal  | public, internal                  |
|class                | internal  | public, internal                  |
|struct               | internal  | public, internal                  |
|delegate             | internal  | public, internal                  |

Nested-type and member accessibilities

| Type               | Default   | Permitted declared accessibilities |
|:-------------------|:----------|:-----------------------------------|
|namespace            | public    | none (always implicitly public)    |
|enum                 | public    | none (always implicitly public)    |
|interface            | public    | none                               |
|class                | private   | All¹                               |
|struct               | private   | public, internal, private²         |
|delegate             | private   | All¹                               |
|constructor          | private   | All¹                               |
|interface member     | public    | none (always implicitly public)    |
|method               | private   | All¹                               |
|field                | private   | All¹                               |
|user-defined operator| none      | public (must be declared public)   |


¹ All === public, protected, internal, private, protected internal

² structs cannot inherit from structs or classes (although they can, interfaces), hence protected is not a valid modifier

[What are the default access modifier in C#](http://stackoverflow.com/questions/2521459/what-are-the-default-access-modifiers-in-c)

#### 5. Abstract class

##### 5.1 Properties

The properties of an absract class are:
- an abstract class can have a constructor
- an abstract class cannot be instanciate
- an abstract may contains only non abstract method

If a method is declared as abstract, the class containing this method must be declared as abstract

```cs

public abstract class BaseClass
{
    private readonly string ClassName;
    
    public BaseClass(string className)
    {
        this.ClassName = className;
    }
    
    public string GetClassName()
    {
        return ClassName;
    }    
}
public class MyClass : BaseClass
{
    public MyClass():base("MyClass")
    {}
}
var myClass = new MyClass();
Console.WriteLine(myClass.GetClassName()); //MyClass

```
##### 5.2 Abstract class vs interface

Obvioulsy, defining an abstract class with abstract members has the same effect to defining an interface.
But the purposes are differents:
- an abstract is made to factorise the code
- an interface is made to define a service contract
- a class can inherit one or more interfaces, but only one abstract class.
- the members of the interface are public with no implementation.
- an abstract classes can have protected parts, static methods, etc.
- Abstract classes can add more functionality without destroying the child classes that were using the old version. In an interface, creation of additional functions will have an effect on its child classes, due to the necessary implementation of interface methods to classes.

#### 6 Inheritance

##### 6.1 Constructor and field initialization order


##### 6.2 Constructor rules




##### 6.3 Do not call overridable methods from a constructor

cf see [CA2214: Do not call overridable methods in constructors](https://msdn.microsoft.com/en-us/library/ms182331.aspx?f=255&MSPPError=-2147217396)

For instance:

```cs
public class BaseClass
{
    protected bool IsInitialized = false;
    
    public virtual void GetInfo()
    {
        Console.WriteLine("Base Class - IsInitialized : {0}", IsInitialized);
    }
    
    public BaseClass()
    {
        Console.WriteLine("Base class contructor");
        this.GetInfo();
    }
}
public class InheritedClass : BaseClass
{
    public override void GetInfo()
    {
        Console.WriteLine("Inherited Class - IsInitialized : {0}", IsInitialized);
    }
    
    public InheritedClass()
    {
        Console.WriteLine("Inherited class contructor");
        this.GetInfo();
    }
}
var myClass = new InheritedClass();

//Output:
//Base class contructor
//Inherited Class - IsInitialized : False
//Inherited class contructor
//Inherited Class - IsInitialized : False

```

As we can see both constructor call the virtual method of instance : the one of the inherited class. 
But the field named IsInitalized has been 


#### 7 Overriding / Hidding
##### 7.1 Virtual / Override

###### 7.1.1 Defintion 

The _virtual_ keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class.

###### 7.1.2 Example

```cs
public class A
{
    public virtual string GetClassName()
    {
        return "A";
    }
}
public class B : A
{
}
public class C : A
{
    public override string GetClassName()
    {
        return "C";
    }
}
public class D : C
{
    public override string GetClassName()
    {
        return "D";
    }
}
var a = new A();
var b = new B();
var c = new C();
var d = new D();
Console.WriteLine(a.GetClassName()); //A
Console.WriteLine(b.GetClassName()); //A
Console.WriteLine(c.GetClassName()); //C
Console.WriteLine(d.GetClassName()); //D
```

###### 7.1.3 virtual vs abstract

-An __abstract function__ has to have no functionality. You're basically saying, any child class MUST give their own version of this method, however it's too general to even try to implement in the parent class.

-A __virtual function__, is basically saying look, here's the functionality that may or may not be good enough for the child class. So if it is good enough, use this method, if not, then override me, and provide your own functionality.

##### 7.2 Overriding vs Hidding

###### 7.2.1 The keywords
The overriding (on instance):
- abstract /ovrerride
- virtual/override

The hidding:
- new / virual

###### 7.2.2 Difference between the overriding and the hidding

The _overriding_ is: 
- Used in polymorphism implementation 
- Includes same method name and same params 
- Used in method overriding 
- It is also called runtime polymorphism
- Causes late binding (at the runtime)

The _hidding_ is:
- It is also used in polymorphism concept
- Includes the same method name and different params
- Used in method overriding
- It is compile-time polymorphism
- Causes early binding (at the compile time)

###### 7.2.3 Example

__Overrding example__

```cs
public class OverridingA
{
    public virtual string GetClassName()
    {
        return "OverridingA";
    }
}
public class OverridingB : OverridingA
{
    public override string GetClassName()
    {
        return "OverridingB";
    }
}
OverridingA a = new OverridingA();
OverridingB b = new OverridingB();
OverridingA bAsA = new OverridingB();

Console.WriteLine(a.GetClassName());    //OverridingA
Console.WriteLine(b.GetClassName());    //OverridingB
Console.WriteLine(bAsA.GetClassName()); //OverridingB
```

__Hidding example__

```cs
public class HiddingA
{
    public string GetClassName()
    {
        return "OverridingA";
    }
}
public class HiddingB : HiddingA
{
    new public int GetClassName()
    {
        return 1;
    }
}
HiddingA a = new HiddingA();
HiddingB b = new HiddingB();
HiddingA bAsA = new HiddingB();

Console.WriteLine(a.GetClassName());    //OverridingA
Console.WriteLine(b.GetClassName());    //1
Console.WriteLine(bAsA.GetClassName()); //OverridingA
```


#### 8 Casting

##### 8.1 Definitions

- _upcast_ to a base class reference (always succeed)
- _downcast_ to a subclass reference (not always succeed : InvalidCastException)

##### 8.2 _as_ operator

###### 8.2.1 Limitation

We can only use the _as_ operator with reference type or nullable type.

```cs
int? x = 12;
long? y = x as long?; //OK

int x = 12;
long y = x as long; //KO

```

###### 8.2.1 The raised exception

Beware before to use the operator _as_ :

```cs
class A
{
    public string GetClassName()
    {
        return "A";
    }
}

class B
{
    public string GetClassName()
    {
        return "B";
    }
}

var a = new A();
var b = new B();

string res1 = ((A)b).GetClassName();    //InvalidCastException
string res2 = (b as A).GetClassName();  //NullReferenceException
```

First method raise an error which is more accurate.
