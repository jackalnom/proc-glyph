using Cairo;
using ProcGlyph.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProcGlyph
{
    public class Letter : Drawable
    {
        private Grid grid;
        private List<LetterPart> parts;
        private List<Drawable> strokes = new List<Drawable>();
        public string Hash { get; private set; }
        private Rectangle boundingBox;
        public bool Valid { get; private set;  }
        private double xCenter;
        private double yCenter;

        private byte[] identity = new byte[56];

        public Letter(Grid grid, List<LetterPart> parts)
        {
            this.grid = grid;
            this.parts = parts;
            Valid = true;
            determineBounding();
            calculateIdentity();
        }

        private bool renderGrid(Grid g, List<Drawable> drawables)
        {
            foreach (LetterPart part in parts)
            {
                Drawable draw = part.Render(g);
                if (draw == null)
                {
                    return false;
                }
                drawables.Add(draw);
            }

            return true;
        }

        public bool Render()
        {
            return renderGrid(grid, strokes);
        }

        public void Draw(Cairo.Context gr)
        {
            gr.SetSourceRGBA(0, 0, 1, .8);
            gr.Rectangle(new Rectangle(0, 0, grid.CanvasWidth, grid.CanvasHeight));
            gr.Stroke();

            gr.Save();
            gr.Translate(xCenter, yCenter);
            
            gr.SetSourceRGBA(1, 1, 1, 1);

            drawLetter(gr);
            gr.Restore();
        }

        private void drawLetter(Context gr)
        {
            gr.Antialias = Antialias.Subpixel;
            gr.LineJoin = LineJoin.Round;
            gr.LineCap = LineCap.Round;

            foreach (Drawable stroke in strokes)
            {
                stroke.Draw(gr);
            }
        }

        private void determineBounding()
        {
            int gridWidth = (int)grid.CanvasWidth;
            int gridHeight = (int)grid.CanvasHeight;
            int width = (int)gridWidth+20;
            int height = (int)gridHeight+20;
            int xOffset = 10;
            int yOffset = 10;

            List<Drawable> identityStrokes = new List<Drawable>();
            Grid identityGrid = new Grid(gridWidth, gridHeight, grid.Offset, grid.Gravity);
            identityGrid.DrawGridLines = false;
            if (!renderGrid(grid, identityStrokes))
            {
                Hash = "bad";
                return;
            }

            ImageSurface surface = new ImageSurface(Format.A8, width, height);
            Context cr = new Context(surface);
            cr.SetSourceRGBA(0, 0, 0, 1);
            cr.Antialias = Antialias.None;

            cr.Translate(xOffset, yOffset);
            foreach (Drawable stroke in identityStrokes)
            {
                stroke.Draw(cr);
            }

            surface.Flush();
            int minX = width;
            int minY = height;
            int maxX = 0;
            int maxY = 0;
            for (int i = 0; i < surface.Data.Count(); i++)
            {
                byte color = surface.Data[i];

                if (color != 0)
                {
                    int x = (i % surface.Stride);
                    int y = i / surface.Stride;
                    if (x > maxX)
                    {
                        maxX = x;
                    }
                    if (x < minX)
                    {
                        minX = x;
                    }
                    if (y > maxY)
                    {
                        maxY = y;
                    }
                    if (y < minY)
                    {
                        minY = y;
                    }
                }
            }

            if (maxX < minX || maxY < minY || minX < xOffset || minY < yOffset || maxX > (width - xOffset) || maxY > (height - yOffset))
            {
                Valid = false;
            }

            boundingBox = new Rectangle((minX - xOffset)*grid.CanvasWidth/gridWidth, (minY - yOffset)*grid.CanvasHeight/gridHeight, (maxX - minX)*grid.CanvasWidth/gridWidth, (maxY - minY)*grid.CanvasHeight/gridHeight);
            if (boundingBox.Width < 60 && boundingBox.Height < 60)
            {
                Valid = false;
            }
            xCenter = (grid.CanvasWidth / 2) - (boundingBox.X + boundingBox.Width / 2);
            yCenter = (grid.CanvasHeight / 2) - (boundingBox.Y + boundingBox.Height / 2);
            surface.Dispose();
            cr.Dispose();
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public static int Distance(Letter a, Letter b)
        {
            int distance = 0;

            for (int i = 0; i < a.identity.Count(); i++)
            {
                if (a.identity[i] != b.identity[i])
                {
                    distance++;
                }
            }

            return distance;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Letter l = obj as Letter;
            if ((Object)l == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Hash == l.Hash);
        }

        private void calculateIdentity()
        {
            List<Drawable> identityStrokes = new List<Drawable>();
            Grid identityGrid = new Grid(7, 7, new GridPoint(0, 0), new GridPoint(3, 3));
            identityGrid.DrawGridLines = false;
            if (!renderGrid(identityGrid, identityStrokes))
            {
                Hash = "bad";
                Valid = false;
                return;
            }

            ImageSurface surface = new ImageSurface(Format.A8, 7, 7);
            Context cr = new Context(surface);

            cr.Translate(xCenter*7/grid.CanvasWidth, yCenter*7/grid.CanvasHeight);
            cr.SetSourceRGBA(0, 0, 0, 1);
            cr.Antialias = Antialias.None;

            foreach (Drawable stroke in identityStrokes)
            {
                stroke.Draw(cr);
            }

            surface.Flush();
            surface.Data.CopyTo(identity, 0);

            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                Hash = Convert.ToBase64String(sha1.ComputeHash(surface.Data));
            }

            surface.Dispose();
            cr.Dispose();
        }
    }
}
