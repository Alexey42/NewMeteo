using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace NewMeteo
{
    public class Map
    {
        private Mat image;
        public Mat Image { 
            get { return image; }
            set { image = value; }
        }
        public string path;
        public double[,] values;

        public Map()
        {
            image = new Mat();
            values = new double[0, 0];
            path = "";
        }

        public Map(Map m)
        {
            image = m.Image.Clone();
            path = (string)m.path.Clone();
            values = (double[,])m.values.Clone();
        }

        public Map(Mat m, string p)
        {
            image = m;
            values = new double[m.Cols, m.Rows];
            path = p;
        }

        public Map(Mat m, string p, double[,] v)
        {
            image = m;
            values = v;
            path = p;
        }

        public void SetValues(IEnumerable<System.Windows.Point> points, double value)
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

        public BitmapSource GetBS()
        {
            var t = image.Type();
            return image.ToBitmapSource();
        }
    }
}
