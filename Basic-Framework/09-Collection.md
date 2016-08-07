###The namespace

| namespace                       |The collections                           |
|:--------------------------------|:-----------------------------------------|
| System.Collections              | Non generic collection                   |
| System.Collections.Specialized  | Strongly typed non-generic collection    |
| System.Collections.Generic      | Generic collection                       |
| System.Collections.ObjectModel  | Proxies and bases for custom collections |
| System.Collections.Concurrent   | Thread-safe collections                  |


###The interfaces

| Non Generic        | Generic                     | Features         |
|:-------------------|:----------------------------|:-----------------|
| IEnumerator        | IEnumerator<T>              |                  |
| IEnumerable        | IEnumerable<T>              | Enumeration only |
| ICollection        | ICollection<T>              | Countable        |
|IDictionary / IList | IDictionary<K,V> / IList<T> | Rich features    |


####IEnumerator


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


#####An example

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
