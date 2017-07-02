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
