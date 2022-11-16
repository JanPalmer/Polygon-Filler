using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2_PolygonFiller
{
    // 12.11.2022 - Scanline
    // 14.11.2022 - Triangulacja - wektory normalne

    public partial class Form1 : Form
    {
        public const float angleStep = 0.1f;
        static public int ticks = 0;

        public Bitmap bitmap;
        public ModelVisualizer visualizer;
        public string s = "";

        public Form1()
        {
            InitializeComponent();
            timerAnimation = new Timer();
            bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            canvas.Image = bitmap;
            //string path = "..\\..\\..\\Objects\\proj2_sfera.obj";
            string path = "..\\..\\..\\Objects\\Sphere.obj";
            //string path = "..\\..\\..\\Objects\\Pyramid.obj";

            //Mesh mesh = MeshCreator.CreateMeshFromObj("D:\\Studia-2022-2023-sem5\\Grafika_Komputerowa_1\\PRO2\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Pyramid.obj");            
            Mesh mesh = MeshCreator.CreateMeshFromObj(path);
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\proj2_sfera.obj");
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Pyramid.obj");
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Rhombus.obj");

            if (mesh == null) Form1.ActiveForm.Close();

            for (int i = 0; i < mesh.vertices.Count; i++)
            {
                //s += "V." + i + " " + mesh.vertices[i].x.ToString() + ' ' + mesh.vertices[i].y.ToString() + ' ' + mesh.vertices[i].z.ToString() + '\n';
                //s += "V." + i + " " + mesh.normalVectors[i].x.ToString() + ' ' + mesh.normalVectors[i].y.ToString() + ' ' + mesh.normalVectors[i].z.ToString() + '\n';
            }

            visualizer = new ModelVisualizer(mesh);
            visualizer.FitModelOnCanvas(canvas);
            //for (int i = 0; i < mesh.vertices.Count; i++)
            //{
            //    s += "V." + i + " " + mesh.vertices[i].cast.X.ToString() + ' ' + mesh.vertices[i].cast.Y.ToString() + '\n';
            //}

            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            //visualizer.DrawFrame(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
            timerAnimation.Start();

            //s += visualizer.scale + '\n';
            //s += "Casts:\n";
            //for (int i = 0; i < mesh.vertices.Count; i++)
            //{
            //    s += "V." + i + " " + mesh.vertices[i].cast.X.ToString() + ' ' + mesh.vertices[i].cast.Y.ToString() + '\n';
            //}
            //label1.Text = s + "\nNill";
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            visualizer.DrawPolygon(e.Graphics);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            visualizer.FitModelOnCanvas(canvas);
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            //Form1.ActiveForm.Text = $"Ticks: {++ticks}, angle: {visualizer.angle}";
            if (Form1.ActiveForm == null) return;
            //visualizer.angle += angleStep * timerAnimation.Interval / 1000.0f;
            //if (visualizer.angle > Math.PI / 4) visualizer.angle = 0;
            visualizer.RotateLight();
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }

        private void sliderDiffuse_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.kd = (float)sliderDiffuse.Value / 100.0f;
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }

        private void sliderSpecular_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.ks = (float)sliderSpecular.Value / 100.0f;
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }

        private void sliderShininess_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.m = sliderShininess.Value;
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }

        private void sliderLightHeight_ValueChanged(object sender, EventArgs e)
        {
            visualizer.light.SetHeight((float)sliderLightHeight.Value / 100.0f);
            visualizer.FillFrame_InterpolateColors(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
        }
    }
}
