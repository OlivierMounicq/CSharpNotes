###LINQ

IQueryable , IEnumerable


####Defered execution

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
