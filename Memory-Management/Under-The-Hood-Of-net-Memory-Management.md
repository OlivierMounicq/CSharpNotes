#Under the hood of .NET memory management

##Chapter #1

Application = the code + data storing the state during the execution

###The heap

They are _4 sections of memory_  creating for the storage:
- __code heap__ stores the actual the native code instructions after they have been _Just In Time_ (JiTed)
- __Small Object Heap__ (SOH) : stores allocated objects <85 Ko 
- __Large Object Heap__ (LOH) : stores allocated objects > 85 Ko
- __Processe Heap__

Everything on the heap has an address.

###The stack / The execution process

####The stack properties

The purpose of the _stack_ is to keep track of a method's data from every other method call.

Each thread has its own thread.

####The method and the stack

The execution process is the following:
- When a method is called, it has its own cocooned environment where any data variables that it creates exist only for the lifetime of the call
- When a method calls another one, the local state of the calling method (variables, parameters) has to be remembered while the method to be called executes. 
- Once the called method finishes, the original state of the caller need to be restored teh it can continue executing

To keep track of everything, .net maintains a __stack__ data structure which it uses to track the state of an execution thread and all the method calls made.

####The stack using

- When a method is called, .net created a container (a stack frame) that contains all of the data necessary to complete the call, including parameters locally declared variables and addresses of the line of code to execute after the method finishes.
- For every method call made in a call tree, the stack containers are stacked on the top of the other
- When a method completes, its container is removed from the top of the stack and the excution returns to the next line of within the calling method
- the frame at the top of the stack is always the oe-ne used by the current executing method.

###Datatypes and their storages

####The datatypes stored in the _stack_

The stack stores the __Common Type System__ (CTS) ie the __value types__.

The value types are

| Byte | SByte | Int16 |
| Int32 | Int64 | UInt16 |
| UInt32 | UInt64 | Single |
| Double | Boolean | char |
| Decimal | IntPtr | UIntPtr |
| Structs | | 












