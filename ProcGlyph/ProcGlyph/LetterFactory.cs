using ProcGlyph.Flourishs;
using ProcGlyph.Permutations;
using ProcGlyph.Shapes;
using ProcGlyph.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph
{
    public class LetterFactory
    {
        ShapeFactory shapeFactory;
        StrokeFactory strokeFactory;
        PermutationFactory permutationFactory;

        private Random random = new Random();
        private Alphabet alphabet;

        public LetterFactory(Alphabet alpha)
        {
            alphabet = alpha;
            strokeFactory = new StrokeFactory(alpha);
            permutationFactory = new PermutationFactory(alpha);
            shapeFactory = new ShapeFactory(alpha);
        }

        private Letter attemptLetter(Grid grid)
        {
            List<GridPoint[]> pointList = new List<GridPoint[]>() { shapeFactory.RandomShape() };

            List<LetterPart> letterDraw = new List<LetterPart>();
            int numPermutations = alphabet.BaseNumPermutations + random.Next(3);

            for (int i = 0; i < numPermutations; i++)
            {
                pointList = permutationFactory.RandomPermutation().Permute(pointList);
            }

            //pointList = pointList.Take(3).ToList();
            int totalPoints = pointList.Sum(p => p.Count());
            if (totalPoints > 10)
            {
                return null;
            }
            foreach (GridPoint[] p in pointList)
            {
                GridPoint[] points;
                points = p.Take(10).ToArray();
                Stroke stroke = strokeFactory.RandomStroke(points.Count());
                if (stroke == null)
                {
                    return null;
                }
                letterDraw.Add(new LetterPart(points, stroke));
            }

            Letter letter = new Letter(grid, letterDraw);
            if (letter.Valid && letter.Render())
            {
                return letter;
            } else
            {
                return null;
            }
        }

        public Letter RandomLetter(Grid grid)
        {
            while (true) {
                Letter letter = attemptLetter(grid);
                if (letter != null)
                {
                    return letter;
                }
            }
        }
    }
}
