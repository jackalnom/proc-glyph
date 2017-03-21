using ProcGlyph.Permutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Flourishs
{
    class DoubleDot : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> lines)
        {
            GridPoint[] line = lines.First();
            GridPoint midPoint = GridPoint.MidPoint(line);

            lines.Add(new GridPoint[] { new GridPoint(midPoint.X - 1, midPoint.Y - 1) });
            lines.Add(new GridPoint[] { new GridPoint(midPoint.X + 1, midPoint.Y - 1) });
            return lines;
        }
    }
}
