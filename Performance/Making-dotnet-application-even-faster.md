
## 1. Garbage collection internals

### 1.1 Basic concepts

* Garbage collecton means we don't have to manually free memory

* Garbage collector is not free and has performance trade-off
  * questionable on real-time system, mobile device

* The CLR Garbage Collector is _almost-concurrent_, _parallel_, _compacting_, _mark-and-sweep_, _generational_, _tracing GC_.

### 1.2 Mark and sweep

There is three phases:
* Mark : identify all live objects
* Sweep : reclaim dead objects
* Compact : shift live objects together

Objects that can still be used must be kept alive

### 1.3 Roots

* Starting points for the Garbage Collector

The main roots are:
* Static variables
* local variable
* Finalization queue, f-reachable queue, GC handles ...
* Roots can ause memory leaks

```cs

internal class Program
{
    public static void TimerProcedure(object param)
    {
        Console.Clear();
        Console.WriteLine(DateTime.Now.TimeOfDay);
        
        GC.Collect();
    }

    static void Main(string[] rgs)
    {
        Console.Title = "Desktop clock";
        Console.SetWindowSize(20,2);
        Timer timer = new Timer(TimerProcedure, null, TimeSpan.Zero, TimeSpan.FromSecond(1));
        Console.ReadLine();
        timer.ToString(); //to avoid the frozen timer in the debug mode
        //Or
        GC.KeepAlive(timer);
    }
}
```

In the debug mode, the JIT compiler tells the GC that the local variable lifetime as to extended until the end of the methods.

The method ```GC.KeepAlive``` extends the local root's scope

### 1.4 Workstation GC

* There are multiple GC flavors
* Workstation GC is "kind of" suitable for client apps (WPF, console, service....)
  * The default for almost .net applications
  * unless the application runs inside ASP.NET
* GC runs on a single thread (concurrent or non concurrent)
* Concurrent workstation GC
  * Special GC thread : it is not triggered when the memory is full, it tries to optimize the garbage collection
  * Runs concurrently with application thread, only shot suspension
* Non-concurrent workstation GC:
  * No special GC thread
  * One of the app threads does the GC
  * All threads re suspended during GC
  
#### 1.4.1 Server GC

* One GC thread per logical processor, all working at once
* Separate heap area for each logical processor
* until the CLR 4.5, server GC was non-concurrent
* in CLR 4.5, server becomes concurrent
  * Now a reasonable default for many nigh-memory apps
 
#### 1.4.2 Configuration

* Configure preferred flavor in __app.config__
  * ignored if invalid
  
```xml
<?xml version = "1.0" encoding="UTF-8" ?>
<configuration>
    <runtime>
        <gcServer enabled="true|false" />
        <gcConcurrent enabled="true|false" />
    </runtime>
</configuration>
```

  
  
