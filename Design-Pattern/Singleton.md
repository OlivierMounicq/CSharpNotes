# Singleton

### Singleton vs static

- singleton can extend a base class
- singleton can implement an interface
- singleton can be initialized lazily or asynchronously while a static class is generally initialized when it is first loaded
- singleton is OO pattern (inhertitance, polymorphisme)
- singleton can be treated as a normal object
 
### Singleton without MultiThreading context

```cs
public class Singleton
{
  private static Singleton instance;
  
  //The private constructor
  private Singleton()
  {
  }

  public static GetInstance()
  {
    if(instance == null)
    {
      instance = new Singleton();
    }
    
    return instance;
  }
}
```



### Singleton in multithreading context

#### A classical method


```cs
public class Singleton
{
  private static Singleton instance;
  private readonly object aLock = new object();
  
  //The private constructor
  private Singleton()
  {
  }

  public static GetInstance()
  {
    lock(myLock)
    {
      if(instance == null)
      {
        instance = new Singleton();
      }    
    }
    return instance;
  }
}
```


#### Singleton with double-checked locking

With the previous Sigleton pattern, after to instanciate the class Singleton, each time we lock to check if the class have been already created. To avoid to lose time to check, we decide to make a double check. 

```cs
public class Singleton
{
  private static Singleton instance;
  private readonly object aLock = new object();
  
  //The private constructor
  private Singleton()
  {
  }

  public static GetInstance()
  {
    if(instance == null)
    {
      lock(myLock)
      {
        if(instance == null)
        {
          instance = new Singleton();
        }    
      }    
    }

    return instance;
  }
}
```
And we have to add a new check and not just inverse the lock statement with the check statement. Because if we inverse the lock with the checking, two threads may access to the code in order instantiate the class as we could see: 

__NEVER DO THAT IN A MULTI-THREADING CONTEXT:__

```cs
public class Singleton
{
  private static Singleton instance;
  private readonly object aLock = new object();
  
  //The private constructor
  private Singleton()
  {
  }

  public static GetInstance()
  {
    if(instance == null)
    {
      //Beware: two threads may access to this code aera. Each one will instantiate the class!!!!
      lock(myLock)
      {
        instance = new Instance()
      }    
    }

    return instance;
  }
}
```

see [Double-checked locking](https://en.wikipedia.org/wiki/Double-checked_locking)

### Drawbacks

Now the singleton is not considered as a design pattern but as an anti-pattern.  
Among these reasons, they are:
 - the singleton is a _global_ variable : it means that any statement in the program could update the value
 - we cannot test the singleton by using the dependency injection: actually we cannot inject a mock object because the constructor is private.
