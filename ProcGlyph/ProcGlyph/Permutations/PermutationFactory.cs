using ProcGlyph.Flourishs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Permutations
{
    public class PermutationFactory
    {
        private Random random = new Random();
        private Alphabet alphabet;

        public PermutationFactory(Alphabet alpha)
        {
            alphabet = alpha;
        }

        public Permutation RandomPermutation()
        {
            return alphabet.AllowedPermutations[random.Next(alphabet.AllowedPermutations.Count())];
        }

    }
}
