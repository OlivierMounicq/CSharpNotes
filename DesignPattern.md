###Design Patterns

####Classification

|Classification | Name | Goal |
|:--------------|:-----|:-----|
| Creational Pattern | Abstract Factory | Provide interface |
|  | Factory |    |
|  | Builder |    |
|  | Lazy Initialization |    |
|  | Prototype |    |
|  | Singleton |  To ensure that a class has been instantiate only one time  |



##### Adapter

```cs
public interface IRectangle
{
    int GetSurface();
}

public class Rectangle : IRectangle
{
    private int Width;
    
    private int Length;
    
    public Rectangle(int width, int length)
    {
        this.Width = width;
        this.Length = length;
    }
    
    public int GetSurface()
    {
        return this.Length * this.Width;
    }
}

public class Square
{
    private int Side;
    
    public Square(int side)
    {
        this.Side = side;
    }
    
    public int GetArea()
    {
        return Side*Side;
    }
}

public class Adapter : IRectangle
{
    private Square adpatedObject;
    
    public Adapter(Square pAdaptedObject)
    {
        this.adpatedObject = pAdaptedObject;
    }
    
    public int GetSurface()
    {
        return this.adpatedObject.GetArea();
    }
}

var rectangle = new Rectangle(5,2);
Console.WriteLine(rectangle.GetSurface());

var square = new Square(10);
var adapter = new Adapter(square);
Console.WriteLine(adapter.GetSurface());

//Output
//10
//100
```
