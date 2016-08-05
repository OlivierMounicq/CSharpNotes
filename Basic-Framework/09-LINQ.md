#LINQ

Langage INtegrated Query

###Definitions

- sequences : a collectio
- element : an item in a sequence

###Lambda expression

A lambda expression is transformed by the compilater 
- either in a _delegate_
- or an expression _tree_


###Interfaces

We can query a _sequence_ if it implements one this interface:
- IEnumerable : for the _local_ query
- IQueryable : made to query the database by using either _Linq to Sql_ or _Entity Framework_


###Fluent syntax vs query expression

```cs
//Fluent syntax:
IEnumerable<string> query = names.Where(n => n.Contains("a").OrderedBy(n => n.Length).Select(n => n.ToUpper());

//Query expression
IEnumerable<string> query = from n in names
                            where n.Contains("a")
                            orderby n.Length
                            select n.ToUpper()
```

###The operator

The LINQ operators are the _extension methods_.

Without extension method, we should use this syntax by using the conventional static method:

```cs
IEnumerable<string> query = Enumerable.Select(
                              Enumerable.Select
                                Enumerable.OrderBy(
                                  Enumerable.Where(
                                    names, n => n.Contains("a)
                                  ), n => n.Length
                                ), n => n.ToUpper()
                              );
```



####Defered execution

The query is executed when the method _Next()_ of the iterator is called.

```cs
public class Scientist
{
    public string FirstName{get; set;}
    
    public string LastName{get; set;}
        
    public string ResearchField {get; set;}    
}

var scientists = new List<Scientist>();

scientists.Add(new Scientist(){FirstName = "Marie", LastName = "Curie", ResearchField = "Physics"});
scientists.Add(new Scientist(){FirstName = "Ada", LastName = "Lovelace", ResearchField = "Mathematics"});

//We build the query
IEnumerable<Scientist> physicians = scientists.Where(p => p.ResearchField == "Physics");

//We add physicians
scientists.Add(new Scientist(){FirstName = "Irene", LastName = "Joliot-Curie", ResearchField = "Physics"});
scientists.Add(new Scientist(){FirstName = "Cécile", LastName = "DeWitt-Morette", ResearchField = "Physics"});

foreach(var scientist in physicians)
{
    Console.WriteLine("{0} {1}", scientist.FirstName, scientist.LastName);
}

/**
The result : 
Marie Curie
Irene Joliot-Curie
Cécile DeWitt-Morette
**/

```


####Links

[Understanding IEnumerable and IQueryable in C#](http://blog.falafel.com/understanding-ienumerable-iqueryable-c/)
