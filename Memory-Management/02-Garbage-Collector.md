## Garbage Collector

### Some data

#### Heap memory size

The application’s memory is limited by the process’s virtual address space. 
In a 32-bit process, you can allocate close to 1.5 gigabytes and in a 64-bit process you can allocate close to 8 terabytes.

|Process | Memory size |
|:-------|:------------|
|32 bit process | 1.5 gigabytes |
|64-bit process | 8 terabytes   |


#### Garbage collector generations

The GC divides the memory into three tiers _memory space generation_ :
- the managed heap __SOH__ (Small Object Heap) : generations G0 and G1
- the managed heap __LOH__ (Large Object Heap) : the generation G2

#### Garbage collector triggering

There are different thresholds which are triggering the garbage collector:
- Gen0 hits ~ 256 K
- Gen1 hits ~ 2 MB (at this point, the GC collects Gen0 and Gen1)
- Gen2 hits ~ 10 MB (at this point, the GC collects Gen0, Gen1 and Gen2)
- system memory is low

When the Gen2 collection runs:
- the entire SOH is compacted
- the LOH is collected.

#### GC & SOH

When the GC runs on the SOH, all rooted objects are copied to compact the memory space. The object copy takes time.

#### GC & LOH

To copy large object takes time, therefore the GC won't copy to compact the lOH's memory space.





