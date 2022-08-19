using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;
namespace Packt.Shared;
public class Singer
{
    // virtual allows this method to be overridden
    public virtual void Sing() // herança polimórfica
    // public void Sing() //~herança não polimórfica
    {
        WriteLine("Singing...");
    }
}
public class LadyGaga : Singer
{
    // sealed prevents overriding the method in subclasses
    public sealed override void Sing() // herança polimórfica
     //    public new void Sing() // herança não polimórfica
    {
        WriteLine("Singing with style...");
    }
}