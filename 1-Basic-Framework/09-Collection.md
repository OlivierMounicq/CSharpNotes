### The namespace

| namespace                       |The collections                           |
|:--------------------------------|:-----------------------------------------|
| System.Collections              | Non generic collection                   |
| System.Collections.Specialized  | Strongly typed non-generic collection    |
| System.Collections.Generic      | Generic collection                       |
| System.Collections.ObjectModel  | Proxies and bases for custom collections |
| System.Collections.Concurrent   | Thread-safe collections                  |


### The interfaces

| Non Generic        | Generic                     | Features         | Implemented in  |
|:-------------------|:----------------------------|:-----------------|:----------------|
| IEnumerator        | IEnumerator&lt;T&gt;              |                  |                 |
| IEnumerable        | IEnumerable&lt;T&gt;              | Enumeration only | Arrays          |
| ICollection        | ICollection&lt;T&gt;              | Countable        |                 |
|IDictionary / IList | IDictionary&lt;K,V&gt; / IList&lt;T&gt; | Rich features    |                 |


### IEnumerator / IEnumerable

#### IEnumerator

##### The non-generic and generic interfaces

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

##### IDispose & IEnumerator&lt;T&gt;

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


#### IEnumerable

##### The interface

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
     IEnumerator<T> GetEnumerator();
}
```


##### 1st example

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

##### 2nd example : using the _yield_ keyword to buid a non generic class implementing IEnumerable

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


##### 3rd example : using the _yield_ keyword to build a generic class implementing IEnumerable&lt;T&gt;

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

##### 4th example: the easiest way by using the _yield_ keyword

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

##### 5th example : implement the IEnumerator interface

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

##### 6th example : implement the generic IEnumerator

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

### ICollection & ICollection&lt;T&gt;

#### Non generic interface

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

#### The features

The features implemented into a collection is that the set of item is accountable.

#### Difference between the non-generic and generic interfaces

- no _Add_ method in the non-generic method
- no _Remove_ method in the non-generic method
- no synchronization method in the generic method


### IList & IList&lt;T&gt;

#### Non-generic interface IList

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

#### The generic interface IList&lt;T&gt;

```cs
public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
{
     T this [int index] { get; set; }
     int IndexOf (T item);
     void Insert (int index, T item);
     void RemoveAt (int index);
}
```

#### Difference between IList & IList&lt;T&gt;

- the interface IList must add the missing feature in ICollection interface : Insert & Remove
- the method _Add_ returns an integer 


### IReadOnlyList&lt;T&gt;

This interface has been added in the framework __4.5__. It's like the IList&lt;T&gt; without the method to add and remove the items.

```cs
public interface IReadOnlyList<out T> : IEnumerable<T>, IEnumerable
{
     int Count { get; }
     T this[int index] { get; }
}
```

__Remark : __ It will be logical if the IList will inherit from IReadOnlyList. But the cost to modify the inheritance schema will be to much. Everybody will have to recompile its solution.

### Arrays

#### Get the generic interface

Arrays are not generic, so if you want to get the _generic interface_, you should do that:

```cs
int[] numbers = {1, 2, 3 };

var enumerator = ((GetEnumerator<int>)numbers).GetEnumerator();

```

#### The Array class

- Not resizable
- The CLR assigns to the array a contiguous space in memory
- Implement IList&lt;T&gt;
- When the method _Call_ and _Remove_ of IList&lt;T&gt;  are called, an error is thrown
- Only way to resize the array: using the static method _Resize_
- _Resize_ method : creates another array and copies the former into the new. And the references to the array elsewhere in the program will still point to the original version
- Array is a class =&gt; always reference type regardless of the array's element type
- Clone method : the new array contains the references of the original array (only the references are copied), it's _shallow array_
- Deep copy : you must loop through the array and clonde each element
- The higher base class: Array and not object[] (because, for instance, an array with the value type like int[] cannot inherit of object[])
- The aray are covariant
- Clear() method does not affect the size of the array
- _O(n)_


#### Equality

```cs
object[] a1 = { "string", 123, true };
object[] a2 = { "string", 123, true };

Console.WriteLine(a1 == a2); //False
Console.WriteLine(a1.Equals(a2)); //False

IStructuralEquatable se1 = a1;
Console.WriteLine(se1.Equals(a2, StructuralComparisons.StructuralEqualityComparer)); //True
```

#### Clone : shallow copy

```cs
StringBuilder[] builder2 = builders;
StringBuilder[] shallowClone = (StringBuilder[]) builders.clone();
```

#### Construction and indexing

##### Instantiate a new array

```cs
int[] myArray = {1, 2, 3};
int[] myArray = new int[5];

