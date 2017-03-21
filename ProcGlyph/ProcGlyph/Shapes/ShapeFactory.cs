using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Shapes
{
    public class ShapeFactory
    {
        private Random random = new Random();
        private Alphabet alphabet;

        public ShapeFactory(Alphabet alpha)
        {
            alphabet = alpha;
        }
        
        public GridPoint[] RandomShape()
        {
            return alphabet.AllowedShapes[random.Next(alphabet.AllowedShapes.Count())];
        }
    }
}
