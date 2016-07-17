#Under the hood of .NET memory management

##Chapter #1

Application = the code + data storing the state during the execution

They are _4 sections of memory_  creating for the storage:
- __code heap__ stores the actual the native code instructions after they have been _Just In Time_ (JiTed)
- __Small Object Heap__ (SOH) : stores allocated objects <85 Ko 
- __Large Object Heap__ (LOH) : stores allocated objects > 85 Ko
- __Processe Heap__

