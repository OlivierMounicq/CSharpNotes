### Structure

#### 1. Properties
- struct can contain data members and function members
- struct are _value type_
- struct do not require heap allocation
- struct inherites implicitly from System.Value
- struct can implement interfaces
- struct cannot inherite
- struct cannot have parameterless constructor
- struct cannot have virtual/protected members (inheritance is forbidden)
- struct cannot have fields initializers
- struct cannot have finalizer
- struct cannot be null (it's a value type)
- when you define a struct constructor, you have to explicitly assign every field

The simple value type (int, double, bool..) provided by the C# are in fact the structure.

#### 2. Differences between Struct and Class
- classes require heap allocation and struct do not require heap allocation
- class has a referential identity, not the struct


#### 3. When should I use the structure?
We can use struture for:
- the data with a light memory footprint 

#### 4. Examples
```cs
    public struct Person : IComparable
    {
        private string lastName;

        internal string firstName;

        //The constructor must set explicitly all fields
        public Person(string pFirstName, string pLastName)
        {
            this.firstName = pFirstName;
            this.lastName = pLastName;
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}";
        }

        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;

            if(!(obj is Person))
            {
                throw new ArgumentException("The argument type have to be a Person type");
            }

            if(((Person)obj).lastName == lastName && ((Person)obj).firstName == firstName)
            {
                return 1;
            }

            return 0;
        }
    }    
    static void Main(string[] args)
    {
        Person albertEinstein = new Person("Albert","Einstein");
        Console.WriteLine(albertEinstein.ToString());

        Person richardFeynmann = new Person("Richard", "Feynmann");
        Console.WriteLine(richardFeynmann.ToString());

        Console.WriteLine($"Albert Einstein == Richard Feynmann ? {albertEinstein.CompareTo(richardFeynmann)}");
        Console.ReadKey();
    }    
        
```

#### 5. Use cases
##### 5.1 Initialization

```cs
public struct Person
{
    public int Age;
    public string LastName;
    public string FirstName;
}
...
Person person;
Console.WriteLine(person.Age);      //0
Console.WriteLine(person.LastName); //string.Empty

```

##### 5.2 Nullable structure
You can set a structure as null by using Nullable<T>.

```cs
public struct Person
{
    public int Age;
    public string LastName;
    public string FirstName;
}
...
Nullable<Person> person;
Console.WriteLine(person.HasValue);      //false


```