int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9} };
````

#####Instantiate a new array by CreateInstance

=&gt; This method also initialiaze the elements

```cs
string[] myArray = Array.CreateInstance(typeof(string), 5);
````

#####Set values by using __SetValue__ and get value by using __GetValue__

```cs
Array a = Array.CreateInstance(typeof(int), 5);
a.SetValue(10,0); //a[0] = 10;

int[,] matrix = new int[5,5];
matrix.SetValue(1,0,0);

int val = matrix.GetValue(0,0);
```

##### Copying

- intance methods
     * Clone  : return a new shallow-copied instance
     * CopyTo : copies a contiguous subset of the array
- static methods :
     * Copy : copies a contiguous of the array
     * ConstrainedCopy : atomic operation. All elements have to be copied otherwise the copy will be rollbacked

### List&lt;T&gt; and ArrayList

- ArrayList and List&lt;T&gt; are dynamically sized
- ArrayList is non generic and implements the IList interface
- List&lt;T&gt; is generic and implements the IList&lt;T&gt; interface
- List&lt;T&gt; and ArrayList use internally the arrays of objects
- Appending elements is efficiency
- Inserting elements could be slow
- Best searching method : use the _BinarySearch_ on the sorted list otherwise each element must checked
- List&lt;T&gt; is up to several times faster than ArrayList if T is a value type because List&lt;T&gt; avoids the overhead of boxing and unboxing elements
- If the element type is _value_, you may choose a List&lt;T&gt; (better performance)
- Choose ArrayList if you want to use reflection. The reflection is easier in with non-generic type
- _O(n)_

#### ArrayList

##### Casting

```cs
ArrayList al = new ArrayList();
al.Add("hello");

//The CLR will compile, but an exception will be thrown during the runtime
int test = (int)al[0];
```

##### Cast an ArrayList into List&lt;T&gt;

```cs
ArrayList al = new ArrayList();
al.AddRange(new[] {1, 2, 3}};
List<int> list = al.Cast<int>().ToList();
```

### LinkedList&lt;T&gt;

- generic doubly linked list
- inserting is efficiency
- Implements IEnumerable, IEnumerable&lt;T&gt;, ICollection, ICollection&lt;T&gt;
- No access to element by index
- The LinkedList&lt;T&gt; has internal fields to keep track of the number of elements, the head and the tail of the list

### Queue and Queue&lt;T&gt;
- FIFO
- Enqueue / Dequeue methods
- Peek : return the first element at head of the queue without to remove it
- Cannot access directly to an item by index
- Does not implement IList and ILIst&lt;T&gt; (no direct access is mandatory)
- They are implemented internally using array
- The queues maintain indexes to access directly to the head and tail of the collection 
- Enqueuing and Dequeuing are quick operations


### Stack & Stack&lt;T&gt;
- LIFO
- Push : add an item
- Pop : retrieve and remove an item
- Peek : retrieve without removing the item

### BitArray
- Dynamically sized collection of compacted bool values
- More memory-efficient than a simple array of bool or a generic list of bool
- Uses one bit for each values (otherwise the bool type uses one byte)


### HashSet&lt;T&gt; ans SortedSet&lt;T&gt;
- Contains method execute quickly using a hash-based lookup
- Do not store duplicate elements
- Silently ignore the requests to add duplicates
- Cannot access element by index
- SortedSet&lt;T&gt; keeps the elements in order
- HashSet&lt;T&gt; is implemented with a hashtable that stores just keys
- SortedSet&lt;T&gt; is implemented with a red/black tree
- Both class implements ICollection&lt;T&gt;


### Dictionaries

- Unsorted dictionaries
     * Dictionary&lt;K,V&gt; : hashtable / _O(1)_
     * Hashtable : hashtable / _O(1)_ / non-generic dictionary
     * ListDictionary : linked list / _O(n)_
     * HybridDictionary
     * OrderedDictionary : hashtable + array / _O(1)_
- Sorted dictionnaries:
     * SortedDictionary&lt;K,V&gt; : red/black tree / _O(log n)_
     * SortedList&lt;K,V&gt; : 2 arrays / _O(log n)_
     * SortedList : 2 arrays / _O(log n)_

#### Interface IDictionary&lt;TKey, TValue&gt;
- The interface extends the ICollection&lt;T&gt;
- Duplicate keys are forbidden (otherwise exception is thrown)
- If the key does not exit, an error is thrown

#### Interface IDictionary

- Request a nonexistent key via the indexer returns null (not threw exception)

#### Dictionary&lt;K,V&gt;

- The underlying hashtable works by converting each element's key into an integer hashcode and the applying an algorithm to convert the hashcode into a hash key
- By default, the methods _object.Equal_ and _GetHashCode_ are used to retrieve a value from its key
- We could override the way to perform the equality : ```new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) ```
- Increase performance by defining the expected size of the collection during the instantiation (via the constructor) to avoid the operation to resize the object.

#### Dictionary&lt;K,V&gt; where K is a custom type

When the key type is a cutom type, you must override the method _GetHashCode_ and _Equals_.

By example:

```cs
public class Person
{
     public string FirstName { get; set; }
     
