using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public interface Permutation
    {
        List<GridPoint[]> Permute(List<GridPoint[]> points);
    }
}
