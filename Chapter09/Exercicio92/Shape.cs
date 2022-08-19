using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; // XmlSerializer

namespace Exercicio92
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public  abstract class Shape
    {
        public string Colour { get; set; }
        public abstract double Area { get; }

    }
}
