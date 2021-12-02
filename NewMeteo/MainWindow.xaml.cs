using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace NewMeteo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        Map CurrentMap;
        public bool isAuth = false;
        bool DrawingBezier = false;
        bool DrawingPoint = false;
        bool MakingWrapper = false;
        int[] WrapedArea;
        System.Windows.Point MouseStart;
        bool Cropping = false;
        int Count = 0;
        List<System.Windows.Point> FigurePoints;

        public MainWindow()
        {
            this.Loaded += new RoutedEventHandler(AuthorizeUser);
            InitializeComponent();
            WrapedArea = new int[4];
            FigurePoints = new List<System.Windows.Point>();
            CurrentMap = new Map();
        }

        private void AuthorizeUser(object sender, RoutedEventArgs e)
        {
            Authorisation auth = new Authorisation();
            auth.ShowDialog();
            if (auth.DialogResult == true)
                isAuth = true;
        }

        private void FindMap(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenMap(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                CurrentMap = new Map(new Mat(ofd.FileName), ofd.FileName);
                img.Source = CurrentMap.image.ToBitmapSource();
            }
        }

        private void SaveMap(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                Cv2.ImWrite(ofd.FileName, CurrentMap.image);
            }
        }

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            int xRelative = (int)e.GetPosition(element).X;
            int yRelative = (int)e.GetPosition(element).Y;
            int xAbsolute = (int)e.GetPosition(canvas_main).X;
            int yAbsolute = (int)e.GetPosition(canvas_main).Y;
            var p = new System.Windows.Point(xRelative, yRelative);
            MouseStart = p;

            if (MakingWrapper)
            {
                WrapedArea[0] = xRelative;
                WrapedArea[1] = yRelative;
                element.CaptureMouse();

                var rect = (UIElement)WrapRectangle;
                var tt = (TranslateTransform)((TransformGroup)rect.RenderTransform)
                    .Children.First(tr => tr is TranslateTransform);
                tt.X = xRelative;
                tt.Y = yRelative;
            }
            if (DrawingBezier)
            {
                if (Count < 4)
                {
                    Count++;
                    FigurePoints.Add(p);
                    figurePolyline.Points.Add(new System.Windows.Point(xAbsolute, yAbsolute));
                    figureLine.X1 = xAbsolute;
                    figureLine.Y1 = yAbsolute;
                    figureLine.X2 = xAbsolute;
                    figureLine.Y2 = yAbsolute;
                    figurePolyline.Visibility = Visibility.Visible;
                    figureLine.Visibility = Visibility.Visible;
                    if (Count == 4)
                    {
                        FinishDrawingBezier(null, e);
                    }
                }
            }
            if (DrawingPoint)
            {
                element.CaptureMouse();
                FigurePoints.Add(p);
                Editor2D.DrawPoint(xAbsolute, xRelative, yAbsolute, yRelative, canvas_main, CurrentMap.image);
            }
        }

        private void img_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            int x = (int)e.GetPosition(element).X;
            int y = (int)e.GetPosition(element).Y;
            if (MakingWrapper)
            {
                zoom_border.isActive = true;
                MakingWrapper = false;
                WrapedArea[2] = x;
                WrapedArea[3] = y;
                element.ReleaseMouseCapture();
                WrapRectangle.Width = 0;
                WrapRectangle.Height = 0;

                if (WrapedArea[0] > WrapedArea[2])
                {
                    var t = WrapedArea[0];
                    WrapedArea[0] = WrapedArea[2];
                    WrapedArea[2] = t;
                }
                if (WrapedArea[1] > WrapedArea[3])
                {
                    var t = WrapedArea[1];
                    WrapedArea[1] = WrapedArea[3];
                    WrapedArea[3] = t;
                }

                int wrapedX = WrapedArea[2] - WrapedArea[0];
                int wrapedY = WrapedArea[3] - WrapedArea[1];
                int wrapedSize = wrapedX * wrapedY;
                if (wrapedSize == 0)
                    return;

                Mat part = CurrentMap.image.SubMat(WrapedArea[1], WrapedArea[3], WrapedArea[0], WrapedArea[2]);

                if (Cropping)
                {
                    CurrentMap.image = part;
                    img.Source = part.ToBitmapSource();
                    img.UpdateLayout();
                    Cropping = false;
                }
            }
            if (DrawingPoint)
            {
                element.ReleaseMouseCapture();
            }
        }

        private void img_MouseMove(object sender, MouseEventArgs e)
        {
            UIElement element = (UIElement)sender;

            int xRelative = (int)e.GetPosition(element).X;
            int yRelative = (int)e.GetPosition(element).Y;
            int xAbsolute = (int)e.GetPosition(canvas_main).X;
            int yAbsolute = (int)e.GetPosition(canvas_main).Y;

            if (MakingWrapper && element.IsMouseCaptured)
            {
                var st = (ScaleTransform)((TransformGroup)WrapRectangle.RenderTransform)
                    .Children.First(tr => tr is ScaleTransform);
                if (yAbsolute - MouseStart.Y < 0 && st.ScaleY > 0 || yAbsolute - MouseStart.Y > 0 && st.ScaleY < 0)
                    st.ScaleY *= -1;
                if (xAbsolute - MouseStart.X < 0 && st.ScaleX > 0 || xAbsolute - MouseStart.X > 0 && st.ScaleX < 0)
                    st.ScaleX *= -1;
                WrapRectangle.Width = Math.Abs(xAbsolute - MouseStart.X);
                WrapRectangle.Height = Math.Abs(yAbsolute - MouseStart.Y);
            }
            if (DrawingPoint && element.IsMouseCaptured)
            {
                FigurePoints.Add(new System.Windows.Point(xRelative, yRelative));
                Editor2D.DrawPoint(xAbsolute, xRelative, yAbsolute, yRelative, canvas_main, CurrentMap.image);
            }
            if (DrawingBezier && Count > 0)
            {
                figureLine.X2 = xAbsolute;
                figureLine.Y2 = yAbsolute;
            }
        }

        private void StartDrawingPoint(object sender, RoutedEventArgs e)
        {
            if (DrawingPoint)
            {
                FinishDrawingPoint(null, e);
            }
            else
            {
                Editor2D.History.Push(new Map(CurrentMap));
                zoom_border.isActive = false;
                DrawingPoint = true;
            }
        }

        private void FinishDrawingPoint(object sender, RoutedEventArgs e)
        {
            for (int i = canvas_main.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvas_main.Children[i];
                if (Child is Rectangle)
                    canvas_main.Children.Remove(Child);
            }
            img.Source = CurrentMap.image.ToBitmapSource();
            img.UpdateLayout();

            float value = ConfirmHeight();
            if (!float.IsNaN(value))
            {
                CurrentMap.SetValues(FigurePoints, value);
            }
            else
            {
                CancelAction();
            }

            DrawingPoint = false;
            zoom_border.isActive = true;
        }

        private void StartDrawingBezier(object sender, RoutedEventArgs e)
        {
            if (DrawingBezier)
            {
                FinishDrawingBezier(null, e);
            }
            else
            {
                Editor2D.History.Push(new Map(CurrentMap));
                zoom_border.isActive = false;
                DrawingBezier = true;
            }
        }

        private void FinishDrawingBezier(object sender, RoutedEventArgs e)
        {
            figurePolyline.Points.Clear();

            if (Count == 4)
            {
                FigurePoints = Editor2D.DrawBezier(FigurePoints, canvas_main, CurrentMap.image);
                img.Source = CurrentMap.image.ToBitmapSource();
                img.UpdateLayout();
                float value = ConfirmHeight();

                if (!float.IsNaN(value))
                {
                    CurrentMap.SetValues(FigurePoints, value);
                }
                else
                {
                    CancelAction();
                }
            }
            Count = 0;
            FigurePoints.Clear();

            DrawingBezier = false;
            zoom_border.isActive = true;
        }

        private float ConfirmHeight()
        {
            ConfirmHeightWindow dialog = new ConfirmHeightWindow();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                return dialog.result;
            }
            else
                return float.NaN;
        }

        private void CropImage(object sender, RoutedEventArgs e)
        {
            if (MakingWrapper)
            {
                zoom_border.isActive = true;
                MakingWrapper = false;
                WrapRectangle.Width = 0;
                WrapRectangle.Height = 0;
            }
            else
            {
                zoom_border.isActive = false;
                MakingWrapper = true;
                Cropping = true;
            }
        }

        private void Show3D(object sender, RoutedEventArgs e)
        {
            _3DViewWindow window = new _3DViewWindow(CurrentMap);
            window.Show();
        }

        private void CancelAction()
        {
            var t = Editor2D.History.Pop();
            CurrentMap = t;
            img.Source = CurrentMap.image.ToBitmapSource();
            img.UpdateLayout();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Z:
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                        CancelAction();
                    break;

            }
        }
    }
}
