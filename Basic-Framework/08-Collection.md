####IEnumerable / IEnumerator

```cs
public interface IEnumerator
{
     bool MoveNext();
     object Current {get;}
     void Reset();
}

public interface IEnumerable
{
     IEnumerator GetEnumerator();
}

public interface IEnumerator<T> : IEnumerator, IDisposable
{
     T Current {get;}
}

public interface IEnumerable<T> : IEnumerable
{
     IEnumrator<T> GetEnumerator();
}
```

IEnumerable <- ICollection <- IDictionary
IEnumerable <- ICollection <- IList

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