     public string LastName { get; set; }

     public Person(string firstName, string lastName)
     {
          this.FirstName = firstName;
          this.LastName = lastName;
     }
}

public class Test
{
     public static void Foo()
     {
          var dict = new Dictionary<Person, Int32>();
          
          var einstein1 = new Person("Albert","Einstein");
          var einstein2 = new Person("Albert","Einstein");
          
          dict.Add(einstein1, 1);
          dict.Add(einstein2, 2); //no error!
          
          Console.WriteLine("Key quantity : {0}", dict.Keys.Count());
     }
}

Test.Foo();
```
[@csharppad.com](http://csharppad.com/gist/367cd9ba5f75f4e38558ba9c5f188472)

So this below code won't raise error because the objects _einstein1_ and _einstein2_ have not the same memory reference, so the default method _Equals_ returns false. And thus, we can insert two key with the same value.

Consequently, if want to get a value, we must use the right reference:

```cs
public class Person
{
     public string FirstName { get; set; }
     
     public string LastName { get; set; }

     public Person(string firstName, string lastName)
     {
          this.FirstName = firstName;
          this.LastName = lastName;
     }
}

public class Test
{
     public static void Foo()
     {
          var dict = new Dictionary<Person, Int32>();
          
          var einstein1 = new Person("Albert","Einstein");
          var einstein2 = new Person("Albert","Einstein");
          
          dict.Add(einstein1, 1);
          dict.Add(einstein2, 2); //no error!
          
          Console.WriteLine("Key quantity : {0}", dict.Keys.Count());
          
          Console.WriteLine("The value : {0}", dict[einstein2]); //No problem
          
          Console.WriteLine("The value : {0}", dict[new Person("Albert","Einstein")]); //Problem : the key is not in the dictionary
          
     }
}

Test.Foo();
```
[@csharppad.com](http://csharppad.com/gist/2119414361bdd1dc07f4f5a63c9d4fdc)

So to avoid those problem, we have to override the methods _EqualsTo_ and _GetHashCode_:


```cs
public class Person
{
     public string FirstName { get; set; }
     
     public string LastName { get; set; }

     public Person(string firstName, string lastName)
     {
          this.FirstName = firstName;
          this.LastName = lastName;
     }
     
     public override bool Equals(object obj)
     {
          return ((Person)obj).FirstName = this.FirstName && ((Person)obj).LastName == this.LastName;
     }
     
     public override int GetHashCode()
     {
          return this.FirstName.GetHashCode() ^ this.LastName.GetHashCode();
     }
}

public class Test
{
     public static void Foo()
     {
          var dict = new Dictionary<Person, Int32>();
          
          var einstein1 = new Person("Albert","Einstein");
          var einstein2 = new Person("Albert","Einstein");
          
          dict.Add(einstein1, 1);
          dict.Add(einstein2, 2); //ERROR!
          
          Console.WriteLine("Key quantity : {0}", dict.Keys.Count());
     }
}

Test.Foo();
```
[@csharppad.com](http://csharppad.com/gist/f000870dea2a66f8732d887d19190bc2)

__Remark__ : baeware when you override the _GetHashCode_ methods because you could face to performance problem. Choose carefully the right method.

And finally, the dictionary uses the inner values in the key objects and not the reference:


```cs
public class Person
{
     public string FirstName { get; set; }
     
     public string LastName { get; set; }

     public Person(string firstName, string lastName)
     {
          this.FirstName = firstName;
          this.LastName = lastName;
     }
     
