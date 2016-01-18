####1. Some defintions
- A __type__ defines the blueprint for a value
- __predefined types__ are the types specially supported by the compiler
- __Custom type__ is a complex type built from _primitive type_

####2 Type categories

There are 4 categories:
- value type
- reference type
- generic type parameter
- pointer type

#####2.1 Value type

The value type contains:
- the most built-in type (all numeric type, char, bool, DateTime),
- struct, 
- enum.

#####2.2 Reference type

The reference types contains:
- class
- array
- delegate
- interface
- string

####3 Default values

|Type | Default value |
|:----|:--------------|
| All reference type | null |
| All numeric and enum type | 0 |
| char type | '\0' |
| bool type | false |

####4 Conversion

#####4.1 Implicit conversion
- always succeed
- no information lost in the conversion

#####4.2 Explicit conversion
- the compiler cannot guarantee the explicit conversion will always succeed
- information may be lost during the conversion

#####4.3 Examples

```cs
int a = 12345;
long b = x; //implicit conversion
short c = (short)x; //explicit conversion
```

####5 Alias


| Alias   | Class            |
|:------- |:---------------- |
|object   | System.Object    |
|string   | System.String    |
|bool     | System.Boolean   |
|byte     | System.Byte      |
|sbyte    | System.SByte     |
|short    | System.Int16     |
|ushort   | System.UInt16    |
|int      | System.Int32     |
|uint     | System.UInt32    |
|long     | System.Int64     |
|ulong    | System.UInt64    |
|float    | System.Single    |
|double   | System.Double    |
|decimal  | System.Decimal   |
|char     | System.Char      |
