## Why Value Type ?

 * Reference type offer a set of managed services : locks, inheritance, and more
 * Values type : 
 	* you cannot use lock, inheritance and more! But 
  	* you could avoid the pressure on the garbage collector because instantiate on the stack

Additional difference between Reference type and Value type
* parameter passing :
	* value type : compare the content
	* reference type : 
* equality 


## Object Layout

### Reference Type

Heap objects (reference type) have two header fields:


| Object Header Word     |
|:-----------------------|
| Method Table pointer   |
| First Object Field     |
| Second Object Field    |
 

__Object Header Word__
- 4 bytes long (32 bits) or 8 bytes long (64 bits)
- helps to synchronise mechanism (with lock statement) 
- GC maintains a list of free block 
- HashCode (if the dashcode has not been overriding) 

__Method Table Pointer__
- it helps the compiler to look up the method version and implement Polymorphism, the reflection service 


### Value Type

Value types (stack objects) don’t have headers. 
=> As the value type has not the header, so they cannot use the managed services.


Using the Value Types

Use value types when performance is critical:
* creating a large number of objects and keep then in the memory in the same time
* creating a large collection


For instance:

```cs
public struct / class Point2D
{
	public int X;
	public int Y;
}
```

Array of "class Point” => 10 000 000 objects = 320 000 000 bytes
Array of “struct Point” => 10 000 000 objects = 80 000 000 bytes

Memory reduction : 1/4 

