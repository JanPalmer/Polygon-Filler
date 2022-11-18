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

        private bool rotateLightsource = true;
        private bool useColorInterpolation = true;
        //private bool useTexture = false;

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

            if (mesh == null) ActiveForm.Close();

            visualizer = new ModelVisualizer(mesh);
            visualizer.FitModelOnCanvas(canvas);


            visualizer.FillFrame(canvas, bitmap);

            buttonShowColor.BackColor = visualizer.model.color.ToColor();
            //visualizer.DrawFrame(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
            timerAnimation.Start();

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            visualizer.DrawPolygon(e.Graphics);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            visualizer.FitModelOnCanvas(canvas);
            if(visualizer.useTexture == true) visualizer.FitImageOnCanvas(visualizer.texture_originalsize);
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            //Form1.ActiveForm.Text = $"Ticks: {++ticks}, angle: {visualizer.angle}";
            if (Form1.ActiveForm == null) return;

            if(rotateLightsource == true)
            {
                visualizer.RotateLight();
                visualizer.FillFrame(canvas, bitmap);
                canvas.Invalidate();
            }
        }

        private void sliderDiffuse_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.kd = (float)sliderDiffuse.Value / 100.0f;
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void sliderSpecular_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.ks = (float)sliderSpecular.Value / 100.0f;
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void sliderShininess_ValueChanged(object sender, EventArgs e)
        {
            visualizer.model.m = sliderShininess.Value;
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void sliderLightHeight_ValueChanged(object sender, EventArgs e)
        {
            visualizer.light.SetHeight((float)sliderLightHeight.Value / 100.0f);
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void radioButton_LightSpiral_CheckedChanged(object sender, EventArgs e)
        {
            rotateLightsource = !rotateLightsource;
        }

        private void radioButtonInterpolationColor_CheckedChanged(object sender, EventArgs e)
        {
            useColorInterpolation = !useColorInterpolation;
            visualizer.interpolateColor = useColorInterpolation;
        }

        private void buttonSetColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (visualizer.useTexture == true)
                {
                    visualizer.useTexture = false;
                    visualizer.texture = null;
                }

                buttonShowColor.BackColor = colorDialog.Color;
                visualizer.model.color = new colorvalue(colorDialog.Color);
            }
        }

        private void buttonTexture_MouseClick(object sender, MouseEventArgs e)
        {
            //useTexture = true;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                buttonShowColor.BackColor = SystemColors.Control;

                Bitmap imageBitmap = new Bitmap(fileDialog.FileName);
                visualizer.texture_originalsize = imageBitmap;
                visualizer.FitImageOnCanvas(visualizer.texture_originalsize);

                if(useColorInterpolation == true)
                {
                    radioButtonInterpolationPoint.Checked = true;
                }

                visualizer.useTexture = true;
                canvas.Invalidate();
            }
        }
    }
}
