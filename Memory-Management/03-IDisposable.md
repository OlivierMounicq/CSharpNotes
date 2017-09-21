# The interface IDisposable & Dispose pattern

## 1. Unmanaged/managed resources 

### 1.1 What is a resource?

A resource is living outside of the process. So a resource is external

### 1.2 The resource types

There are three different kinds of resources:
- full managed resources : this resource are allocated on the CLR's heap memory (the memory space and their field are managed by the CLR).
- an object with unmanaged and manages resources : those resources have some _unmanaged_ fields and some _managed_ fields. Their memory space is managed by the CLR.
- full unmanaged resource object : the resource lives outside of the CLR's heap memory (native resource).


### 1.3 Why to use the Finalizer

The GC pushes all object with a ```Finalize``` method in a special queue : _Finalize queue_. And there is a thread dedicated to this queue. Its goal is simply : calling the ```Dispose``` method for each object in this queue and removing the object from this queue.

### 1.4 Why to use the method Dispose ?

First, the Dispose method is __never called by the GC__. We use Dispose method to prepare the instance to be ready-deleted. Generally, we use ```using``` keyword to call automatically the ```Dispose``` method.

Actually, there are two cases to use Dispose method:
- using dispose method with managed resource : actually you want to clean the object before its deletion by the GC. But don't forget: even you call the ```Dispose``` method, you won't trigger garbage collection.
- using to dispose the unmanaged resource with also using the ```Finalize``` method to set up the security in order to be sure that the unmanaged resources will always be destroyed. (the CLR does not the manage the space of the unmanaged resource, so we must define explicitly the workflow to clean up the memory)

Actually the managed resources are totally managed by the garbage collector (the allocation and the deallocation). So when there is no reference pointing to object, the memory used by the object will be free by the garbage collector.  
But if the object owns unmanaged resource (like file, database connection, ...), you must delete __explicitly__ the reference of unmanaged resource otherwise the object won't be deallocated (because the reference from the unmanaged resource is considered as a root keeping the object in the memory).

To deallocate explicitly the unmanaged resource, you have to create and __call__ a ```Dispose``` method (of the ```IDisposable``` interface) to explain the deallocation of the unmanaged resource. 

An examle of ```Dispose``` method using 

```cs 
public class Foo : IDisposable
{
  public void DoAction()
  {
    Console.WriteLine("Do action");    
  }
    
  public void Dispose()
  {
    Console.WriteLine("The Dispose method has been calling");    
  }
}
public static class Main
{
  public static void Run()
  {
    using(var foo = new Foo())
    {
      foo.DoAction();      
    }
  } 
}
Main.Run();

```

