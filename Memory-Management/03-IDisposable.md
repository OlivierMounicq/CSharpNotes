#The interface IDisposable


##1. Unmanaged/managed resources 

###1.1 The resource types

There are three different kinds of resources:
- full managed resources
- an object with unmanaged and manages resources
- full managed resource object


###1.2 Why to use the method Dispose ?

Actually the managed resources are totally managed by the garbage collector (the allocation and the deallocation). So when there is no reference pointing to object, the memory used by the object will be free by the garbage collector.
But if the object owns unmanaged resource (like file, database connection, ...), you must delete the reference of unmanaged resource otherwise the object won't be deallocated.

To deallocate explicitly the unmanaged resource, you have to create and call a _Dispose_ method (of the _IDisposable_ interface) to explain the deallocation of the unmanaged resource.


##2. Dispose vs Finalize

###2.1 Dispose

So the _Dispose_ method is used to explain how to deallocate the unmanaged resource.


###2.2 Finalize & destructor

####2.2.1 Finalize and destructor.

You cannot create a _Finalize_ method which is called by the Garbage Collector. The only way is to to create a destructor. All the code inside the destructor will be copied in to the _Finalize_ method by the CLR.

####2.2.2 Why to use the finalizer?

If the code does not call the Dispose (the developer has forgot to call the _Dispose_ method), the object won't be released. To avoid this problem, you can to call the _Dispose_ method from the destructor (consequently, from the finalizer)

###2.3 The worklow

So there are two ways to call the _Dispose_ method:
- the developer calls in his/her code the _Dispose_ method
- the finalizer/destructor calls the the _Dispose_ method.

But if the developer has called the _Disposible_ method in his/her code, it's useless to call the _Dispose_ method from the finalizer. When the method Dispose has been called, you must suppress the object in the finalize queue to gain performance.

###3.The best practices / The pattern


####3.1.When should you use and implement the IDisposable interface and the finalizer?




####3.2. How to use the IDisposable interface?

```cs
public class BaseClass : IDisposable
{
  private bool disposed = false;
  
  public Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if(!disposed)
    {
      if(disposing)
      {
        //TODO : clean up the managed resource
        //Consequently this code cannot be called by the destructor/finalizer
      }
    
      //TODO : clean up the unmanaged resource
    }
    
    disposed = true;
  }

  ~BaseClass()
  {
    Dispose(false);
  }

}

```

Only the public method will be call either by the calling method or by the finalizer. If the public _Dispose_ method is called, so you can suppress the object from the finalize queue.

The protected method will be call either by the method of the object or by the inherited class.



####Some url

[IDisposable for Dummies #1 – Why? What?](http://blog.ilab8.com/2012/04/26/idisposable-for-dummies-1-why/)

[IDisposable for dummies #2 – A guide about ‘how to implement it’](http://blog.ilab8.com/2012/04/29/idisposable-for-dummies-2-how/)

[How to Implement IDisposable and Finalizers: 3 Easy Rules](http://blog.stephencleary.com/2009/08/how-to-implement-idisposable-and.html)

[IDisposable and using in C#](https://coding.abel.nu/2011/12/idisposable-and-using-in-c/)

[Implementing IDisposable and the Dispose pattern](http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P)

[Back to basics : Dispose vs finalize](http://www.c-sharpcorner.com/UploadFile/nityaprakash/back-to-basics-dispose-vs-finalize/)

[Difference between Finalize and Dispose method](http://www.dotnet-tricks.com/Tutorial/netframework/P1MK271013-Difference-Between-Finalize-and-Dispose-Method.html)
