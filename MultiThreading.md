
# MultiThreading

###1.GENERALITY


####1.1 Processes vs Thread

- Processes are fully isolated from each other
- Threads have just limited degree of isolation
   -> Threads share (heap) memory with other running in the same application

###2. Threads

####2.2 First Thread

#####2.2.1 No parameter
```cs
public delegate void ThreadStart();
```
=> No returned value

```cs
Thread t = new Thread(WriteY);
t.Start();

static void WriteY(){...}
```


#####2.2.2 With parameter

```cs
public delegate void ParameterizedThreadStart (object obj);
```
=> Only one parameter

```cs
Thread t = new Thread(WriteY);
t.Start("a string as parameter");

static void WriteY(object param){}
```

####2.4 Local state vs shared state

```cs
staic void Main()
{
	new Thread(Go).Start();
	Go;
}

public void Go()
{
	for(int cpt = 0; cpt < 5; cpt++)
	{
		Console.Write("?");
	}	
}
```
The method Go will be instaniate two times in the stack. Each thread has its own memory stack. 
The program will display 10 times the question mark.

####2.3 Why to use the multi-threading

- Maintaining a responsive user interface
- Making efficient use of an otherwise blocked CPU
- Parallel programming
- Speculative execution
- Allowing requests to be processed simultaneously (by example: web server)

#####2.5 Foreground and Background Threads
By default, threads you create explicitly are **_foreground threads_**.
=> Once all foreground threads finish, the application ends, and any background threads still running abruptly terminate.

But, the _pooled_ threads (ie those managing by a thread pool) are background thread.

#####2.6 Thread Priority

```cs
enum ThreadPriority { Lowest, BelowNormal, Normal, AboveNormal, Highest }
```

_Think carefully before elevating a thread’s priority — it can lead to problems such as resource starvation for other threads._

#####2.7 The exception Handling

Any _try/catch/finally_ blocks in scope when a thread is created are of no relevance to the thread when it starts executing. Consider the following program:

```cs
public static void Main()
{
  try
  {
    new Thread (Go).Start();
  }
  catch (Exception ex)
  {
    // We'll never get here!
    Console.WriteLine ("Exception!");
  }
}

static void Go() { throw null; }   // Throws a NullReferenceException
```

The try/catch statement in this example is ineffective, and the newly created thread will be encumbered with an unhandled NullReferenceException. This behavior makes sense when you consider that each thread has an independent execution path.



####2.8 How Threading works

#####2.8.1 Thread scheduler

Multithreading is managed internally by a thread scheduler, a function the CLR typically delegates to the operating system.
A thread scheduler ensures all active threads are allocated appropriate execution time, and that threads that are waiting
or blocked (for instance, on an exclusive lock or on user input)  do not consume CPU time.

single-processor computer => a thread scheduler performs time-slicing

multi-processor computer => multithreading is implemented with a mixture of time-slicing and genuine concurrency,
where different threads run code simultaneously on different CPUs

#####2.8.2 Thread pooling

######2.8.2.1 Generality

Whenever you start a thread, a few hundred microseconds are spent organizing such things as a fresh private local variable stack. Each thread also consumes (by default) around 1 MB of memory. The _**thread pool**_ cuts these overheads by sharing and recycling threads, allowing multithreading to be applied at a very granular level without a performance penalty. This is useful when leveraging multicore processors to execute computationally intensive code in parallel in “divide-and-conquer” style.

The thread pool also keeps a lid on the total number of worker threads it will run simultaneously. Too many active threads throttle the operating system with administrative burden and render CPU caches ineffective. Once a limit is reached, jobs queue up and start only when another finishes

=> The thread pool uses a hill-climbing algorithm to manage the different threads.

######2.8.2.1.1 In the engine


######2.8.2.1.2 The conditions to optimize the thread pool

The thread pool strategy (implemented in the CLR) works best if the following condtions are met:
- work items are short running (<250 ms, ideally <100ms)
- the thread spend the most of their time blocked do not dominate the pool

