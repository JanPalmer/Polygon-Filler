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
        public string fmt = "F2";

        private bool rotateLightsource = true;
        private bool useColorInterpolation = false;
        private bool useNormalMap = true;
        private bool showEdgesAndVertices = false;
        //private bool useTexture = false;

        public Form1()
        {
            InitializeComponent();
            timerAnimation = new Timer();
            bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            canvas.Image = bitmap;
            //string path = "..\\..\\..\\Objects\\proj2_sfera.obj";
            string pathOBJ = "..\\..\\..\\Objects\\Sphere.obj";
            string pathNORMALMAP = "..\\..\\..\\Objects\\proj2_brick_normalmap.png";
            //string path = "..\\..\\..\\Objects\\Pyramid.obj";

            //Mesh mesh = MeshCreator.CreateMeshFromObj("D:\\Studia-2022-2023-sem5\\Grafika_Komputerowa_1\\PRO2\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Pyramid.obj");            
            Mesh mesh = MeshCreator.CreateMeshFromObj(pathOBJ);
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\proj2_sfera.obj");
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Pyramid.obj");
            //Mesh mesh = MeshCreator.CreateMeshFromObj("C:\\Studia sem5\\Grafika Komputerowa 1\\PRO2\\Polygon-Filler-main\\PRO2-PolygonFiller\\PRO2-PolygonFiller\\Objects\\Rhombus.obj");

            Bitmap normalMapBitmap = new Bitmap(pathNORMALMAP);

            if (mesh == null) ActiveForm.Close();

            visualizer = new ModelVisualizer(mesh, rotateLightsource, useColorInterpolation, useNormalMap);
            visualizer.FitModelOnCanvas(canvas);
            visualizer.light.SetHeight((float)sliderLightHeight.Value / 100.0f);
            visualizer.model.kd = (float)sliderDiffuse.Value / 100.0f;
            visualizer.model.ks = (float)sliderSpecular.Value / 100.0f;
            visualizer.model.m = sliderShininess.Value;
            labelLightHeight.Text = visualizer.light.position.z.ToString(fmt);
            visualizer.SetNewNormalMap(normalMapBitmap);

            visualizer.FillFrame(canvas, bitmap);

            checkBoxLightMovement.Checked = !rotateLightsource;
            checkBoxNormalMap.Checked = useNormalMap;
            checkBoxPaintEdges.Checked = showEdgesAndVertices;

            buttonShowColor.BackColor = visualizer.model.color.ToColor();
            //visualizer.DrawFrame(Graphics.FromImage(bitmap), bitmap);
            canvas.Invalidate();
            timerAnimation.Start();

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (showEdgesAndVertices) visualizer.DrawPolygon(e.Graphics);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (visualizer == null) return;
            visualizer.FitModelOnCanvas(canvas);
            if(visualizer.useTexture == true) visualizer.FitImageOnCanvas(visualizer.texture_originalsize);
            if (visualizer.useNormalMap == true) visualizer.FitNormalMapOnCanvas(visualizer.normalMap_originalsize);
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
            float height = (float)sliderLightHeight.Value / 100.0f;
            visualizer.light.SetHeight(height);
            labelLightHeight.Text = visualizer.light.position.z.ToString(fmt);
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void radioButtonInterpolationColor_CheckedChanged(object sender, EventArgs e)
        {
            useColorInterpolation = !useColorInterpolation;
            visualizer.useColorInterpolation = useColorInterpolation;
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
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
            visualizer.FillFrame(canvas, bitmap);
            canvas.Invalidate();
        }

        private void buttonTexture_MouseClick(object sender, MouseEventArgs e)
        {
            //useTexture = true;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF";
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

        private void buttonNewModel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "3D meshes|*.OBJ";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Mesh mesh = MeshCreator.CreateMeshFromObj(fileDialog.FileName);
                visualizer.SetNewModel(mesh, canvas);
                canvas.Invalidate();
            }
        }

        private void buttonNormalMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Normal Maps|*.JPG;*.PNG";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap normalmapBitmap = new Bitmap(fileDialog.FileName);
                visualizer.SetNewNormalMap(normalmapBitmap);
                canvas.Invalidate();
            }
        }

        private void checkBoxNormalMap_CheckedChanged(object sender, EventArgs e)
        {
            useNormalMap = !useNormalMap;
            visualizer.useNormalMap = useNormalMap;
        }

        private void checkBoxPaintEdges_CheckedChanged(object sender, EventArgs e)
        {
            showEdgesAndVertices = !showEdgesAndVertices;
            canvas.Invalidate();
        }

        private void checkBoxLightMovement_CheckedChanged(object sender, EventArgs e)
        {
            rotateLightsource = !rotateLightsource;
            visualizer.spiralRotation = rotateLightsource;
        }
    }
}