![01](https://github.com/OMQ/CSharpNotes/blob/master/Performance/img/Making-dotnet-application-faster-01.png)


What is even worse : to access two continuous points need cache


## Basic Value Type

The basic value type implementation is inadequate : if you use an array of Struct, we don’t enhance anymore the memory performance. But there are two operations that we could enhance: comparing and hashing object.

### Origin of Equals

``` List<T>.Contains ``` call Equals

Equal method
Declared by ```System.Object``` and overridden by ```System.ValueType```

```cs
//In System.ValueType, simplified version
public override bool Equals(object o)
{
 if(o == null) return false; //there is no point by executing this statement !
 if(o.GetType() != GetType()) return false; 
  if(CanCompareBits(this)) return FastEqualsCheck(this, o);
  foreach(FieldInfo f in GetType().GetFields()){
    if(!f.GetValue(this).Equals(f.GetValues(o)) return false;
  }
    
  return true;
}
```

### Boxing

Equal parameter must be boxing because the parameter of the Equals method is an object:

```cs
public virtual bool Equals(object o);
```

![01](https://github.com/OMQ/CSharpNotes/blob/master/Performance/img/Making-dotnet-application-faster-02.png)

### Avoiding Boxing and Reflection

- Override ```Equals```
- overload ```Equals```

```cs
struct Point2D : IEquatable<Point2D>
{
	public int X, Y;
	public override bool Equals(object o){...}
	public bool Equals(Point2D p){...}
}
```

### Final Tuning

#### Add equality operators

```cs
struct Point2D
{
	public static bool operator==(Point2D a, Point2D b)...;
	public static bool operator!=(Point2D a, Point2D b)...;
}
```
You have to implement both operator (equal and not equal) otherwise the compiler will trigger an error.


### Add GetHashCode

* Used by ```Dictionary```, ```HashSet``` and other collections.
* Declared by ```System.Object```overriden by System.ValueType
* Must be consistent with ```Equals``` : A.Equals(B) => A.GetHashCode() == B.GetHashCode()

```cs
struct Point2D : IEquatable<Point2D>
{
  	public int X;
  	public int Y;
	
	public override int GetHashCode()
	{
		int hash = 19;
		hash = hash * 29 + X;
		hash = hash * 29 + Y;
		return hash;
	}
}
```

### The code

#### First version: the worse performance

```cs
struct Point2D : IEquatable<Point2D>
{
  	public int X;
  	public int Y;
}
```

#### Second version: the best performance

```cs
struct Point2D : IEquatable<Point2D>
{
  	public int X;
  	public int Y;
	
	public override bool Equals(object obj)
	{
		if(!(obj is Point2D)) return false;
		Point2D other = (Point2D)obj;
		return  X == other.X a&& Y == other.Y;
	}

	public bool Equals(Point2D other)
	{
		return X == other.X && Y == other.Y;
	}
}
```

## Startup costs

### Cold Startup

Start the application for the first time since you boot your system.  
The common cost is the disk I/O : load assemblies, windows dll, data file (and after all assemblies/file are available in the cache for others applcations).  

### Warm Startup
You launch your application again after to close it.

* JIT Compilation (warming :  the cost)
* Signature validation
* DLL rebasing
* Initialization

### Improving Startup Time with NGen
 
NGen precompiles .NET assemblies to native code
Ø  Ngen install MyApp.exe
* Includes dependencies
* Precompiled assemblies stored in C:\Windows\Assembly\NativeImages
* Fall back to original if stale

<u>Automatic NGen in Windows and CLR 4.5</u>
 
Enable by default in the windows services



### Multi-Core Background JIT
 
* Usually, methods are compiled to native when invoke
* Multi-core background JIT in CLR 4.5
	* Opt in using System.Runtime.ProfileOPtimization class

```cs
Using System.Runtime;
 
ProfileOptimization.SetProfileRoot(folderName);
ProfileOptimization.StartProfileRoot(folderName);
``` 
 
A method already precompiled in another thread could be used directly without to use JIT to get the native code of the method
 
Relies on profile information generated at runtime : that information is used to determine which methods are likely to be invoked.
 
### RuyJIT
 
A rewrite of the JIT Compiler
* Faster compilation (throughput)
* Better code (quality)
 
And it fixes some issues like pool code generation, ..etc.   
Relies on profile information collected at runtime
 
[performance improvements in ryujit in .net core and .net framework](https://blogs.msdn.microsoft.com/dotnet/2017/06/29/performance-improvements-in-ryujit-in-net-core-and-net-framework/)  
[github.com/dotnet/announcements/issues/10](https://github.com/dotnet/announcements/issues/10)

#### How to set the JIT version

```bat
c:\program> set COMPLUS_AltJit = *
```

### Managed Profile-Guided Optimization (MPGO)
 
Introduced in .NET 4.5
* Improves precompiled assemblies’ disk layout
* Places hot code and data closer together on disk 

## Improving Cold Startup

### I/O cost are &#35;1 thing to improve

#### ILMerge (Microsoft research)

A.dll + B.dll + C.exe => ILMerge => Merged C.exe

#### Executable packers

Example : Rugland Packer (RPX), available on CodePlex


NTFS File compression

#### Placing strong-named assemblies in the GAC

The CLR has to verify the intergry of the dll (it has to calulate the hash code of each page). 
This step could be skipped if the dll is loaded from the GAC because the CLR considers the GAC like a trusted, secrure location.

#### Windows SuperFetch

It prefetchs code and data by considering the previous application runnings.

Windows SuperFetch can not be disambled.

Windows SuperFetch is a windows service.

### Precompiling Serialization Assemblies

#### Serialization often creates dynamic methods on the first use

```cs
void Dynamic_Serialize_MyClass(MyClass c, BinaryWriter w)
{
	w.WriteInt32(c.X);
	w.WriteSingle(c.Y)
	w.WriteUInt16(c.Z);
}
```

#### These methods can be precompiled

* __SGen.exe__ creates precompiled serialization assemblies for XmlSerializer.
* Protobuf-net has a precompilation tool


## Precompiling Regexes

By default, the Regex class interprets the regular expression when you match it.  
Regex can generate IL code instead of using interpretation:

```cs
Regex r = new Regex(pattern, RegexOptions.Compiled);
```

Even better, you can precompile regular expression to an assembly:


```cs
var info = new RegexCompilationInfo(
	@"[0-9]+", RegexOptions.None,
	"Digits", "Utils", true);
Regex.CompileToAssembly(
	new[] { info }, new AssemblyName("RegexLib, ..."));
```

```Digits``` is the class name, ```Utils``` is the namespace


## Pointers

### Why to use pointers in C# ?

* Interoperability with Win32 and other DLLs
* Performance in specific scenarios

### Pointers and pinning

Accessing to an array:
* we want to go from ```byte[]``` to ```byte*``` 
* When getting a pointer to a heap object, what if the GC moves it during the compaction phase (in the SOH)? : actually, there will be a conflict : the GC change the adress memory of the object but it is not aware about the pointer which is an external reference for it.

So to use pointer, we have to pin the object:

```cs
byte[] source = ...;
fixed (byte* p = &source)
{
 ...
}
//In the following code, the source object won't be pinning
```

__Beware__ : by pinning the object, the GC won't be able to compact the SOH memory.

If there is an error during the pinning, the object won't be pinning and the keywork ```fixed``` acts as try/catch block.

### Directly manipulate memory

```cs
//copy the first element of the array
*p = (byte)12;

//cast of the first element into an INT
int x = *(int*)p;
```

With the pointer, yo can access to memeory zone out of the array: it requires ```unsafe``` block and "Allow unsafe code".

### Copying memory using pointers

* Mimicking ```Array.Copy``` or ```Buffer.BlockCopy```  
* Better to copy more than one byte per iteration

```cs
public static unsafe void Copy(byte[] src, byte[] dst)
{
	fixed (byte* p =&src[0]);
	fixed (byte* q =&dst[0]);
	{
		long* pSrc = (long*)p;
		long* pDst = (long*)q;

		for(int i = 0; i < dst.Length/8; i++)
		{
			*pDst = *pSrc;
			++pDst; ++pSrc;
		}
	}
}
```

* Might be interresting to unroll the loop


### Reading Structure

#### Marshal.PtrToStructure

* ```System.Runtime.InteropServices.Marshal``` is designed for the interopearbility scenarios
*  The method ```Marshal.PtrToStructure``` is useful to read structure from unmanaged memory.
	* ``` Object PtrToStructure(Type type, IntPtr address)``` 
* the GC can pin an object in memory and give us the pointer to it:

```xs
GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
try
{
	IntPtr address = handle.AddrOfPinnedObject();
}
finally
{
	//You are responsable about the object pinning
	handle.Free();
}
```

#### Using pointers

* Pointers can help by casting 

```cs
fixed(byte* p = &data[offset])
{
	TcpHeader* pHeader = (TcpHeader*)p;
	return *pHeader;
}
```

#### Generic approach

* Unfortunately, ```T*``` does not work : T must be blittable. 
* We can generate a method for each ```T``` and call it when necessary
	* Reflection.Emit
	* CSharpCodeProvider
	* Roslyn
	
## Choosing a collection

### Array

* Flat, sequential, statically sized 
* very fast to access to elements
* No per-element overhead
* Very efficient if the element type is a value type
* foundation for many other collection classes

Pros:
* accessing sequentially to element
* you know the size in advance
* you don't need to look up elements often

### List&lt;T&gt;

* Dynamic (resizable) array
	* Double its size with each expansion
	* For 100 000 000 insertions : [log<sub>2</sub> 100 000 000] = 27 expansions
	* 
* Each time, there is an insertion into the list:
	* a new array is created by allocation
	* all elements of the former array are copied to the new array
	* the GC has to clean the former array used by the List class.
	
* Extension not at the end of the list is very expensive
	* good only to append data
* No specialization lookup facility
* Still no per-element overhead (because based on an array)

### LinkedList&lt;T&gt;

* Doubly-linked list : each element knows its previous and next element
* Vey flexible collections for insertions/deletions : to insert/delete you only need to update the next and previous pointer.
* Still requires linear time (O(n)) for lookup
* Very big space overhead per element (each element has a next and previous pointers)

### Trees

* ```SortedDictionary<K,V>``` and ```SortedSet<T>``` are implemented with a balanced binary search tree
	* efficient lookup by key
	* Sorted by key
	* The key has to implement the interface ```IComparable<T>``` 
* All fundamental operations take _O_(log<sub>2</sub>(n)) times
	* For example, log<sub>2</sub>(100 000 000) is less than 27
	* Great for storing dynamic data that is queried often
* Big space overhead per element (several addtional fields)	
* Trade-off between the memory (which is big) and the efficiency

### Associative collection
 
 * ```Dictionary<K,V>``` and ```HashSet<T>``` use hashing to arrange the elements
 * Insertion, deletion and lookup work in _constant_ time _O_(1)
 	* GetHashCode must be well-distributed for this to happen
* Medium memory overhead
	* Combination of arrays and linked list
	* Smaller than trees in most cases
	

## Comparison of Built-in Collections

| Collection | Space overhead | Lookup | Insertion | Deletion | Special |
|:-----------|:---------------|:-------|:----------|:---------|:--------|
| Array      | None           | _O_(n) | N/A       | N/A      |         |
| LinkedList | Medium         | _O_(n) | _O_(1)    | _O_(1)   |         |
| SortedDictionnary SortedSet | Very big | _O_(log<sub>2</sub>(n)) | _O_(log<sub>2</sub>(n)) | Sorted |
| Hashet Dictionary | Medium | _O_(1) | _O_(1) | _O_(1) | |