The _oversubscription_ is the devil.


######2.8.2.2 Thread pool without TPL (prior 4.0)

The tasks didn't exist prior version 4.0, the only way to call the thread pool was to call 

```cs
ThreadPool.QueueUserWorkItem

public static bool QueueUserWorkItem(
	WaitCallback callBack
)
```



######2.8.2.3 Thread pool with the TPL

To use the thread pool, you can call __Task.Run__

```cs
Task.Run(() => Console.WriteLine("Hello the thread pool!"));
```


#####2.8.3.4 The thread pool inconveninces

- You can not set the name of a pooled thread
- the pooled threads are __always__ _background threads_.
- blocking pooled threads can degrade performance

To know if the thread is a pooled thread : Thread.CurrentThread.IsThreadPoolThread


####2.9 Some misunderstandings

#####2.9.1 The captured variables

By example, this code

```cs
for (int i = 0; i < 10; i++)
  new Thread (() => Console.Write (i)).Start();
```

won't return 012356789 but the returned output could be this one
0223557799

===> BEWARE : the captured variables => the problem is that the _i_ variable refers to the same memory location throughout the loop's lifetime. Therefore, each thread calls Console.Write on a variable whose the value may change as it running.

To avoid this problem, a solution is to use the temp variable : 

```cs
for (int i = 0; i < 10; i++)
{
  int temp = i;
  new Thread (() => Console.Write (temp)).Start();
}
```

Another error is:
```cs
string text = "t1";
Thread t1 = new Thread ( () => Console.WriteLine (text) );
 
text = "t2";
Thread t2 = new Thread ( () => Console.WriteLine (text) );
 
t1.Start();
t2.Start();
```

And the output will be:
- t1
- t2


###3.TASKS

####3.1 The Task Parallel Library (TPL)

The Task Parallel Library (TPL) has been introduced in the version 4.0 of the CLR

The Task Parallel Library (TPL) is based on the concept of a task, which represents an asynchronous operation. In some ways, a task resembles a thread or ThreadPool work item, but at a higher level of abstraction. The term task parallelism refers to one or more independent tasks running concurrently.

####3.2 Differences between Thread and Task

The differences are:
- A task could return a value, not a thread (actually, There is no direct mechanism to return the result from thread.)
- We can chain tasks together to execute one after the other. (with conditional continuation)
- Establish a parent/child relationship when one task is started from another task
- Child task exception can propagate to parent task
- Task support cancellation through the use of cancellation tokens.
- Asynchronous implementation is easy in task, using’ async’ and ‘await’ keywords.


####3.3 Task

```cs
Task(Action action); //A unit work
Task<TResult>(Func func); //A unit work with a result
TaskFactory
TaskFactory<TResult>
TaskScheduler
TaskCompletionSource //For manually controlling task's workflow
```

#####3.3.1 Task starting

```cs
static void Main()
{
	Task.Run(() => Console.WriteLine("I'm the task");
}
```

#####3.3.2 Returning values

Task<TResult> could be seen as a _future_ (or _promise_ ...). 

```cs
static void Main()
{
	Task<int> primeNumberQuantity = Task.Run(() =>
		Enumerable.Rabge(2, 3000000).Count(n =>
			Enumerable.Range(2, (int)Math.Sqrt(n)-1).All(i => n % i > 0)));
	
	Console.WriteLine("The compuation running...");
	Console.WriteLine("The quantity is : " + primeNumerQuantity);
}
```
The second line will be displayed in the console when the task has returned a value. As you can see, the Task<TResult> is like a future.

#####3.3.3 State object / AsyncState object

######3.3.3.1 State object

It's better to use TaskFactory than Task.Run because there are more options.
Task.Run is a shortcut.

```cs
static void Main()
{
	var task = Task.Factory.StartNew(DisplayMessage,"Hello World!");
	task.Wait();
}

static void DisplayMessage(object state)
{
	Console.WriteLine(state);
}
```



######3.3.3.2 AsyncState object

