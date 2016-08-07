####Operator ++

```cs
int count = 1;
Console.WriteLine(100*count++);
Console.WriteLine(100*++count);

//Output
//100
//300
```

####Numeric operator on short

This code produce a compile time error : 

_error CS0266: Cannot implicitly convert type 'int' to 'short'. An explicit conversion exists (are you missing a cast?)_

```cs
short x = 1;
short y = 1;
short z;

z = x + y;
```

Actually, we have to cast the result return by the operator:

```cs
short x = 1;
short y = 1;
short z;
z = (short)(x + y);
```

####A method to calculate the age


```cs
public class MyClass
{
    public static int CalculerAge(DateTime birthday)
    {
        if(DateTime.Today.Month > birthday.Month && DateTime.Today.Month > birthday.Month && DateTime.Today.Day < birthday.Day)
        {
            throw new Exception("Wrong birth date");
        }
        
        int age = DateTime.Today.Year - birthday.Year;
        
        if(DateTime.Today.Month < birthday.Month && DateTime.Today.Day < birthday.Day)
        {
            age--;
        }
        
        return age;
    }
}
var OlivierBirth = new DateTime(1976,5,7);
var OlivierAge = MyClass.CalculerAge(OlivierBirth);
Console.WriteLine(OlivierAge);

OlivierBirth = new DateTime(1976,1,2);
OlivierAge = MyClass.CalculerAge(OlivierBirth);
Console.WriteLine(OlivierAge);

OlivierBirth = new DateTime(2016,5,7);
OlivierAge = MyClass.CalculerAge(OlivierBirth);
Console.WriteLine(OlivierAge);

//Console:
//39
//40
//Exception : ...

```


#### String and the operators == & !=

The operateurs __==__ and __!=__ have been defined to compare the value of the string.

```cs
string str = "TOTO";
string ing = "TOTO";
Console.WriteLine(str == ing);
```


#### Virtual and public/protected

```cs

public class A
{
    public virtual void GetData()
    {
        Console.WriteLine("A class of A type");
    }
}

public class B : A
{
    public virtual void GetData()
    {
        Console.WriteLine("A class of B type");
    }
}

public class C : A
{
    protected virtual void GetData()
    {
        Console.WriteLine("A class of C type");
    }
}

public class D : A
{
    public override void GetData()
    {
        Console.WriteLine("A class of D type");
    }
}

(new A()).GetData(); //A class of A type
(new B()).GetData(); //A class of B type
(new C()).GetData(); //A class of A type !!!!!
(new D()).GetData(); //A class of D type
```


#### Factorial & Recursivity

```cs

public static int Factorial(int i)
{
    if(i > 1)
    {
        return i * Func(i-1);
    }
    
    return i;
}

Factorial(5) //120

```


