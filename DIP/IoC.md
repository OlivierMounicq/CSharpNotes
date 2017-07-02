```cs
public interface ICreditCard
{
    string Charge();
}

public class Mastercard : ICreditCard
{
    public string Charge()
    {
        return "Your Mastercard has been charged";    
    }
}

public class Visa : ICreditCard
{
    public string Charge()
    {
        return "Your Visa card has been charged";    
    }
}

public class Shopper
{
    private ICreditCard creditCard;
    
    public Shopper(ICreditCard creditCard)
    {
        this.creditCard = creditCard;    
    }
    
    public string DoTransaction()
    {
        return this.creditCard.Charge();    
    }
}

public static class Main
{
    public static void Run()
    {
        var aVisaCard = new Visa();
        var shopper = new Shopper(aVisaCard);
    
        Console.WriteLine(shopper.DoTransaction());
    }
}

Main.Run();
```

[source](http://csharppad.com/gist/4e7644573ad5a06f38a2c433b2da2d0e)

```cs
public interface ICreditCard
{
    string Charge();
}

public class Mastercard : ICreditCard
{
    public string Charge()
    {
        return "Your Mastercard has been charged";    
    }
}

public class Visa : ICreditCard
{
    public string Charge()
    {
        return "Your Visa card has been charged";    
    }
}

public class Shopper
{
    private ICreditCard creditCard;
    
    public Shopper(ICreditCard creditCard)
    {
        this.creditCard = creditCard;    
    }
    
    public string DoTransaction()
    {
        return this.creditCard.Charge();    
    }
}

public class Resolver
{
    public ICreditCard ResolveCreditCard()
    {
        if(new Random().Next(2) == 1)
            return new Visa();
            
        return  new Mastercard();    
    }
}

public static class Main
{
    public static void Run()
    {
        Resolver resolver = new Resolver();
        var shopper = new Shopper(resolver.ResolveCreditCard());
    
        Console.WriteLine(shopper.DoTransaction());
    }
}

Main.Run();
```

[source](http://csharppad.com/gist/75c30d8144a17d20b96e508b2f7a2c55)

```cs
public interface ICreditCard
{
    string Charge();
}

public class Mastercard : ICreditCard
{
    public string Charge()
    {
        return "Your Mastercard has been charged";    
    }
}

public class Visa : ICreditCard
{
    public string Charge()
    {
        return "Your Visa card has been charged";    
    }
}

public class Shopper
{
    private ICreditCard creditCard;
    
    public Shopper(ICreditCard creditCard)
    {
        this.creditCard = creditCard;    
    }
    
    public string DoTransaction()
    {
        return this.creditCard.Charge();    
    }
}

public class Resolver
{
    private Dictionary<Type, Type> dependencyMap = new Dictionary<Type,Type>();
    
    
    public void Register<TFrom, TTo>()
    {
        this.dependencyMap.Add(typeof(TFrom), typeof(TTo));    
    }
    
    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }
    
    private object Resolve(Type typeToResolve)
    {
        Type resolvedType = null;
        
        try
        {
            resolvedType = this.dependencyMap[typeToResolve];    
        }
        catch(Exception ex)
        {
            throw new Exception(string.Format("Could not resolve type {0}", typeToResolve.FullName));
        }
        
        var firstConstructor = resolvedType.GetConstructors().First();
        var constructorParameters = firstConstructor.GetParameters();
        
        if(constructorParameters.Count() == 0)
        {
            return Activator.CreateInstance(resolvedType);    
        }
        
        IList<object> parameters = new List<object>();
        foreach(var parameterToResolve in constructorParameters)
        {
            parameters.Add(Resolve(parameterToResolve.ParameterType));    
        }
        
        return firstConstructor.Invoke(parameters.ToArray());
    }
}

public static class Main
{
    public static void Run()
    {
        Resolver resolver = new Resolver();
        resolver.Register<Shopper, Shopper>();
        resolver.Register<ICreditCard, Mastercard>();
        
        var shopper = resolver.Resolve<Shopper>();
    
        Console.WriteLine(shopper.DoTransaction());
    }
}

Main.Run();
```

[source](http://csharppad.com/gist/5d7435ca5877f70653c840114ae98781)
