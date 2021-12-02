using HelixToolkit.Wpf;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace NewMeteo
{
    /// <summary>
    /// Логика взаимодействия для _3DViewWindow.xaml
    /// </summary>
    public partial class _3DViewWindow : Window
    {
        private Map map;
        private Model3DGroup MainModel3Dgroup = new Model3DGroup();
        private Dictionary<Point3D, int> PointDictionary = new Dictionary<Point3D, int>();
        private int xmin, xmax, dx, zmin, zmax, dz;
        private double texture_xscale, texture_zscale;

        public _3DViewWindow(Map _m)
        {
            InitializeComponent();
            map = _m;
            View(map.values, MainModel3Dgroup);

            viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
            ModelVisual3D model_visual = new ModelVisual3D();
            model_visual.Content = MainModel3Dgroup;
            viewPort3d.Children.Add(model_visual);
            //CreateModel();
        }

        private void SaveSTL(object sender, RoutedEventArgs e)
        {
            var exp = new StlExporter();
            VistaFolderBrowserDialog ofd = new VistaFolderBrowserDialog();
            ofd.RootFolder = Environment.SpecialFolder.Recent;
            if (ofd.ShowDialog() == true)
            {
                using (var stream = new FileStream(ofd.SelectedPath + "\\res.stl", FileMode.OpenOrCreate))
                    exp.Export(MainModel3Dgroup, stream);
            }
        }

        private void View(float[,] value, Model3DGroup model_group)
        {
            // Make a mesh to hold the surface.
            MeshGeometry3D mesh = new MeshGeometry3D();
            xmin = 0;
            xmax = value.GetUpperBound(0);
            dx = 1;
            zmin = 0;
            zmax = value.GetUpperBound(1);
            dz = 1;
            texture_xscale = xmax - xmin;
            texture_zscale = zmax - zmin;
            // Make the surface's points and triangles.
            float offset_x = xmax / 2f;
            float offset_z = zmax / 2f;
            for (int x = xmin; x <= xmax - dx; x += dx)
            {
                for (int z = zmin; z <= zmax - dz; z += dx)
                {
                    // Make points at the corners of the surface
                    // over (x, z) - (x + dx, z + dz).
                    Point3D p00 = new Point3D(x - offset_x, value[x, z], z - offset_z);
                    Point3D p10 = new Point3D(x - offset_x + dx, value[x + dx, z], z - offset_z);
                    Point3D p01 = new Point3D(x - offset_x, value[x, z + dz], z - offset_z + dz);
                    Point3D p11 = new Point3D(x - offset_x + dx, value[x + dx, z + dz], z - offset_z + dz);

                    AddTriangle(mesh, p00, p01, p11);
                    AddTriangle(mesh, p00, p11, p10);
                }
            }

            // Make the surface's material using an image brush.
            ImageBrush texture_brush = new ImageBrush();
            texture_brush.ImageSource = map.GetBI();
            DiffuseMaterial surface_material = new DiffuseMaterial(texture_brush);

            // Make the mesh's model.
            GeometryModel3D surface_model = new GeometryModel3D(mesh, surface_material);

            // Make the surface visible from both sides.
            surface_model.BackMaterial = surface_material;

            // Add the model to the model groups.
            model_group.Children.Add(surface_model);
        }

        private void AddTriangle(MeshGeometry3D mesh,
            Point3D point1, Point3D point2, Point3D point3)
        {
            // Get the points' indices.
            int index1 = AddPoint(mesh.Positions, mesh.TextureCoordinates, point1);
            int index2 = AddPoint(mesh.Positions, mesh.TextureCoordinates, point2);
            int index3 = AddPoint(mesh.Positions, mesh.TextureCoordinates, point3);

            // Create the triangle.
            mesh.TriangleIndices.Add(index1);
            mesh.TriangleIndices.Add(index2);
            mesh.TriangleIndices.Add(index3);
        }

        private int AddPoint(Point3DCollection points,
            PointCollection texture_coords, Point3D point)
        {
            if (PointDictionary.ContainsKey(point))
                return PointDictionary[point];

            // We didn't find the point. Create it.
            points.Add(point);
            PointDictionary.Add(point, points.Count - 1);

            // Set the point's texture coordinates.
            texture_coords.Add(
                new Point(
                    (point.X - xmin) * texture_xscale,
                    (point.Z - zmin) * texture_zscale));

            // Return the new point's index.
            return points.Count - 1;
        }

        private void CreateModel()
        {
            ModelVisual3D model = new ModelVisual3D();
            ModelImporter importer = new ModelImporter();
            model.Content = importer.Load("C:\\Users\\55000\\Downloads\\камера.stl");
            viewPort3d.Children.Add(model);
        }

    }
}
