using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohdAndArt.Toolkit.Graphics
{
    public class WinformGraphics
    {
        private ViewportSpecifications _viewportSpecs;
        public ViewportSpecifications ViewportSpecs { get { return _viewportSpecs; } set { _viewportSpecs = value; } }

        public enum GridLineOrientation
        {
            Horizontal = 0,
            Vertical
        }

        public void DrawGrid(DrawingDetails details)
        {
            details.GridOrientation = GridOrientation.Horizontal;
            DrawLines(details);

            details.GridOrientation = GridOrientation.Vertical;
            DrawLines(details);
        }


        int DetermineMiddlePoint(int start, int end, int increment)
        {
            List<int> list = new List<int>();
            for (int i = start; i < end; i+=increment)
            {
                list.Add(i);
            }

            int length = list.Count;
            var temp = list.ToArray()[length / 2];
            return temp;
        }

        void DrawLines(DrawingDetails details)
        {
            var opacity = 20;

            int viewportHeight = _viewportSpecs.Height;
            int viewportHeightFactor = _viewportSpecs.CenterLine;
            int viewportWidth = _viewportSpecs.Width;

            //using (Pen p = new Pen(details.PenColor, details.PenWidth))
            using (Pen p = new Pen(Color.FromArgb(opacity, details.PenColor), details.PenWidth)) 
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                int incrementAmount = 0;
                if (details.GridOrientation == GridOrientation.Horizontal)
                {
                    incrementAmount = details.GridSpacingHorizontal;
                    int verticalHalfWayPoint = DetermineMiddlePoint(0, viewportHeight, incrementAmount);

                    for (int i = 0; i < viewportHeight; i += incrementAmount)
                    {
                        if (i == verticalHalfWayPoint) 
                        {
                            using (var p2 = new Pen(Color.FromArgb(100, Color.Green), 2))
                            {
                                details.Canvas.DrawLine(p2, 0, i, viewportWidth, i);
                            }
                        }
                        else
                        {
                            details.Canvas.DrawLine(p, 0, i, viewportWidth, i);
                        }
                    }
                }
                else if (details.GridOrientation == GridOrientation.Vertical)
                {
                    incrementAmount = details.GridSpacingHorizontal;
                    int horizontalHalfWayPoint = DetermineMiddlePoint(0, viewportWidth, incrementAmount);

                    for (int i = 0; i < viewportWidth; i += incrementAmount)
                    {
                        if (i == horizontalHalfWayPoint)
                        {
                            using (var p2 = new Pen(Color.FromArgb(100, Color.Green), 2))
                            {
                                details.Canvas.DrawLine(p2, i, 0, i, viewportHeight);
                            }
                        } 
                        else
                        {
                            details.Canvas.DrawLine(p, i, 0, i, viewportHeight);
                        }
                    }
                }

            }
        }
    }

    public class DrawingDetails
    {
        public System.Drawing.Graphics Canvas { get; set; }
        public System.Drawing.Color PenColor { get; set; }
        public GridOrientation GridOrientation { get; set; }
        public int PenWidth { get; set; }
        public int GridSpacingHorizontal { get; set; }
        public int GridSpacingVertical { get; set; }
    }

    public enum GridOrientation
    {
        Horizontal = 0,
        Vertical
    }


    public struct ViewportSpecifications
    {
        public bool Initialized { get; set; }

        public Point Origin;
        public Point Finish;

        public int Width
        {
            get
            {
                return Finish.X;
            }
        }

        public int Height
        {
            get
            {
                return Finish.Y;
            }
        }

        public int CenterLine
        {
            get
            {
                return Finish.Y / 2;
            }
        }
    }
}
