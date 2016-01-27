###The memory management

#### The stack and the heap

#####The stack

This memory contains all:
- value variable
- the local and paramter function of the methods


#####The heap
The heap contains:
- the reference variable
- the static variable (even if the static variable type is a value type)
- the array (even if the nested variable type is a value type)


#####The stack vs the heap

| The stack | The heap |
|:----------|:---------|
|Each allocation is pushed on the top of the stack (push/pop) | Each allocation is referenced by a variable |
|Very fast | Two variables could reference the same allocation |
|Made for the local variables, temporary storage of the function returns ...|The object life is greater than the function life that has created the object|
|Integrated support to the processor | Multithreading |
| No persistence (the objects are destroyed at the end of the method responsible of the creation | Suitable to allocate memory and pass it to another pointer |
|Fixed size (StackOverflowException) | Growing size (OutOfMemoryException) |



####The memory size

#####The default size of memory



#####How to set the memory size



####The stackoverflow and out of memory

#####The stackoverflow

This error raised when the stack memory capacity is not enough.

