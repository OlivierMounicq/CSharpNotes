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
  
### Tools to inspect the IL

We can use the ILDASM application to modify the IL code: IL Roundtripping.
- Ships with Visual Studio and .NET framework SDK
- IL roundtripping with ILASM (textual language for IL)



