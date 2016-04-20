##Garbage Collector

###Some data

####Heap memory size

The application’s memory is limited by the process’s virtual address space. 
In a 32-bit process, you can allocate close to 1.5 gigabytes and in a 64-bit process you can allocate close to 8 terabytes.

|Process | Memory size |
|:-------|:------------|
|32 bit process | 1.5 gigabytes |
|64-bit process | 8 terabytes   |


####Garbage collector generations

The GC divides the memory into three tiers _memory space generation_ :
- the managed heap __SOH__ (Small Object Heap) : generations G0 and G1
- the managed heap __LOH__ (Large Object Heap) : the generation G2


