# The statments

### For vs Foreach

The iterator inside the _foreach_ loop is __immutable__.

```cs

for(int i=0; i < 10; i++)
{
    i =i+2;
    Console.WriteLine(i);
}
```

And the output is

```cs
2
5
8
11
```

But if we use a _foreach_ loop, this code won't be compiled:

```cs
var lst = new IList<int>(){0,1,2,3,4,5,6,7,8,9};

foreach(int i in lst)
{
    i = i + 2;
    Console.WriteLine(i);
}
```

The error is :

```cs
error CS0144: Cannot create an instance of the abstract class or interface 'IList<int>'
````


