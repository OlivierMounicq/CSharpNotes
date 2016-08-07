#LINQ

Langage INtegrated Query

###Definitions

- sequences : a collection
- element : an item in a sequence

###Lambda expression

A lambda expression is transformed by the compilater 
- either in a _delegate_
- or an expression _tree_ 


###Interfaces

We can query a _sequence_ if it implements one this interface:
- IEnumerable : for the _local_ query
- IQueryable : made to query the database by using either _Linq to Sql_ or _Entity Framework_

###Local query vs database query

Local query:
- _IEnumerable<T>_
- lambda expression => delegate

Database query (Linq2Sql or Entity Framework):
- IQueryable
- lambda expression => Expression


###AsEnumerable, AsQueryable

(IEnumerable<T>).AsQueryable => IQueryable<T>

or

(IQueryable<T>).AsEnumerable() => IEnumerable<T>

###Transform a db query to a local query to apply some operator

The db query will be transformed into a SQL query. And all C# operators/method have not equivalent in SQL. 
For instance, the regular expressio does not exist in SQl, so if you want to execute this db query, the CLR won't be able to transform the regex into a SQL statement. So in this case, we can mix the db query with a local query by this way by using the _AsEnumerable_ method:


```cs
var regex = new Regex("ey");

//An exception will throw : there is no regex SQL statement!
var dbQuery = dataContext.Persons.Where(p => regex.Matches(p.LastName).Count > 1);

//Apply the regular expression after to use AsEnumerable()
var dbQuery = dataContext.Persons.AsEnumerable().Where(p => regex.Matches(p.LastName).Count > 1);
```

###Delegate vs Expression tree

```cs
//local query
public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> predicate)

//database query
public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource,bool>> predicate)
```

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

=> _Many operators have no keyword in query syntax_.

The operator that we could use with _query syntax_:

|           |                   |           |
|-----------|-------------------|-----------|
| Where     | OrderBy           | GroupBy   |
|Select     | ThenBy            | Join      |
|SelectMany | OrderByDescending | GroupJoin |
|           | ThenByDescending  |           |


####Mix the fluent syntax with the query syntax

```cs
int qty = (from n in names where n.Contains("a") select n).Count();
```


###The extension method

The LINQ operators are the _extension methods_.

Without extension method, we should use this syntax by using the conventional static method:

```cs
IEnumerable<string> query = Enumerable.Select(
                              Enumerable.Select
                                Enumerable.OrderBy(
                                  Enumerable.Where(
                                    names, n => n.Contains("a")
                                  ), n => n.Length
                                ), n => n.ToUpper()
                              );
```



###Defered execution

The query is executed when the method _MoveNext()_ of the iterator is called.

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

###Inner mechanism

The CLR use the _Decorator_ design patter. Each pattern will be a decorator.

###Captured variable

####For loop
An error will throw, because the variable _i_ is capture by both loop.


```cs
try
{
    IEnumerable<char> query = "Hello world! I am a C# application";
    string vowels = "aeiou";

    int i = -10;

    for(i=0; i < vowels.Length; i++)
    {
        Console.WriteLine(i);
        query = query.Where(c => c != vowels[i]);
    }

    Console.WriteLine(i);

    foreach (char c in query)
    {
        Console.Write(c);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

```

Actually, the value of _i_ just before the second loop is equal at 5. So the program attempt to execute this statement 
```cs c != vowels[5] ```

####Foreach loop

The foreach loop does not run like the For loop.




###Links

[Understanding IEnumerable and IQueryable in C#](http://blog.falafel.com/understanding-ienumerable-iqueryable-c/)
