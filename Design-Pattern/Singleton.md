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

#####


#####
