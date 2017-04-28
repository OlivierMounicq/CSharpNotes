# Dev Tools

### Compiler

csc.exe 
al.exe : assembly link  

Input:
  - code files (.cs)
  - reference to assemblies (.exe, .ddl)
  - compiler flags (/target, /optimize, /debug)
  
Output:
  - Assemblies (.exe, .ddl)
  - .Net modules (.netmodules) : not used anymore
  - Windows Runtime modules (.winmd)
  
C# > = 5.0 & .net >= 4.5 : the compiler suports the creating Windows Runtime modules for Windows Runtime applications
(XAML applications for Windows 8 and beyond)  

csc.exe /nologo hello.cs
/nologo : suppress copyright output header  

### Intermediate Language

  - Virtual Machine language of the CLR
  - Target language of C#, VB.net, ...
  - JIT compiled into native code
  
###  ILDASM application

ILDASM is a tools to inspect the IL.

We can use the ILDASM application to modify the IL code: IL Roundtripping.
- Ships with Visual Studio and .NET framework SDK
- IL roundtripping with ILASM (textual language for IL)


##### How to use ILDASM application

```bat
C:\my projects\Hello World\ildasm.exe hello.exe
```

##### Emit the IL code into an output file

```bat
C:\my projects\Hello World\ildasm.exe /out=hello.il hello.exe
C:\my projects\Hello World\notepad hello.il
```

##### Compile the IL code

You could modify the IL code file and generate a new executable file (.exe):

```bat
C:\my projects\Hello World\ilasm.exe /exe hello.il
```


### .NET Reflector
- Built by _Red Gate_
- Decompiler of IL into C#, VB.net, ...

### ILSpy
- Open source alternative of .NET Reflector




