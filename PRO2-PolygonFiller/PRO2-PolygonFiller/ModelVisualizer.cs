using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PRO2_PolygonFiller
{

    using NormalVector = Vector;

    public class ModelVisualizer
    {
        public const int radius = 5;
        public Brush brush = Brushes.Black;
        public Pen pen = Pens.Black;
        public float scale = 1;
        public Point canvasCenter;

        public Model model;
        public Light light;
        public float angle;
        public Vector viewDir;

        public bool interpolateColor = true;
        public bool spiralRotation = true;
        private bool spiralRotation_outward = true;
        //private float spiralCoef = 15.0f;

        public ModelVisualizer(Mesh _mesh)
        {
            if (_mesh == null) throw new Exception();
            model = new Model(_mesh, new colorvalue(1, 0, 0));
            light = new Light();
            viewDir = new Vector(0, 0, 1);
            angle = 0.1f;
        }

        public void FitModelOnCanvas(PictureBox pictureBox)
        {
            float dx = 0, dy = 0, max;
            Vertex n, s, e, w;
            n = s = e = w = model.mesh.vertices[0];
            foreach(Vertex v in model.mesh.vertices)
            {
                if (v.y > n.y) n = v;
                if (v.x > e.x) e = v;
                if (v.x < w.x) w = v;
                if (v.y < s.y) s = v;
            }

            dx = Math.Abs(e.x - w.x);
            dy = Math.Abs(s.y - n.y);
            max = Math.Max(dx, dy);

            int canvasSize = Math.Min(pictureBox.Width, pictureBox.Height);
            scale = (float)(canvasSize - 2 * radius) / max * 0.95f;
            canvasCenter = new Point(pictureBox.Width / 2, pictureBox.Height / 2);

            model.mesh.CastVertices(canvasCenter, scale);
            light = new Light(new Vertex(3.0f, 0, 2.0f), new colorvalue(1.0f, 1.0f, 1.0f));
            light.position.CastVertex(canvasCenter, scale);
        }

        public Color GetVertexShadeColor(Vertex v, NormalVector nv, Vector viewVector, Vertex lightPosition)
        {
            Vector lightVector = new Vector(lightPosition, v);
            lightVector.Normalize();
            Vector reflectionVector = -lightVector - 2 * Vector.Dot(-lightVector, nv) * nv;

            float magnitude = model.kd * (float)Math.Max(Vector.Dot(nv, lightVector), 0)
                + model.ks * (float)Math.Pow(Math.Max(Vector.Dot(viewVector, reflectionVector), 0.0f), model.m);
            float red = magnitude * light.color.R * model.color.R;
            float green = magnitude * light.color.G * model.color.G;
            float blue = magnitude * light.color.B * model.color.B;

            return colorvalue.ToColor(red, green, blue);
        }

        //public Color GetFaceShadeColor(Face f, Vector viewVector, Vector lightVector)
        //{
        //    return GetVertexShadeColor(f.centerOfMass, f.centerNormalVector, viewVector, lightVector);
        //}

        public void CalculateFaceVerticesColors(Face f, Vector viewVector, Vertex lightPosition)
        {
            f.vertexColors = new colorvalue[f.vertices.Count];
            Vertex currVertex;
            NormalVector currNormal;
            for (int i = 0; i < f.vertices.Count; i++)
            {
                currVertex = f.GetVertex(i);
                currNormal = f.GetNormal(i);

                f.vertexColors[i] = new colorvalue(GetVertexShadeColor(currVertex, currNormal, viewVector, lightPosition));
            }
        }

        public Color GetColorAtPoint(Point p, Face f)
        {
            if(interpolateColor == true)
            {
                Point v1 = f.GetVertex(0).cast, v2 = f.GetVertex(1).cast, v3 = f.GetVertex(2).cast;

                float w_v1_1 = (v2.Y - v3.Y) * (p.X - v3.X) + (v3.X - v2.X) * (p.Y - v3.Y);
                float w_v2_1 = (v3.Y - v1.Y) * (p.X - v3.X) + (v1.X - v3.X) * (p.Y - v3.Y);
                float w_mian = (v2.Y - v3.Y) * (v1.X - v3.X) + (v3.X - v2.X) * (v1.Y - v3.Y);
                float w_v1 = w_v1_1 / w_mian;
                float w_v2 = w_v2_1 / w_mian;
                float w_v3 = 1 - w_v1 - w_v2;

                float r = w_v1 * f.GetColor(0).R + w_v2 * f.GetColor(1).R + w_v3 * f.GetColor(2).R;
                float g = w_v1 * f.GetColor(0).G + w_v2 * f.GetColor(1).G + w_v3 * f.GetColor(2).G;
                float b = w_v1 * f.GetColor(0).B + w_v2 * f.GetColor(1).B + w_v3 * f.GetColor(2).B;

                return colorvalue.ToColor(r, g, b);
            }
            else
            {
                Vertex v = new Vertex(f, p);
                v.CastVertexBack(canvasCenter, scale);

                Vertex v1 = f.GetVertex(0), v2 = f.GetVertex(1), v3 = f.GetVertex(2);

                float w_v1_1 = (v2.y - v3.y) * (v.x - v3.x) + (v3.x - v2.x) * (v.y - v3.y);
                float w_mian = (v2.y - v3.y) * (v1.x - v3.x) + (v3.x - v2.x) * (v1.y - v3.y);
                float w_v2_1 = (v3.y - v1.y) * (v.x - v3.x) + (v1.x - v3.x) * (v.y - v3.y);
                float w_v1 = w_v1_1 / w_mian;
                float w_v2 = w_v2_1 / w_mian;
                float w_v3 = 1 - w_v1 - w_v2;

                v.z = w_v1 * v1.z + w_v2 * v2.z + w_v3 * v3.z;

                NormalVector vn1 = f.GetNormal(0), vn2 = f.GetNormal(1), vn3 = f.GetNormal(2);
                NormalVector vn_new = new NormalVector(0, 0, 0);
                vn_new.x = w_v1 * vn1.x + w_v2 * vn2.x + w_v3 * vn3.x;
                vn_new.y = w_v1 * vn1.y + w_v2 * vn2.y + w_v3 * vn3.y;
                vn_new.z = w_v1 * vn1.z + w_v2 * vn2.z + w_v3 * vn3.z;
                vn_new.Normalize();

                return GetVertexShadeColor(v, vn_new, viewDir, light.position);
            }
        }

        public void RotateLight(Point pivot, Light lightsource, float angle)
        {
            // Rotates the Light counter-clockwise by an angle
            float px = lightsource.position.cast.X;
            float py = lightsource.position.cast.Y;

            float s = (float)Math.Sin(angle);
            float c = (float)Math.Cos(angle);
            px -= pivot.X;
            py -= pivot.Y;

            float xnew = px * c - py * s;
            float ynew = px * s + py * c;

            lightsource.position.cast.X = (int)xnew + pivot.X;
            lightsource.position.cast.Y = (int)ynew + pivot.Y;
            lightsource.position.CastVertexBack(canvasCenter, scale);
        }
        public void RotateLight()
        {
            // Rotates the Light counter-clockwise by an angle

            float px = light.position.cast.X;
            float py = light.position.cast.Y;

            //if (spiralRotation)
            //{
            //    if (spiralCoef > 10.0f) spiralRotation_outward = false;
            //    if (spiralCoef <= 0.0f) spiralRotation_outward = true;

            //    if (spiralRotation_outward)
            //    {
            //        spiralCoef += 1.0f;
            //        angle += 0.1f;
            //    }
            //    else
            //    {
            //        spiralCoef -= 1.0f;
            //        angle -= 0.1f;
            //    }
            //}
            //else
            //    spiralCoef = 250.0f;

            //float s = (float)Math.Sin(angle);
            //float c = (float)Math.Cos(angle);
            //px -= canvasCenter.X;
            //py -= canvasCenter.Y;

            //angle += 0.5f;
            //if (spiralRotation_outward)
            //    
            //else
            //    angle -= 1.0f;
            
            float spiralCoef = 25.0f;
            float angleStep = (float)Math.PI / 16.0f;
            
            //if (angle >= 20.0f * (float)Math.PI || angle < 0.0f) spiralRotation_outward = !spiralRotation_outward;


            if (spiralRotation_outward == true)
            {
                angle += angleStep;
                //spiralCoef = 25.0f;

                if(angle > 30.0f * (float)Math.PI + angleStep * 2) 
                    spiralRotation_outward = !spiralRotation_outward;
            }
            else
            {
                angle -= angleStep;
                //spiralCoef = 25.0f;

                if(angle < 0.0f)
                    spiralRotation_outward = !spiralRotation_outward;
            }


            float xnew, ynew;

            if (spiralRotation_outward)
            {
                xnew = ((spiralCoef * angle + 0.1f) / (2.0f * (float)Math.PI)) * (float)Math.Sin(angle);
                ynew = ((spiralCoef * angle + 0.1f) / (2.0f * (float)Math.PI)) * (float)Math.Cos(angle);
            }
            else
            {
                xnew = ((spiralCoef * angle + 0.1f) / (2.0f * (float)Math.PI)) * (float)Math.Cos(angle);
                ynew = ((spiralCoef * angle + 0.1f) / (2.0f * (float)Math.PI)) * (float)Math.Sin(angle);
            }


            //if (Math.Abs(xnew - canvasCenter.X) <= 100 || Math.Abs(xnew - canvasCenter.X) > canvasCenter.X * 2) spiralRotation_outward = !spiralRotation_outward;


            //float xnew = px * c * spiralCoef + 10.0f;
            //float ynew = py * s * spiralCoef + 7.0f;

            //xnew *= spiralCoef; ynew *= spiralCoef;

            light.position.cast.X = (int)xnew + canvasCenter.X;
            light.position.cast.Y = (int)ynew + canvasCenter.Y;
            light.position.CastVertexBack(canvasCenter, scale);
        }

        public void DrawEdges(Graphics g, Face f)
        {
            if (f == null || f.vertices == null || f.vertices.Count <= 0) return;
            for (int i = 0; i < f.vertices.Count; i++)
            {
                g.DrawLine(pen, f.parent.vertices[f.vertices[i].v].cast, f.parent.vertices[f.vertices[(i + 1) % f.vertices.Count].v].cast);
            }
        }

        public void DrawVertices(Graphics g, Mesh m)
        {
            foreach (Vertex v in m.vertices)
            {
                g.FillEllipse(brush, v.cast.X - radius, v.cast.Y - radius, 2 * radius, 2 * radius);
            }
        }

        public void FillFrame_InterpolateColors(Graphics g, Bitmap bitmap)
        {
            g.Clear(Color.White);
            foreach(Face f in model.mesh.faces)
            {
                if(interpolateColor == true) CalculateFaceVerticesColors(f, viewDir, light.position);
                Scanline.ScanLine_ColorInterpolation(this, f, bitmap);
            }
        }

        public void DrawPolygon(Graphics g)
        {
            DrawVertices(g, model.mesh);
            foreach(Face f in model.mesh.faces)
            {
                DrawEdges(g, f);
            }
        }

        //public void DrawFrame(Graphics g, Bitmap bitmap)
        //{
        //    g.Clear(Color.White);
        //    DrawVertices(g, model.mesh);
        //    foreach(Face f in model.mesh.faces)
        //    {
        //        DrawEdges(g, f);
        //        ScanLineWithShading(f, bitmap, light);
        //    }
        //}
    }
}
