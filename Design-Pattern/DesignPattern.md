###Design Patterns

####Classification

|Classification | Name | Goal |
|:--------------|:-----|:-----|
| Creational Pattern | Abstract Factory | Provide interface |
| Creational Pattern | Factory |    |
| Creational Pattern | Builder |    |
| Creational Pattern | Lazy Initialization |    |
| Creational Pattern | Prototype |    |
| Creational Pattern | Singleton |  To ensure that a class has been instantiate only one time  |
|                    |           |          |
| Structural Pattern | Adapter |   |
| Structural Pattern | Bridge |   |
| Structural Pattern | Composite |   |
| Structural Pattern | Decorator |   |
| Structural Pattern | Facade |   |
| Structural Pattern | Flyweight |   |
| Structural Pattern | Proxy |   |
| Structural Pattern | Adapter |   |
|                    |           |          |
| Behavioral Pattern | Chain of the reponsability |       |
| Behavioral Pattern | Command |     |
| Behavioral Pattern | Interpreter  |    |
| Behavioral Pattern | Iterator  |   |
| Behavioral Pattern | Mediator |       |
| Behavioral Pattern |  Memento |     |
| Behavioral Pattern | Observer |     |
| Behavioral Pattern | State |    |
| Behavioral Pattern | Strategy |     |
| Behavioral Pattern | Template method |    |
| Behavioral Pattern | Visitor |   |




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
