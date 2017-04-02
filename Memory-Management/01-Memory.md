# The memory management

## 1. Introduction

Application = the code + data storing the state during the execution

## 2. The heap

### 2.1 The type of heap

They are _4 sections of memory_  creating for the storage:
- __code heap__ stores the actual the native code instructions after they have been _Just In Time_ (JiTed)
- __Small Object Heap__ (SOH) : stores allocated objects <85 Ko 
- __Large Object Heap__ (LOH) : stores allocated objects > 85 Ko
- __Processe Heap__

Everything on the heap has an address.




## 3. The stack

### 3.1 The stack properties

The purpose of the _stack_ is to keep track of a method's data from every other method call.

Each thread has its own thread.

### 3.1.1 The method execution and the stack

The execution process is the following:
- When a method is called, it has its own cocooned environment where any data variables that it creates exist only for the lifetime of the call
- When a method calls another one, the local state of the calling method (variables, parameters) has to be remembered while the method to be called executes. 
- Once the called method finishes, the original state of the caller need to be restored teh it can continue executing

To keep track of everything, .net maintains a __stack__ data structure which it uses to track the state of an execution thread and all the method calls made.

### 3.1.2 The stack using

- When a method is called, .net created a container (a stack frame) that contains all of the data necessary to complete the call, including parameters locally declared variables and addresses of the line of code to execute after the method finishes.
- For every method call made in a call tree, the stack containers are stacked on the top of the other
- When a method completes, its container is removed from the top of the stack and the excution returns to the next line of within the calling method
- the frame at the top of the stack is always the oe-ne used by the current executing method.

## 4. Datatypes and their storages

### 4.1 The datatypes stored in the _stack_

The stack stores the __Common Type System__ (CTS) ie the __value types__.

The value types are

|Value type | Value type | Value type |
|:-----|:------|:------|
| Byte | SByte | Int16 |
| Int32 | Int64 | UInt16 |
| UInt32 | UInt64 | Single |
| Double | Boolean | char |
| Decimal | IntPtr | UIntPtr |
| Structs | | 

### 4.2 The datatypes stored in the _heap_

The _reference types_ stored on the heap (LOH or SOH) are:
- classes
- interfaces
- delegate
- strings
- instance of object
- the static variable (even if the static variable type is a value type)
- the array (even if the nested variable type is a value type)


## 5. The stack vs the heap

| The stack | The heap |
|:----------|:---------|
|Each allocation is pushed on the top of the stack (push/pop) | Each allocation is referenced by a variable |
|Very fast | Two variables could reference the same allocation |
|Made for the local variables, temporary storage of the function returns ...|The object life is greater than the function life that has created the object|
|Integrated support to the processor | Multithreading |
| No persistence (the objects are destroyed at the end of the method responsible of the creation | Suitable to allocate memory and pass it to another pointer |
|Fixed size (StackOverflowException) | Growing size (OutOfMemoryException) |


## 6. The object

The double array is instantiated on the LOH (not on the SOH).

































