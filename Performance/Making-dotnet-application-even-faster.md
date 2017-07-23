
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

### 1.4 GC Flavors

There are two 

### 1.4.1 Workstation GC

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
  
#### 1.4.2 Server GC

* One GC thread per logical processor, all working at once
* Separate heap area for each logical processor
* until the CLR 4.5, server GC was non-concurrent
* in CLR 4.5, server becomes concurrent
  * Now a reasonable default for many nigh-memory apps
 
#### 1.4.3 Configuration

* Configure preferred flavor in __app.config__
  * ignored if invalid
  * cannot switch flavors at runtime, but can query flavor by using ```GCSettings``` class.
  
  
```xml
<?xml version = "1.0" encoding="UTF-8" ?>
<configuration>
    <runtime>
        <gcServer enabled="true|false" />
        <gcConcurrent enabled="true|false" />
    </runtime>
</configuration>
```

### 1.5 Tools

Concurrency Visualizer

  
### 1.6 Generations

#### 1.6.1 Goals

* A full GC on entire heap is expensive and inefficient
* Divide the heap into regions nd perform smal  collections
* New objects die fast, old object stay alive
  * Typicall behavior for any applications
  * for instance : A web server
    * the object created for the query will die just after to sent the response
    * the object used for the initialization of server will stay alive for a while
 
#### 1.6.2 Characteristics

* The generations are larger on 64-bits system than the 32-bits system
* Generation sizes depend on the CPU cache size and even the amount of physical memory
* Make sure your temporary objects die young and avoid frequent promotions to generations 2

### 1.7 SOH / LOH

* Large objects are stored in a separate heap region (LOH)
  * _Large_ means larger than 85000 bytes or array of > 1000 doubles   

* The GC does not compact the LOH
  * this may cause fragmentation
* The LOH is considered art of generation 2 : the LOH objects are considered as old object.
  * Temporary large objects are a common GC performance problem.
* LOH fragmentation leads to waste of memory
  * .net 4.5.1 introduces LOH compaction
  
```cs
GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
GC.Collect();
```

  * You can test for LOH fragmentation using the ```!dumphead -stat``` SOS command

### 1.8 Foreground and background GC

* In concurrent GC, application threads continue to run during full GC
* What happens if an application thread allocates during the GC ?
  * In the CLR 2.0, the application thread waits for full GC to complete
  * In the CLR 4.0, the application thread launches a _foreground_ GC
  
### 1.9 Finalization

* The CLR runs a finalizer after the object becomes unreachable
* Let's design the finalization mechanism
  * Finalization queue for potentially "finalizable" objects
  * Identify candidates for finalization
  * Selecting thread for finalization : the finalizer thread
  * F-reachable queue for finalization candidates
  
* The finalization extends object lifetime
* The f-reachable queue might fill up faster than the finalizer thread can drain it





* the Finalize queue is a root



## 2. Garbage Collector and Performance counters

### 2.1 The metrics


### 2.2 Switching to value type

- 1. The value types are more friendly to the GC : the value types is always smaller than the reference type (Sync Block Index + Method Table Pointer)
- 2.The value types are embedded in their container
  - an array of struct is stored contiguously in memory
  - an array of reference type stores references ! so the array is not stored contiguously.
- Value types are easier for the GC to traverse  

