using ProcGlyph.Permutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Flourishs
{
    class Dash : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> lines)
        {
            GridPoint[] line = lines.First();
            GridPoint midPoint = GridPoint.MidPoint(line[0], line[1]);
            
            // Calculate normal
            int xDir = (line[1].Y - line[0].Y);
            int yDir = (line[1].X - line[0].X);
            int magnitude = (int)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (magnitude == 0)
            {
                return lines;
            }
            xDir = xDir / magnitude + 1;
            yDir = yDir / magnitude;
            GridPoint a = new GridPoint(midPoint.X + xDir, midPoint.Y - yDir);
            GridPoint b = new GridPoint(midPoint.X - xDir, midPoint.Y + yDir);
            if (!a.Valid() || !b.Valid())
            {
                return lines;
            }
            lines.Add(new GridPoint[] { a, b });
            return lines;
        }
    }
}
