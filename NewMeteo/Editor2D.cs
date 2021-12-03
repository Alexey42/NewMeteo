using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewMeteo
{
    public static class Editor2D
    {
        public static Stack<Map> History = new Stack<Map>();

        public static void DrawPoint(int xAbs, int xRel, int yAbs, int yRel, Canvas canvas_main, Mat CurrentImage)
        {
            var r = new Rectangle();
            r.IsHitTestVisible = false;
            r.Fill = Brushes.Red;
            r.Width = 2;
            r.Height = 2;
            r.RadiusX = 2;
            r.RadiusY = 2;
            canvas_main.Children.Add(r);
            Canvas.SetTop(r, yAbs);
            Canvas.SetLeft(r, xAbs);
            CurrentImage.DrawMarker(new OpenCvSharp.Point(xRel + 1, yRel + 3), new Scalar(0, 0, 255), MarkerTypes.Square, 1, 1);
        }

        public static List<System.Windows.Point> DrawBezier(List<System.Windows.Point> points, Canvas canvas_main, Mat CurrentImage)
        {
            List<List<Point>> p = new List<List<Point>>();
            List<System.Windows.Point> result = new List<System.Windows.Point>();
            p.Add(new List<Point>());

            for (float i = 0f; i < 1f; i += 0.01f)
            {
                var x = X(i, (float)points[0].X, (float)points[1].X, (float)points[2].X, (float)points[3].X);
                var y = Y(i, (float)points[0].Y, (float)points[1].Y, (float)points[2].Y, (float)points[3].Y);
                p[0].Add(new Point(x, y));
                result.Add(new System.Windows.Point(x, y));
            }
            CurrentImage.Polylines(p, false, new Scalar(0, 0, 255), 1);

            return result;
        }

        public static void DrawText(int xAbs, int xRel, int yAbs, int yRel, Canvas canvas_main, 
            Mat CurrentImage, string value)
        {
            if (xAbs >= 0 || yAbs >= 0)
            {
                var t = new Label();
                t.Content = value;
                t.Foreground = Brushes.Red;
                t.FontSize = 8;
                canvas_main.Children.Add(t);
                Canvas.SetTop(t, yAbs);
                Canvas.SetLeft(t, xAbs);
            }
            CurrentImage.PutText(value, new Point(xRel, yRel), HersheyFonts.HersheyComplex, 0.5, Scalar.Red);
        }

        public static float[,] SmoothSurface(float[,] values)
        {


            return values;
        }

        private static float X(float t, float x0, float x1, float x2, float x3)
        {
            return (float)(
                x0 * Math.Pow((1 - t), 3) +
                x1 * 3 * t * Math.Pow((1 - t), 2) +
                x2 * 3 * Math.Pow(t, 2) * (1 - t) +
                x3 * Math.Pow(t, 3)
            );
        }
        
        private static float Y(float t, float y0, float y1, float y2, float y3)
        {
            return (float)(
                y0 * Math.Pow((1 - t), 3) +
                y1 * 3 * t * Math.Pow((1 - t), 2) +
                y2 * 3 * Math.Pow(t, 2) * (1 - t) +
                y3 * Math.Pow(t, 3)
            );
        }
    }
}