```cs
static void Main()
{
	var task = Task.Factory.StartNew(DisplayMessage,"Hello World!");
	task.Wait();
}

static void DisplayMessage(object state)
{
	Console.WriteLine(state);
}
```

#####3.3.4 TaskCreationOptions

- LongRunning : the scheduler will dedicate a thread to avoid the switch context for the long running task
- PreferFairness : the sheduler will try to ensure that taks are scheduled in the order they were started
- AttachedToParent : dedictated to the child task



####3.4 Exceptions

#####3.4.1 Exceptions

Unlike the threads, the tasks propagate the exception.

#####3.4.2 Unobserved exceptions


####3.4 Chaining taks (Continuation)

#####3.4.1 ContinueWith

To chain the tasks, we will use __ContinueWith__

```cs
Task task1 = Task.Factory.StartNew(() => Console.Write("1st task"));
Task task2 = task1.ContinuationWith(antRef => Console.Write("2nd Task. The ref of the previous tasks : " + antRef));
```

By default, the two tasks may execute on different tasks. To avoid that, you must specify __TaskContinuationOptions.ExecuteSynchronously__.

```cs
Task task1 = Task.Factory.StartNew(() => Console.Write("1st task"));
Task task2 = task1.ContinuationWith(antRef => Console.Write("2nd Task. The ref of the previous tasks : " + antRef)
, TaskContinuationOptions.ExecuteSynchronously);
```

#####3.4.2 Example

```cs
Task.Factory.StartNew<int>(() => 16)
	.ContinueWith(ant => ant.Result * 4)
	.ContinueWith(ant => ant.Math.Sqrt(ant.Result))
	.ContinueWith(ant => Console.WriteLine(ant.Result)); //8
```
#####3.4.3 Continuation and exception



#####3.4.4 Conditional conditions



####3.5 Task cancellation


####3.6 Task scheduler

The task scheduler allocates tasks to threads and it is represented by the abstract TaskScheduler class.
There are two concrete implementation:
- _default scheduler_
- _synchronization context scheduler_

####3.7 AggregateException & Flatten/Handle

#####3.7.1 Flatten

#####3.8.1 Handle

####3.9 Debugging

Debug -> Windows -> Parallel Task

####3.10 Parallel.For



###4.SYNCHRONIZATION

####4.1 Categories

######4.1.1 Basic synchronization
- Join

######4.1.2 Exclusive locking
- Monitor, lock
- Mutex (computer-wide locks) 
- SpinLock

#####4.1.3 Non exclusive locking
The purpose is to _limit_ the concurrency
- Semaphore (Slim)
- ReaderWriterLock

#####4.1.4 Sigaling
A thread can be block until receiving one or more notifaction
- ManualResetEvent(Slim)
- AutoResetEvent
- CountdownEvent
- Barrier

#####4.1.5 _Nonblocking synchronization construct_

- Thread.MemoryBarrier
- Thread.VolatileRead
- Thread.VolatileWrite

#####4.1.6 Declarative synchronization: inheritance from _ContextBoundObject_ and _Synchronization_ attribute
There are two ways to use locks:
- lock manually
- to lock _declaratively_.

```cs

using System;
using System.Threading;
using System.Runtime.Remoting.Contexts;

[Synchronization]
public class MyClassToLock : ContextBoundObject
{
	public void Go()
	{
		Console.WriteLine("My auto lock");
	}
}

public static void Main()
{
	MyClassToLock safeInstance = new MyClassToLock();
	new Thread(safeInstance.Go).Start();
	new Thread(safeInstance.Go).Start();
}
```




####4.2 Thread Join

```cs
static void Main()
{
	Thread t = new Thread(() => for(int cpt = 0; cpt < 1000; cpt++){Console.WriteLine(cpt)});
	t.Start();
	t.Join();
}
```
The main thread will be blocked during the execution of the thread t. 
Problem: when a thread is blocked, the CPU starts an another thread (=> switch context)

####4.3 Mutual exclusion

