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
using System.Collections;
using System.Collections.Generic;

public class Numbers : IEnumerable<Int32>
{
    public readonly int[] list = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public IEnumerator<Int32> GetEnumerator()
    {
        foreach (var number in list)
        {
            yield return ((Int32)(Math.Pow(number, 2)));
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

var numbers = new Numbers();

foreach(var number in numbers)
{
     Console.WriteLine(number);
}

```

#####4th example: the easiest way by using the _yield_ keyword

```cs
public class List
{
     public static IEnumerable<Int32> GetList()
     {
          yield return 1;
          yield return 2;
          yield return 3;
          yield return 4;
          yield return 5;
     }
}

foreach(var n in List.GetList())
{
     Console.WriteLine(n);
}
```

#####5th example : implement the IEnumerator interface

```cs
using System.Collections;
using System.Collections.Generic;
public class Numbers : IEnumerable
{
    int[] data = {1, 2, 3, 4, 5};
    
    public IEnumerator GetEnumerator()
    {
        return new Enumerator(this);    
    }
    
    
    class Enumerator : IEnumerator
    {
        Numbers numbers;
        int currentIdx = -1;
        
        public Enumerator(Numbers pNumbers)
        {
            this.numbers = pNumbers;   
        }
        
        public object Current
        {
            get
            {
                if(currentIdx == -1)
                {
                    throw new InvalidOperationException("Enumeration has not started");
                }
                
                if(currentIdx == numbers.data.Length)
                {
                    throw new InvalidOperationException("Invalid index!");
                }
                
                return this.numbers.data[currentIdx];
            }
        }
            
        public bool MoveNext()
        {
            if(currentIdx >= numbers.data.Length - 1)
            {
                return false;
            }
                
            return ++currentIdx < numbers.data.Length;
        }
            
        public void Reset()
        {
            currentIdx = -1;
        }
    }
}
var numberLst = new Numbers();
foreach(var n in numberLst)
{
    Console.WriteLine(n);   
}

//Output:
1
2
3
4
5
```

#####6th example : implement the generic IEnumerator

```cs
using System.Collections;
using System.Collections.Generic;

public class Numbers : IEnumerable<Int32>
{
    int[] data = {1, 2, 3, 4, 5};
    
    public IEnumerator<Int32> GetEnumerator(){ return new Enumerator(this); }
    IEnumerator IEnumerable.GetEnumerator(){ return new Enumerator(this); }
    
    class Enumerator : IEnumerator<Int32>
    {
        Numbers numbers;
        int currentIdx = -1;
        
        public Enumerator(Numbers pNumbers)
        {
            this.numbers = pNumbers;   
        }
        
        public int Current => this.numbers.data[currentIdx];
        
        object IEnumerator.Current => this.Current;
        
        public bool MoveNext() => ++currentIdx < this.numbers.data.Length;
        
        public void Reset() => currentIdx = -1;
        
        void IDisposable.Dispose(){ }
    }
}

var numberLst = new Numbers();
foreach(var n in numberLst)
{
    Console.WriteLine(n);   
}
```

###ICollection & ICollection<T>

####Non generic interface

```cs
public interface ICollection : IEnumerable
{
     int Count { get; }
     bool IsSynchronized{ get; }
     object SyncRoot{ get; }
     
     void CopyTo(Array array, int index);
}
```

#### Generic interface

```cs 
public interface ICollection<T> : IEnumerable<T>, IEnumerable
{
     int Count { get; }
     
     bool Contains(T item);
     void CopyTo(T[] array, int arrayIndex);
     bool IsReadOnly { get; }

     void Add(T item);
     bool Remove(T item);
     void Clear();
}
```

####The features

The features implemented into a collection is that the set of item is accountable.

####Difference between the non-generic and generic interfaces

- no _Add_ method in the non-generic method
- no _Remove_ method in the non-generic method
- no synchronization method in the generic method


###IList & IList<T>

####Non-generic interface IList

```cs
public interface IList : ICollection, IEnumerable
{
     object this [int index] {get; set;}
     bool IsFixedSize { get; }
     bool IsReadOnly  { get; }
     int  Add      (object value);
     void Clear();
     bool Contains (object value);
     int  IndexOf  (object value);
     void Insert   (int index, object value);
     void Remove   (object value);
     void RemoveAt (int index);
}
```

####The generic interface IList<T>

```cs
public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
{
     T this [int index] { get; set; }
     int IndexOf (T item);
     void Insert (int index, T item);
     void RemoveAt (int index);
}
```

####Difference between IList & IList<T>

- the interface IList must add the missing feature in ICollection interface : Insert & Remove
- the method _Add_ returns an integer 


###IReadOnlyList<T>

This interface has been added in the framework __4.5__. It's like the IList<T> without the method to add and remove the items.

```cs
public interface IReadOnlyList<out T> : IEnumerable<T>, IEnumerable
{
     int Count { get; }
     T this[int index] { get; }
}
```

__Remark : __ It will be logical if the IList will inherit from IReadOnlyList. But the cost to modify the inheritance schema will be to much. Everybody will have to recompile its solution.

###Arrays

####Get the generic interface

Arrays are not generic, so if you want to get the _generic interface_, you should do that:

```cs
int[] numbers = {1, 2, 3 };

var enumerator = ((GetEnumerator<int>)numbers).GetEnumerator();

```

####The Array class

- Not resizable
- The CLR assigns to the array a contiguous space in memory
- Implement IList<T>
- When the method _Call_ and _Remove_ of IList<T>  are called, an error is thrown
- Only way to resize the array: using the static method _Resize_
- _Resize_ method : creates another array and copies the former into the new. And the references to the array elsewhere in the program will still point to the original version
- Array is a class => always reference type regardless of the array's element type
- Clone method : the new array contains the references of the original array (only the references are copied), it's _shallow array_
- Deep copy : you must loop through the array and clonde each element
- The higher base class: Array and not object[] (because, for instance, an array with the value type like int[] cannot inherit of object[])
- The aray are covariant
- Clear() method does not affect the size of the array
- _O(n)_


####Equality

```cs
object[] a1 = { "string", 123, true };
object[] a2 = { "string", 123, true };

Console.WriteLine(a1 == a2); //False
Console.WriteLine(a1.Equals(a2)); //False

IStructuralEquatable se1 = a1;
Console.WriteLine(se1.Equals(a2, StructuralComparisons.StructuralEqualityComparer)); //True
```

####Clone : shallow copy

```cs
StringBuilder[] builder2 = builders;
StringBuilder[] shallowClone = (StringBuilder[]) builders.clone();
```

####Construction and indexing

#####Instantiate a new array

```cs
int[] myArray = {1, 2, 3};
int[] myArray = new int[5];

int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9} };
````

#####Instantiate a new array by CreateInstance

=> This method also initialiaze the elements

```cs
Array.CreateInstance(typeof(string), 5);
````







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
