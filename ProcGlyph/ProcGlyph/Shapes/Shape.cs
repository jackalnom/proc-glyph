using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Shapes
{
    public interface Shape
    {
        List<GridPoint[]> All();
    }
}
