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
        public int width;
        public int height;

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
            width = image.Width;
            height = image.Height;
        }

        public Map(Mat m, string p)
        {
            image = m;
            values = new double[m.Cols, m.Rows];
            path = p;
            width = image.Width;
            height = image.Height;
        }

        public Map(Mat m, string p, double[,] v)
        {
            image = m;
            values = v;
            path = p;
            width = image.Width;
            height = image.Height;
        }

        /**
		 * <summary>
		 * Converts mat to BitmapImage
		 * </summary>
		 */
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

        /**
		 * <summary>
		 * Converts mat to BitmapSource
		 * </summary>
		 */
        public BitmapSource GetBS()
        {
            return image.ToBitmapSource();
        }

        /**
		 * <summary>
		 * Method that put height data on map
		 * </summary>
		 *
		 * <param name="points">Points of new surface </param>
		 * <param name="value">New height</param>
		 * <param name="rangeCoef">Defines area of new surface</param>
		 * <param name="decreaseCoef">Defines how fast decreases height around</param>
		 */
        public void SetValues(IEnumerable<System.Windows.Point> points, double value, 
            double rangeCoef, double decreaseCoef)
        {
            foreach (var p in points)
            {
                int x = (int)p.X;
                int y = (int)p.Y;
                int range = (int)(value * rangeCoef);
                values[x, y] = value;

                for (int i = -range; i <= range; i++)
                    for (int j = -range; j <= range; j++)
                        if (x + i < width && x + i >= 0 && y + j < height && y + j >= 0)
                        {
                            var k = Math.Sqrt(i * i + j * j) / decreaseCoef;
                            if (i * j != 0 && value - k > values[x + i, y + j])
                            {
                                values[x + i, y + j] = value - k;
                            }
                        }
            }
        }

        
    }
}
