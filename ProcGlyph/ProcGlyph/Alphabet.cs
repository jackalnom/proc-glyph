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
    public class Alphabet
    {
        private static Stroke[] strokes = new Stroke[] { new Circle(), new JoinedArc(), new Line(), new Curved(), new Arc(), new RoundedIn(), new RoundedOut(), new Looped() };
        private static Permutation[] permutation = new Permutation[] { new Bridge(), new Mirror(), new CenterConnect(), new Identity(), new Segment(), new Inverse(), new Rotate90(), new ShrinkAndMirror(), new Merge(), new Dash(), new Dot(), new DoubleDot() };
        private static Shape[] shapes = new Shape[] { new Straight(), new Triad(), };

        public int BaseNumPermutations { get; set; }
        public Stroke[] AllowedStrokes { get; private set; }
        public List<Permutation> AllowedPermutations { get; private set; }
        public GridPoint[][] AllowedShapes { get; private set; }
        private Random random = new Random();

        public Alphabet()
        {
            BaseNumPermutations = random.Next(6) + 1;
            int numStrokes = random.Next(3) + 1;
            List<Stroke> strokeList = new List<Stroke>();
            while (strokeList.Count() < numStrokes)
            {
                Stroke randomStroke = strokes[random.Next(strokes.Count())];
                if (!strokeList.Contains(randomStroke))
                {
                    strokeList.Add(randomStroke);
                }
            }
            AllowedStrokes = strokeList.ToArray();
             
            AllowedPermutations = new List<Permutation>();
            while (AllowedPermutations.Count() < 4)
            {
                AddAdditionalPermutation();
            }

            List<GridPoint[]> shapeList = new List<GridPoint[]>();
            while (shapeList.Count() < 3)
            {
                Shape randomShapeTemplate = shapes[random.Next(shapes.Count())];
                GridPoint[] randomShape = randomShapeTemplate.All()[random.Next(randomShapeTemplate.All().Count)];
                if (!shapeList.Contains(randomShape))
                {
                    shapeList.Add(randomShape);
                }
            }
            AllowedShapes = shapeList.ToArray();
        }

        public void AddAdditionalPermutation()
        {
            Permutation randomPermutation = permutation[random.Next(permutation.Count())];
            if (!AllowedPermutations.Contains(randomPermutation))
            {
                AllowedPermutations.Add(randomPermutation);
            }
        }
    }
}
