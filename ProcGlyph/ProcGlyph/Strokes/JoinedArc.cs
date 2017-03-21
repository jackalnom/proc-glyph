using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cairo;

namespace ProcGlyph.Strokes
{
    public class JoinedArc : Stroke
    {
        public JoinedArc()
        {

        }
      
        private class JoinedArcDraw : Drawable
        {
            private PointD center, leftArm, rightArm;
            private double width, height, startAngle, lineWidth;

            public JoinedArcDraw(PointD center, double width, double height, double startAngle, PointD leftArm, PointD rightArm, double lineWidth)
            {
                this.center = center;
                this.width = width;
                this.height = height;
                this.startAngle = startAngle;
                this.leftArm = leftArm;
                this.rightArm = rightArm;
                this.lineWidth = lineWidth;
            }

            public void Draw(Context gr)
            {
                gr.Save();   
                gr.Scale(width, height);
                gr.MoveTo(leftArm.X / width, leftArm.Y / height);
                gr.Arc(center.X / width, center.Y / height, 1, startAngle, startAngle+Math.PI);
                gr.LineTo(rightArm.X / width, rightArm.Y / height);   
                gr.Restore();

                gr.LineWidth = lineWidth;
                gr.Stroke();
            }
        }

        public int MinPoints()
        {
            return 2;
        }

        public int MaxPoints()
        {
            return 3;
        }

        public Drawable Draw(Grid grid, GridPoint[] points)
        {
            GridPoint leftArm;
            GridPoint point;
            GridPoint rightArm;

            if (points.Count() == 3)
            {
                leftArm = points[0];
                point = points[1];
                rightArm = points[2];
            } else
            {
                point = points[1];
                GridPoint dir = GridPoint.Direction(points[0], points[1]);
                leftArm = new GridPoint(points[0].X + dir.Y/2, points[0].Y + dir.X/2);
                rightArm = new GridPoint(points[0].X - dir.Y/2, points[0].Y - dir.X/2);
            }

            if (!leftArm.Valid() || !rightArm.Valid())
            {
                return null;
            }

            GridPoint betweenArms = GridPoint.MidPoint(leftArm, rightArm);
            int gridRadius = GridPoint.Distance(leftArm, rightArm) / 2;

            if (gridRadius == 0)
            {
                return null;
            }

            GridPoint directionToArms = new GridPoint(betweenArms.X - point.X, betweenArms.Y - point.Y).normalize(gridRadius);
            GridPoint arcCenter = new GridPoint(point.X + directionToArms.X, point.Y + directionToArms.Y);
            GridPoint arcX = new GridPoint(arcCenter.X + gridRadius, arcCenter.Y);
            GridPoint arcY = new GridPoint(arcCenter.X, arcCenter.Y + gridRadius);

            if (!arcCenter.Valid() || !arcX.Valid() || ! arcY.Valid())
            {
                return null;
            }

            PointD arcCenterVec = grid.TranslateGridPoint(arcCenter);
            PointD leftArmVec = grid.TranslateGridPoint(leftArm);
            PointD rightArmVec = grid.TranslateGridPoint(rightArm);

            double width = (grid.TranslateGridPoint(arcX).X - grid.TranslateGridPoint(arcCenter).X);
            double height = (grid.TranslateGridPoint(arcY).Y - grid.TranslateGridPoint(arcCenter).Y);
            double startAngle = (Math.Atan2(directionToArms.X, -directionToArms.Y));

            return new JoinedArcDraw(arcCenterVec, width, height, startAngle, leftArmVec, rightArmVec, grid.LetterLineWidth);
        }
    }
}
