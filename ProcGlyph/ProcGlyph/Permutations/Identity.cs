using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class Identity : Permutation
    {
        public List<GridPoint[]> Permute(List<GridPoint[]> points)
        {
            return points;
        }
    }
}