     public override bool Equals(object obj)
     {
          return ((Person)obj).FirstName = this.FirstName && ((Person)obj).LastName == this.LastName;
     }
     
     public override int GetHashCode()
     {
          return this.FirstName.GetHashCode() ^ this.LastName.GetHashCode();
     }
}

public class Test
{
     public static void Foo()
     {
          var dict = new Dictionary<Person, Int32>();
          
          var einstein1 = new Person("Albert","Einstein");
          dict.Add(einstein1, 1);
          
          einstein1.FirstName = "Richard";
          einstein1.LastName = "Feynmann";
          dic.Add(einstein1, 2); //No error
          
          Console.WriteLine("Key quantity : {0}", dict.Keys.Count());
     }
}

Test.Foo();
```

[@csharppad.com](http://csharppad.com/gist/5ea35d14b9102298920b446d7abd5d25)

#### OrderedDictionary

- An OrderedDictionary is not a __sorted__ dictionary
- Non generic dictionary that maintains elements in the same order that they were added
- A combination of Hashtable and an ArrayList
- Because this class inherits ArrayList, we can access to the method _RemoveAt_
- No generic version

#### ListDictionary
- Singly linked list to store the underlying data
- Preserves the entry order of the items
- Extremely slow with large list
- Very efficiency with very small list (fewer than 10 items)
- Non generic class
- No generic version

#### HybridDictionary
- For a small set of data, the underlying class is ListDictionary
- For the large set of data, the underlying class will a HashTable
- Low memory when the dictionary is small
- Too much overhead to convert ListDictionary to HashTable, so the performances are not good
- Non generic class
- No generic version

#### SortedDictionary&lt;TKey,TValue&gt;
- Uses a red/black tree
- Must faster than SortedList&lt;TKey, TValue&gt;
- The duplicate keys are not allowed

#### sortedList&lt;T&gt;
- Implements internally with an ordered array pair providing fast via binary-chop search
- Poor insertion performance
- You can go directly to the _nth_ element in the sorting sequence
- The duplicate keys are not allowed


### CUSTOMIZABLE COLLECTIONS AND PROXIES

Namespace : System.Collections.ObjectModel

- Wrapper/proxy implemented IList&lt;T&gt;
- Each Add, Remove and Clear operation is routed via a virtual method

#### Collection&lt;T&gt;

- a constructor accepting an existing IList&lt;T&gt;, the supplied list is proxied and not copied, so it means that each modificaton in the Collection&lt;T&gt; will modify the list too
- Generic class 
- The methods are :
     * protected virtual void ClearItems()
     * protected virtual void InsertItem(int index, T item)
     * protected virtual void RemoveItem(int index)
     * protected virtual void SetItem(int index, T item)
     * protected IList&lt;T&gt; Items { get; }

#### CollectionBase

- non generic version of Collection&lt;T&gt;
- the methods are:
     * OnInsert
     * OnInsertComplete
     * OnSet
     * OnSetComplete
     * OnRemove
     * OnRemoveComplete
     * OnClear
     * OnClearComplete

#### KeyedCollection&lt;TKey, TItem&gt;

- Subclass of Collection&lt;TItem&gt;
- Combines linear list with a hashtable
- Does not implement the interface IDictionary
- Does not support the concept of the key/value pair
- Like Collection&lt;Item&gt; with a fast lookup key


### Complexity

| Complexity | Class                       |
|:-----------|:----------------------------|
| _O(1)_     | ```Dictionary<K,>```        |
|            | ```HashTable ```            |
|            | ``ÒrderedDictionary ```     |
| _O(log n)_ | ```SortedDictionary<K,V> ```|
|            | ```SortedList ```           |
|            | ```SortedList<K,V> ```      |
| _O(n)_     | ```List ```                 |
|            | ```List<T> ```              |
|            | ```ListDictionary ```       |









-----------------




#####IList
inherit of ICollection and:
- indexOf
- Insert
- removeAt

#####The list
- ICollection&lt;T&gt;
- IList&lt;T&gt;
- IReadOnlyList&lt;T&gt;

#####Other collections
- LinkedList&lt;T&gt;
- Queue&lt;T&gt; (FIFO)
- Stack&lt;T&gt; (LIFO)
- HashSet&lt;T&gt;
- SortedSet&lt;T&gt;
- OrderedDictionary (is not a sorted! 

####Links

[When to use IEnumerable, ICollection, Ilist](http://www.claudiobernasconi.ch/2013/07/22/when-to-use-ienumerable-icollection-ilist-and-list/)
