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
	public static bool operator==(Point2D a, Point2D b)...
	public static bool operator!=(Point2D a, Point2D b)...
}
```
You have to implement both operator (equal and not equal) otherwise the compiler will trigger an error.


### Add GetHashCode




### The code

#### First version: the worse performance

```cs



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