[the code](http://csharppad.com/gist/1ab8459fe95aebe21f12cd861c6a63de)

## 2. Dispose vs Finalize

### 2.1 Summary

So the methods:
- ```Dispose``` is mandatory for the objects using unmanaged resources and we explain how to close/clean/delete the unmanaged resource
- ```Finalize``` method is used to force the ```Dispose``` method calling in the case the developer has forgotten to call explicitly the ```Dispose``` method in his/her code.

So there are two cases for the object with unmanaged resource reference:
- either the developer call explicitly or by using the ```using``` keyword the ```Dipose```method => everything is OK for the GC: it can delete the object
- the developer forgot to call the ```Dispose``` method : in this case, we have to use the Finalizer. All objects with a ```Finalize``` method will be pushed into a Finalize queue and then the GC will call for each object in this queue the ```Finalize``` method.

### 2.2 Finalize & destructor

#### 2.2.1 Finalize and destructor.

You cannot create and __call__ a _Finalize_ method which is called by the Garbage Collector. The only way is to to create a destructor. All the code inside the destructor will be copied in the _Finalize_ method by the CLR.
And the _Finalize_ is only called by the CLR. Never by the developper.

#### 2.2.2 Why to use the finalizer?

If the code does not call the Dispose (the developer has forgotten to call the _Dispose_ method), the object won't be released. To avoid this problem, you can to call the _Dispose_ method from the destructor (consequently, from the finalizer).

#### 2.2.3 The rules

Avoid dispose the managed ressources in the destructor/finalize method. (In this case, an exception could be throw).

### 2.4 Destructor/Finalize vs Dispose.

The main difference between them is the deterministic and non deterministic call.
The developper decides to call the Dispose method: it's a deterministic process.

But the developper is not able to trigger the garbage collector, only the CLR decides when to trigger the garbage collector: it's a non deterministic process.


### 2.3 The worklow

So there are two ways to call the _Dispose_ method:
- the developer calls in his/her code the _Dispose_ method
- the finalizer/destructor calls the the _Dispose_ method.

But if the developer has called the _Disposible_ method in his/her code, it's useless to call the _Dispose_ method from the finalizer. When the method Dispose has been called, you must suppress the object in the finalize queue to gain performance.

### 3.Dispose properties

The properties are:
- you can call the _Dispose_ method even if the object has been disposed
- if I call another method on a disposed object, an _ObjectDisposedException_ will be thrown


```cs
using System;
using System.IO.

class Program
{
  static void Main(string[] args)
  {
    var stream = new StreamWriter(@"C:\dummyfolder\dummy.txt", false);
    
    using(stream)
    {
      stream.WriteLine("Hello world");
    }
  
    //The Dispose method has been called (see the using keyword)
    
    stream.Dispose(); //No error
    
    try
    {
      stream.WriteLine("I try to write!");
     }
     catch(ObjectDisposedException ex)
     {
        Console.WriteLine("I try to use a method of a disposed object");
     }  
  
     stream.Dipose(); //NO error
     
     Console.ReadLine();
  }
}
```



### 4.The Dispose pattern

#### 4.1.When should you use and implement the IDisposable interface and the finalizer?

- Unmanaged resource : Dispose + Finalizer 
- Managed resource : Dispose

Never use the Finalizer method in the case of the full managed resource: the CLR does not need an explanation to free up the memory. Otherwise, the garbage collector will add the object in Finalizer queue, apply the useless methods defined in Finalizer and remove the objec from the Finalizer queue: you waste the time of the Garbage Collector.


#### 4.2. How to use the IDisposable interface?

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

### 4.3 A full example

This example comes from [system.object.finalize](https://msdn.microsoft.com/en-us/library/system.object.finalize(v=vs.110).aspx)

```cs 
using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

public class FileAssociationInfo : IDisposable
{
   // Private variables.
   private String ext;
   private String openCmd;
   private String args;
   private SafeRegistryHandle hExtHandle, hAppIdHandle;

   // Windows API calls.
   [DllImport("advapi32.dll", CharSet= CharSet.Auto, SetLastError=true)]
   private static extern int RegOpenKeyEx(IntPtr hKey, 
                  String lpSubKey, int ulOptions, int samDesired,
                  out IntPtr phkResult);
   [DllImport("advapi32.dll", CharSet= CharSet.Unicode, EntryPoint = "RegQueryValueExW",
              SetLastError=true)]
   private static extern int RegQueryValueEx(IntPtr hKey,
                  string lpValueName, int lpReserved, out uint lpType, 
                  string lpData, ref uint lpcbData);   
   [DllImport("advapi32.dll", SetLastError = true)]
   private static extern int RegSetValueEx(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string lpValueName,
                  int Reserved, uint dwType, [MarshalAs(UnmanagedType.LPStr)] string lpData,
                  int cpData);
   [DllImport("advapi32.dll", SetLastError=true)]
   private static extern int RegCloseKey(UIntPtr hKey);

   // Windows API constants.
   private const int HKEY_CLASSES_ROOT = unchecked((int) 0x80000000);
   private const int ERROR_SUCCESS = 0;

    private const int KEY_QUERY_VALUE = 1;
    private const int KEY_SET_VALUE = 0x2;

   private const uint REG_SZ = 1;

   private const int MAX_PATH = 260;

   public FileAssociationInfo(String fileExtension)
   {
      int retVal = 0;
      uint lpType = 0;

      if (!fileExtension.StartsWith("."))
             fileExtension = "." + fileExtension;
      ext = fileExtension;

      IntPtr hExtension = IntPtr.Zero;
      // Get the file extension value.
      retVal = RegOpenKeyEx(new IntPtr(HKEY_CLASSES_ROOT), fileExtension, 0, KEY_QUERY_VALUE, out hExtension);
      if (retVal != ERROR_SUCCESS) 
         throw new Win32Exception(retVal);
      // Instantiate the first SafeRegistryHandle.
      hExtHandle = new SafeRegistryHandle(hExtension, true);

      string appId = new string(' ', MAX_PATH);
      uint appIdLength = (uint) appId.Length;
      retVal = RegQueryValueEx(hExtHandle.DangerousGetHandle(), String.Empty, 0, out lpType, appId, ref appIdLength);
      if (retVal != ERROR_SUCCESS)
         throw new Win32Exception(retVal);
      // We no longer need the hExtension handle.
      hExtHandle.Dispose();

      // Determine the number of characters without the terminating null.
      appId = appId.Substring(0, (int) appIdLength / 2 - 1) + @"\shell\open\Command";

      // Open the application identifier key.
      string exeName = new string(' ', MAX_PATH);
      uint exeNameLength = (uint) exeName.Length;
      IntPtr hAppId;
      retVal = RegOpenKeyEx(new IntPtr(HKEY_CLASSES_ROOT), appId, 0, KEY_QUERY_VALUE | KEY_SET_VALUE,
                            out hAppId);
       if (retVal != ERROR_SUCCESS) 
         throw new Win32Exception(retVal);

      // Instantiate the second SafeRegistryHandle.
      hAppIdHandle = new SafeRegistryHandle(hAppId, true);

      // Get the executable name for this file type.
      string exePath = new string(' ', MAX_PATH);
      uint exePathLength = (uint) exePath.Length;
      retVal = RegQueryValueEx(hAppIdHandle.DangerousGetHandle(), String.Empty, 0, out lpType, exePath, ref exePathLength);
      if (retVal != ERROR_SUCCESS)
         throw new Win32Exception(retVal);

      // Determine the number of characters without the terminating null.
      exePath = exePath.Substring(0, (int) exePathLength / 2 - 1);
      // Remove any environment strings.
      exePath = Environment.ExpandEnvironmentVariables(exePath);

      int position = exePath.IndexOf('%');
      if (position >= 0) {
         args = exePath.Substring(position);
         // Remove command line parameters ('%0', etc.).
         exePath = exePath.Substring(0, position).Trim();
      }
      openCmd = exePath;   
   }

   public String Extension
   { get { return ext; } }

   public String Open
   { get { return openCmd; } 
     set {
        if (hAppIdHandle.IsInvalid | hAppIdHandle.IsClosed)
           throw new InvalidOperationException("Cannot write to registry key."); 
        if (! File.Exists(value)) {
           string message = String.Format("'{0}' does not exist", value);
           throw new FileNotFoundException(message); 
        }
        string cmd = value + " %1";
        int retVal = RegSetValueEx(hAppIdHandle.DangerousGetHandle(), String.Empty, 0, 
                                   REG_SZ, value, value.Length + 1);
        if (retVal != ERROR_SUCCESS)
           throw new Win32Exception(retVal);                          
     } }

   public void Dispose() 
   {
      Dispose(true);
      GC.SuppressFinalize(this);
   }   

   protected void Dispose(bool disposing)
   {
      // Ordinarily, we release unmanaged resources here; 
      // but all are wrapped by safe handles.

      // Release disposable objects.
      if (disposing) {
         if (hExtHandle != null) hExtHandle.Dispose();
         if (hAppIdHandle != null) hAppIdHandle.Dispose();
      }
   }
}
```

## 5.Abstract

The _Dispose_ method is made to explain how to deallocate the unmananaged and the managed resource.
If the developer forgot to call the _Dispose_ method in his/her code, the _Finalize_ method is made to call automatically (actually the CLR will do the job) the _Dispose_ method in order to deallocate the unmanaged resource and release the memory space used by the object.

## 6. Some url

[IDisposable for Dummies #1 – Why? What?](http://blog.ilab8.com/2012/04/26/idisposable-for-dummies-1-why/)

[IDisposable for dummies #2 – A guide about ‘how to implement it’](http://blog.ilab8.com/2012/04/29/idisposable-for-dummies-2-how/)

[How to Implement IDisposable and Finalizers: 3 Easy Rules](http://blog.stephencleary.com/2009/08/how-to-implement-idisposable-and.html)

[IDisposable and using in C#](https://coding.abel.nu/2011/12/idisposable-and-using-in-c/)

[Implementing IDisposable and the Dispose pattern](http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P)

[Back to basics : Dispose vs finalize](http://www.c-sharpcorner.com/UploadFile/nityaprakash/back-to-basics-dispose-vs-finalize/)

[Difference between Finalize and Dispose method](http://www.dotnet-tricks.com/Tutorial/netframework/P1MK271013-Difference-Between-Finalize-and-Dispose-Method.html)

[Finalize vs Dispose](http://stackoverflow.com/questions/732864/finalize-vs-dispose)

[Dispose pattern](https://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.110).aspx)

[Object.Finalize Method ()](https://msdn.microsoft.com/en-us/library/system.object.finalize(v=vs.110).aspx)  

[IDisposable and Thread Safety](http://www.blackwasp.co.uk/ThreadSafeIDisposable.aspx)
