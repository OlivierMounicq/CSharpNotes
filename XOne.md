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





