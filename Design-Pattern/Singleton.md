###Singleton

####Singleton without MultiThreading context

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



####Singleton in multithreading context

#####A classical method


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


##### Singleton with double-checked locking

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

NEVER DO THAT IN A MULTI-THREADING CONTEXT:

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
