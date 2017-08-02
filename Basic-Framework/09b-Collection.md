

## Introducing

### Lists / Dictionnary

- list / array : index-based collection
- index-based lists often use less memory 
- dictionary implements a hash table
- performance : 
  - access to an element with a dictionary by using a key : fast
  - access to an element with a lst by using an index : very fast
- Sets : HashSet&lt;T&gt; / ISet&lt;T&gt;
  - the focus is not on direct access to an element but treating the collection as a single group
  - the goal is to create new collection by combinig otrher collection by using union / intersection ... operations
  - they use also a hashtable like the dictionary but there is no lookup with keys 
  - to get a particular element, you have to enumerate the set.
  

### Basics collection operations

- Reading
  - Look up an element (by index or key)
  - Enumerate the elements  
- Writing
  - Add an element
  - Remove an element
  - LIST : insert an element  
  
  
- Enumerating a collection
  - List : items enumerated in index order
  - Dictionary : don't rely on the order
  - All collections support the enumeration

- Looking up items:
  - many collection
  - Not : Sets, Linked lists, Stacks, Queues
  
  
## List

### Generic Collection / Object Model Collection

- List&lt;T&gt; 
  - namespace : System.Collections.Generic
  - Extensive API
  - like array but with adding/removing elements
- ReadOnlyCollection<T>
  - namespace : System.Collections.ObjectModel
  - Read-only wrapper for lists
- Collection&lt;T&gt; 
  - namespace : System.Collections.ObjectModel
  - Allow lists to be customized
- ObservableCollection&lt;T&gt;
  - namespace : System.Collections.ObjectModel
  - List with change notification

###  List&lt;T&gt; under the hood

- List&lt;T&gt; encapsulates Array[T] : it uses an internal array
- To be able to insert element, the list object creates an array bigger than what it needs to hold its elements. 
- The List&lt;T&gt; has two properties:
  - List&lt;T&gt;.Count (from the ICollection&lt;T&gt; interface) : the element quantity in the list
  - List&lt;T&gt;.Capacity : the global capacity of the List&lt;T&gt; : this property is specific to the List&lt;T&gt; and it does not form part of any interface.
- When the element quantity will overload the capacity of the internal array, the List&lt;T&gt; will create a _new_ internal array by doubling the former size.  
- If you know roughly the element quantity, you can instantiante the List&lt;T&gt; by defining the _capacity_ : ```var lst = new List<string>(capacity)```
- Remove an element:
  - ```RemoveAt``` : you rearrange the array to compact it : removing an element near the beginning of the list is expensive  
  - ```Remove(object)``` : this will take longer than ```RemoveAt(idx)```, must find index by searching elements and then apply ```RemoveAt(idx)``` method
   
 ```cs
public class Foo
{
    public static void Run()
    {
        int former = 0;
        var list = new List<Int64>();
        Console.WriteLine(list.Capacity);
        
        for(Int64 cpt = 0; cpt < Int64.MaxValue; cpt++)
        {
            list.Add(cpt);
            
            if(former != list.Capacity)
            {
                former = list.Capacity; 
                Console.WriteLine(list.Capacity);
            }
        }        
    }
}

Foo.Run();  
```
  
And the result is :  
0,4,8,16,32,64,128,256,512,1024,2048,4096,8192,16384,32768,65536,131072,262144,524288,1048576,2097152,4194304,8388608,16777216

  
  
  
  
