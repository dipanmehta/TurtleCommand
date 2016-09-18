using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleCommand.Model
{
    public class Turtle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public FaceEnum? CurrentFace { get; set; }
    }
}
