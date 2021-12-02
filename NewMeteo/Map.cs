using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenCvSharp;

namespace NewMeteo
{
    public class Map
    {
        public Mat image;
        public string path;
        public float[,] values;

        public Map()
        {
            image = new Mat();
            values = new float[0, 0];
            path = "";
        }

        public Map(Map m)
        {
            image = m.image.Clone();
            path = (string)m.path.Clone();
            values = (float[,])m.values.Clone();
        }

        public Map(Mat m, string p)
        {
            image = m;
            values = new float[m.Cols, m.Rows];
            path = p;
        }

        public Map(Mat m, string p, float[,] v)
        {
            image = m;
            values = v;
            path = p;
        }

        public void SetValues(IEnumerable<System.Windows.Point> points, float value)
        {
            foreach (var p in points)
            {
                values[(int)p.X, (int)p.Y] = value;
            }
        }

        public BitmapImage GetBI()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            image.UriSource = new Uri(path);
            image.EndInit();
            return image;
        }

    }
}
