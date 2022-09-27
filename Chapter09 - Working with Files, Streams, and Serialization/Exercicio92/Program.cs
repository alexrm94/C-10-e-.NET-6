using Exercicio92; // Circle, Rectangle, Shape
using System.Xml.Serialization; // XmlSerializer
using static System.Console;
using static System.Environment;
using static System.IO.Path;

// create a list of Shapes to serialize
List<Shape> listOfShapes = new()
{
new Circle { Colour = "Red", Radius = 2.5 },
new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
new Circle { Colour = "Green", Radius = 8.0 },
new Circle { Colour = "Purple", Radius = 12.3 },
new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
};

//criar o objeto que formatará a List of Shapes como XML
XmlSerializer serializerXml = new(listOfShapes.GetType());

//criar o arquivo que será escrito
string path = Combine(CurrentDirectory, "listofshapes.xml");
using (FileStream fileXml = File.Create(path))
{
    serializerXml.Serialize(fileXml, listOfShapes);
}

List<Shape>? loadedShapesXml = null;

using (FileStream fileXml = File.Open(path, FileMode.Open))
{
    // deserialize and cast the object graph into a List of Person
    loadedShapesXml = serializerXml.Deserialize(fileXml) as List<Shape>;
}
if(loadedShapesXml == null)
{
    WriteLine($"{nameof(loadedShapesXml)} is empty.");
}

else
{ 
        foreach (Shape item in loadedShapesXml)
        {
            WriteLine("{0} is {1} and has an area of {2:N2}",
            item.GetType().Name, item.Colour, item.Area);
        }
 }
WriteLine(listOfShapes.GetType());