using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph.Strokes
{
    public class StrokeFactory
    {
        private Random random = new Random();
        private Alphabet alphabet;

        public StrokeFactory(Alphabet alpha)
        {
            alphabet = alpha;
        }

        public Stroke RandomStroke(int points)
        {
            IEnumerable<Stroke> minPointStrokes = alphabet.AllowedStrokes.Where(s => s.MinPoints() <= points && s.MaxPoints() >= points);
            if (minPointStrokes == null || minPointStrokes.Count() == 0)
            {
                return null;
            }
            Stroke[] minPointStrokeArray = minPointStrokes.ToArray();
            return minPointStrokeArray[random.Next(minPointStrokeArray.Count())];
        }

    }
}
