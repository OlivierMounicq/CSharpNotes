# Inspecting the runtime state

### Inspecting the runtime state
- State of your program (C#)
- Underlying data structure of the runtime (CLR)

### Tools to inspect the runtime state

There are different tools to inspect the runtime state:

##### VS debugger

- stepping through source, setting breakpoints
- Controlling exception behavior
- Expression evaluator (Watch, Immediate)

##### Native code debugger

In order to get morer details about the runtime staten you could also use the __Native Code Debugger__.

- Debugging Tools for Windows download
- WinDbg.exe (with a GUI) and cdb.exe (a console application)
- SOS (Son Of Strike), PSSCOR4 (Product Service Support), SOSEX, CLRMD extension extensions

### dbg.exe

Some commands:
- !help : to get all commands
- 

### CDB.EXE

```bat
C:\my projects\Hello World\cdb.exe hello.exe
```