Let have a look on the different mutual exclusion.

#####4.3.1 Lock

Space name : System.Threading.Monitor



#####4.3.2 Monitor

```cs
lock (x)
{
    DoSomething();
}
```

This is equivalent to:

```cs
System.Object obj = (System.Object)x;
System.Threading.Monitor.Enter(obj);
try
{
    DoSomething();
}
finally
{
    System.Threading.Monitor.Exit(obj);
}
```

#####4.3.3 Mutex

A Mutex is like a C# lock, but it can work across multiple processes. In other words, Mutex can be computer-wide as well as application-wide.

Acquiring and releasing an uncontended Mutex takes a few microseconds — about 50 times slower than a lock.

```cs
using(var mutex = new Mutex(true, "Olivier Mounicq Developer"))
{
	try
	{
		...
	}
	catch(Exception ex)
	{
	}
	finally
	{
		mutex.ReleaseMutex();
	}
}
```


#####4.3.5 Lock vs Mutex

So, the differences between Lock and Mutex are:
- lock is a compiler keyword, not an actual class or object. 
- Lock is a wrapper around the functionality of the Monitor class and is designed to make the Monitor easier to work with for the common case.
- The Monitor (and the lock keyword) are restricted to the AppDomain
- Mutex is specific to the Operating System allowing you to perform cross-process locking and synchronization.
- Mutex is a cross process

#####4.3.3 Performance
With computer on 2015-aera:
- Lock about 50 nanosec to aquire and release a lock
- Mutex about 1 microsecond

#####4.3.3 Lock's drawbacks
- locks block the threads
- locks are slow
- Deadlock

####4.4 Non exclusive locks

####4.4.1 Semaphore

A semaphore with a capacity of one is similar to a Mutex or lock, except that the semaphore has no “owner” — it’s thread-agnostic. Any thread can call Release on a Semaphore, whereas with Mutex and lock, only the thread that obtained the lock can release it.

####4.4.2 ReaderWriterLockSlim

- EnterReadLock()
- ExitReadLock()
- EnterWriteLock()
- ExitWriteLock()

ReaderWriterLockSlim allows more concurrent Read activity than a simple lock.

###5. Concurrent list or Thread safe list

By default the enumerating collections are thread-unsafe : an exception is thrown if the list is modified during the enumaration. (To avoid the exception, we must duplicate the enumeration before to enumerate the items) 

####5.1 The different concurrent lists

The differerent concuurent lists are:
- BlockingCollection<T> : Provides bounding and blocking functionality for any type that implements _IProducerConsumerCollection<T>_
- ConcurrentDictionary<TKey, TValue> : a thread safe dictionary
- ConcurrentQueue<T> : FIFO thread safe list
- ConcurrentStack<T> : LIFO thread safe list
- ConcurrentBag<T> : unordered thread safe list
- IProducerConsummerCollection<T> : The interface that a type must implement to be used in a _BlockingCollection_

###6. Misc

####6.1 Singleton

#####6.1.1 Singleton vs static

- singleton can extend a base class
- singleton can implement an interface
- singleton can be initialized lazily or asynchronously while a static class is generally initialized when it is first loaded
- singleton is OO pattern (inhertitance, polymorphisme)
- singleton can be treated as a normal object

But it is impossible to test the singleton.

#####6.1.2 Singleton and MemoryBarrier


#####6.2 Static member

If two threads want to call the property DateTime.Now, an error will be raised. Only way to avoid it's to lock the type itself:

```cs
lock(typeof(DateTime))
{
	return DateTime.Now;
}
```



###7 async / await

```cs
Task<int> GetPrimesCountAsync(int start, int count)
{
     return Task.Run(() => ParallelEnumerable.Range(start, count).Count(n => Enumerable.Range(2, (int)Math.Sqrt(n)-1).All(i => n % i > 0)));
}

....

async void Display()
{
     int res = await GetPrimesCountAsync(2, 1000000);
     Console.WriteLine(res);
}
```
















