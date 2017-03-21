using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Cairo;
using ProcGlyph;
using System.Collections.Generic;
using ProcGlyph.Strokes;
using ProcGlyph.Shapes;
using ProcGlyph.Flourishs;
using System.IO;

public class Program
{
    public const int WIDTH = 800;
    public const int LETTER_WIDTH = 75;
    public const int LETTER_HEIGHT = 75;

    static void Main()
    {
        int width = LETTER_WIDTH * 7;
        ImageSurface surface = new ImageSurface(Format.ARGB32, width, LETTER_HEIGHT*7);
        Context cr = new Context(surface);
        Alphabet alpha = new Alphabet();
        Console.WriteLine("Num Permutations: " + alpha.BaseNumPermutations);
        Console.WriteLine("Allowed Strokes: " + string.Join(", ", alpha.AllowedStrokes.Select(i => i.GetType().ToString())));
        Console.WriteLine("Allowed Permutations: " + string.Join(", ", alpha.AllowedPermutations.Select(i => i.GetType().ToString())));

        Grid grid = new ProcGlyph.Grid(75, 75, new GridPoint(1, 0), new GridPoint(2, 3));
        List<Letter> letters = new List<Letter>();
        LetterFactory letterFactory = new LetterFactory(alpha);

        int ndx = 0;
        int numAttempts = 0;
        int attemptsSinceLastIncrease = 0;
        while (letters.Count() < 25 && numAttempts < 200)
        {
            numAttempts++;
            if (attemptsSinceLastIncrease > 20)
            {
                // increase number of permutations to find new letters
                Console.WriteLine("Increasing number of permutations");
                alpha.BaseNumPermutations++;
                alpha.AddAdditionalPermutation();
                attemptsSinceLastIncrease = 0;
            }
            Letter newLetter = letterFactory.RandomLetter(grid);
            int mostSimilar = 500;
            foreach (Letter letter in letters)
            {
                int distance = Letter.Distance(newLetter, letter);
                if (distance < mostSimilar)
                {
                    mostSimilar = distance;
                }
            }
            if (mostSimilar > 2)
            {
                ndx++;
                Console.WriteLine($"Letter: {ndx} distance: {mostSimilar}");
                letters.Add(newLetter);
            } else
            {
                attemptsSinceLastIncrease++;
            }
        }

        double x = 0;
        double y = 0;

        foreach (Letter letter in letters)
        {
            cr.Save();
            cr.Translate(x, y);
            letter.Draw(cr);
            cr.Restore();
            x += grid.CanvasWidth;
            if (x > (width - grid.CanvasWidth))
            {
                x = 0;
                y += grid.CanvasHeight;
            }
        }

        bool imageSaved = false;
        int fileNumber = 1;
        while (!imageSaved)
        {
            string filename = $"alphabet{fileNumber}.png";
            if (!File.Exists(filename))
            {
                surface.WriteToPng(filename);
                imageSaved = true;
            }
            else
            {
                fileNumber++;
            }
        }
        cr.Dispose();
        surface.Dispose();
    }
}