###The namespace

| namespace                       |The collections                           |
|:--------------------------------|:-----------------------------------------|
| System.Collections              | Non generic collection                   |
| System.Collections.Specialized  | Strongly typed non-generic collection    |
| System.Collections.Generic      | Generic collection                       |
| System.Collections.ObjectModel  | Proxies and bases for custom collections |
| System.Collections.Concurrent   | Thread-safe collections                  |


###The interfaces

| Non Generic        | Generic                     | Features         | Implemented in  |
|:-------------------|:----------------------------|:-----------------|:----------------|
| IEnumerator        | IEnumerator<T>              |                  |                 |
| IEnumerable        | IEnumerable<T>              | Enumeration only | Arrays          |
| ICollection        | ICollection<T>              | Countable        |                 |
|IDictionary / IList | IDictionary<K,V> / IList<T> | Rich features    |                 |


###IEnumerator / IEnumerable

####IEnumerator

#####The non-generic and generic interfaces

```cs
//Non generic
public interface IEnumerator
{
     bool MoveNext();
     object Current {get;}
     void Reset();
}

//Generic
public interface IEnumerator<T> : IEnumerator, IDisposable
{
     T Current {get;}
}
```

#####IDispose & IEnumerator<T>

The interface _IEnumerator_ inherits of the _IDispose_. So, when we use the _foreach_ loop, the method _Dispose()_ is called. So the resources are released after the _foreach_ loop:

```cs
foreach(var itemDisposable in myDisposableItemList)
{
     ....
}

//equivalent to:

using(var enumerator = myDisposableItemList.GetEnumerator())
{
     while(enumerator.MoveNext())
     {
          var item = enumerator.Current;
          ...
     }
}
```


####IEnumerable

#####The interface

IEnumerable like a IEnumerator provider

```cs
//Non-generic
public interface IEnumerable
{
     IEnumerator GetEnumerator();
}

//Generic
public interface IEnumerable<T> : IEnumerable
{
     IEnumrator<T> GetEnumerator();
}
```


#####1st example

```cs
string quote = "Hello world!";
IEnumerator enumeration = quote.GetEnumerator();

while(enumeration.MoveNext())
{
     Console.Write(((char)enumeration.Current));
}
```

And the output is :

```cmd
Hello world!
```

#####2nd example : using the _yield_ keyword to buid a non generic class implementing IEnumerable

```cs
public class MyCollection : IEnumerable
{
     int[] data = {1, 2, 3};
     
     public Enumerator GetEnumerator()
     {
          foreach(int n in data)
          {
               yield return n; 
          }
}
```


#####3rd example : using the _yield_ keyword to build a generic class implementing IEnumerable<T>

```cs
public class SquaredNumbers : IEnumerable<int>
{
     public readonly int[] list = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

     public IEnumerator<Int32> GetEnumerator()
     {
          foreach(var number in list)
          {    
               yield return (Int32)Math.Pow(number,2);   
          }
     }
}

var numbers = new SquaredNumbers();

for(int i = 0; i < 10; i++)
{
    Console.WriteLine("{0} : {1}", numbers.list[i], numbers[i]);  
}
```



###Arrays

####Get the generic interface

Arrays are not generic, so if you want to get the _generic interface_, you should do that:

```cs
int[] numbers = {1, 2, 3 };

var enumerator = ((GetEnumerator<int>)numbers).GetEnumerator();

```





#####ICollection
- Count
- IsSynchronized
- CopyTo

#####IList
inherit of ICollection and:
- indexOf
- Insert
- removeAt

#####The list
- ICollection<T>
- IList<T>
- IReadOnlyList<T>

#####Other collections
- LinkedList<T>
- Queue<T> (FIFO)
- Stack<T> (LIFO)
- HashSet<T>
- SortedSet<T>
- OrderedDictionary (is not a sorted! 

####Links

[When to use IEnumerable, ICollection, Ilist](http://www.claudiobernasconi.ch/2013/07/22/when-to-use-ienumerable-icollection-ilist-and-list/)
