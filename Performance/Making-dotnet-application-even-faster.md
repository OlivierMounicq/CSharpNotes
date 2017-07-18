
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
