using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio92
{
    public class Circle : Shape
    {
        public double Radius { get; set; }  
        public override double Area { get { return Radius*Radius*Math.PI; } }  
    }
}
