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
        public Point topleft, bottomleft, topright, bottomright;

        public Model model;
        public Light light;
        public float angle;
        public Vector viewDir;

        // Spiral constants
        const float spiralCoef = 30.0f;
        const float angleStep = (float)Math.PI / 16.0f;

        // Texture variables
        public bool useTexture = false;
        public Bitmap texture_originalsize;
        public Bitmap texture;

        // Normal map variables
        public bool useNormalMap = false;
        public Bitmap normalMap_originalsize;
        public Bitmap normalMap;
        public NormalVector[,] nvArray_model;
        public NormalVector[,] nvArray_normalMap;
        public Vertex[,] vArray;

        // Bools for modes
        public bool useColorInterpolation = true;
        public bool spiralRotation = true;
        private bool spiralRotation_outward = true;

        public ModelVisualizer(Mesh _mesh, bool _rotateLightsource = false, bool _useColorInterpolation = true, bool _useNormalMap = false)
        {
            if (_mesh == null) throw new Exception();
            model = new Model(_mesh, new colorvalue(1, 0, 0));
            light = new Light();
            viewDir = new Vector(0, 0, 1);
            angle = 0.1f;

            spiralRotation = _rotateLightsource;
            useColorInterpolation = _useColorInterpolation;
            useNormalMap = _useNormalMap;
        }

        public void SetNewModel(Mesh _mesh, PictureBox pictureBox)
        {
            model = new Model(_mesh, new colorvalue(1, 0, 0));
            FitModelOnCanvas(pictureBox);
            if (useTexture == true) FitImageOnCanvas(texture_originalsize);
            if (useNormalMap == true) FitNormalMapOnCanvas(normalMap_originalsize);
        }
        public void SetNewNormalMap(Bitmap bitmap)
        {
            normalMap_originalsize = bitmap;
            FitNormalMapOnCanvas(normalMap_originalsize);
        }
        public void FitModelOnCanvas(PictureBox pictureBox)
        {
            float dx, dy, max;
            Vertex n, s, e, w;
            n = s = e = w = model.mesh.vertices[0];
            foreach(Vertex v in model.mesh.vertices)
            {
                if (v.y < n.y) n = v;
                if (v.x > e.x) e = v;
                if (v.x < w.x) w = v;
                if (v.y > s.y) s = v;
            }

            dx = Math.Abs(e.x - w.x);
            dy = Math.Abs(s.y - n.y);
            max = Math.Max(dx, dy);

            int canvasSize = Math.Min(pictureBox.Width, pictureBox.Height);
            scale = (float)(canvasSize - 2 * radius) / max * 0.95f;
            canvasCenter = new Point(pictureBox.Width / 2, pictureBox.Height / 2);

            model.mesh.CastVertices(canvasCenter, scale);
            light.position.CastVertex(canvasCenter, scale);

            topleft = new Point(w.cast.X, n.cast.Y);
            bottomleft = new Point(w.cast.X, s.cast.Y);
            topright = new Point(e.cast.X, n.cast.Y);
            bottomright = new Point(e.cast.X, s.cast.Y);

            vArray = new Vertex[topright.X - topleft.X + 1, bottomleft.Y - topleft.Y + 1];
            nvArray_model = new NormalVector[topright.X - topleft.X + 1, bottomleft.Y - topleft.Y + 1];
            foreach (Face f in model.mesh.faces)
            {
                Scanline.ScanLine_GetNormalVectorArray(this, f, vArray, nvArray_model);
            }
        }

        // Fit the model texture from a given Bitmap
        public void FitImageOnCanvas(Bitmap image)
        {
            texture = new Bitmap(topright.X - topleft.X + 1, bottomleft.Y - topleft.Y + 1);
            Graphics g = Graphics.FromImage(texture);
            g.Clear(Color.White);
            g.DrawImage(image, 0, 0, texture.Width, texture.Height);
        }

        public void FitNormalMapOnCanvas(Bitmap mapOriginal)
        {
            normalMap_originalsize = mapOriginal;
            normalMap = new Bitmap(topright.X - topleft.X + 1, bottomleft.Y - topleft.Y + 1);
            Graphics g = Graphics.FromImage(normalMap);
            g.Clear(Color.White);
            g.DrawImage(normalMap_originalsize, 0, 0, normalMap.Width, normalMap.Height);
            nvArray_normalMap = new NormalVector[normalMap.Width, normalMap.Height];

            for (int y = 0; y < normalMap.Height; y++)
                for(int x = 0; x < normalMap.Width; x++)
                {
                    nvArray_normalMap[x, y] = colorvalue.ConvertColorToVector(normalMap.GetPixel(x, y));
                    nvArray_normalMap[x, y].Normalize();
                }
        }
        public NormalVector GetNormalMapNormalVector_PointInterpolation(Point p)
        {
            NormalVector nv_texture = nvArray_normalMap[p.X - topleft.X, p.Y - topleft.Y];
            NormalVector nv_model = nvArray_model[p.X - topleft.X, p.Y - topleft.Y];
            NormalVector B, T;
            Vector unitZ = new Vector(0, 0, 1);

            // Calculate 3x3 transform matrix
            if(Vector.Distance(nv_model, unitZ) < 0.01f)
            {
                B = new NormalVector(0, 1, 0);
            }
            else
            {
                B = Vector.Cross(nv_model, unitZ);
            }

            T = Vector.Cross(B, nv_model);

            return Vector.Multiply3x3MatrixByVector((T, B, nv_model), nv_texture);
        }
        public NormalVector GetNormalMapNormalVector_ColorInterpolation(Vertex v, NormalVector nv)
        {
            NormalVector nv_texture = nvArray_normalMap[v.cast.X - topleft.X, v.cast.Y - topleft.Y];
            NormalVector B, T;
            Vector unitZ = new Vector(0, 0, 1);

            // Calculate 3x3 transform matrix
            if (Vector.Distance(nv, unitZ) < 0.01f)
            {
                B = new NormalVector(0, 1, 0);
            }
            else
            {
                B = Vector.Cross(nv, unitZ);
            }

            T = Vector.Cross(B, nv);

            return Vector.Multiply3x3MatrixByVector((T, B, nv), nv_texture);
        }

        public Color GetVertexShadeColor(Vertex v, NormalVector nv, Vector viewVector, Vertex lightPosition, colorvalue color)
        {
            Vector lightVector = new Vector(lightPosition, v);
            lightVector.Normalize();
            Vector reflectionVector = -lightVector - 2 * Vector.Dot(-lightVector, nv) * nv;

            float magnitude = model.kd * (float)Math.Max(Vector.Dot(nv, lightVector), 0)
                + model.ks * (float)Math.Pow(Math.Max(Vector.Dot(viewVector, reflectionVector), 0.0f), model.m);
            float red = magnitude * light.color.R * color.R;
            float green = magnitude * light.color.G * color.G;
            float blue = magnitude * light.color.B * color.B;

            return colorvalue.ToColor(red, green, blue);
        }
        public Color GetVertexShadeColor(Vertex v, NormalVector nv, Vector viewVector, Vertex lightPosition)
        {
            return GetVertexShadeColor(v, nv, viewVector, lightPosition, model.color);
        }

        public void CalculateFaceVerticesColors(Face f, Vector viewVector, Vertex lightPosition)
        {
            f.vertexColors = new colorvalue[f.vertices.Count];
            Vertex currVertex;
            NormalVector currNormal;
            for (int i = 0; i < f.vertices.Count; i++)
            {
                currVertex = f.GetVertex(i);
                currNormal = (useNormalMap == true) ? GetNormalMapNormalVector_ColorInterpolation(currVertex, f.GetNormal(i)) : f.GetNormal(i);

                if(useTexture == true)
                {
                    colorvalue bitmapColor = new colorvalue(texture.GetPixel(currVertex.cast.X - topleft.X, currVertex.cast.Y - topleft.Y));
                    f.vertexColors[i] = new colorvalue(GetVertexShadeColor(currVertex, currNormal, viewVector, lightPosition, bitmapColor));
                }
                else
                {
                    f.vertexColors[i] = new colorvalue(GetVertexShadeColor(currVertex, currNormal, viewVector, lightPosition));
                }

            }
        }
        public Color GetColor_ColorInterpolation(Point p, Face f)
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
        public (Vertex, NormalVector) GetNormalVector_PointInterpolation(Point p, Face f)
        {
            Vertex v = new Vertex(p);
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
            return (v, vn_new);
        }
        // Main function referenced in ScanLine to get a pixel's Color. Its value depends on current settings
        public Color GetColorAtPoint(Point p, Face f)
        {
            if(useColorInterpolation == true)
            {
                return GetColor_ColorInterpolation(p, f);
            }
            else
            {
                Vertex v = vArray[p.X - topleft.X, p.Y - topleft.Y];
                NormalVector nv = (useNormalMap == true) ? nvArray_normalMap[p.X - topleft.X, p.Y - topleft.Y] : nvArray_model[p.X - topleft.X, p.Y - topleft.Y];

                if (useTexture == false)
                {
                    return GetVertexShadeColor(v, nv, viewDir, light.position);
                }
                else
                {
                    colorvalue bitmapColor = new colorvalue(texture.GetPixel(p.X - topleft.X, p.Y - topleft.Y));
                    return GetVertexShadeColor(v, nv, viewDir, light.position, bitmapColor);
                }
            }
        }

        //public void RotateLight(Point pivot, Light lightsource, float angle)
        //{
        //    // Rotates the Light counter-clockwise by an angle
        //    float px = lightsource.position.cast.X;
        //    float py = lightsource.position.cast.Y;

        //    float s = (float)Math.Sin(angle);
        //    float c = (float)Math.Cos(angle);
        //    px -= pivot.X;
        //    py -= pivot.Y;

        //    float xnew = px * c - py * s;
        //    float ynew = px * s + py * c;

        //    lightsource.position.cast.X = (int)xnew + pivot.X;
        //    lightsource.position.cast.Y = (int)ynew + pivot.Y;
        //    lightsource.position.CastVertexBack(canvasCenter, scale);
        //}
        public void RotateLight()
        {
            // Rotates the Light in a unwinding and winding spiral

            if (spiralRotation_outward == true)
            {
                angle += angleStep;
            }
            else
            {
                angle -= angleStep;
            }

            if(angle < 0.0f || angle > 30.0f * (float)Math.PI + angleStep * 4)  spiralRotation_outward = !spiralRotation_outward;

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

            light.position.cast.X = (int)xnew + canvasCenter.X;
            light.position.cast.Y = (int)ynew + canvasCenter.Y;
            light.position.CastVertexBack(canvasCenter, scale);
        }

        // Drawing
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

        public void FillFrame(PictureBox pictureBox, Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(pictureBox.Image);
            g.Clear(Color.White);

            foreach (Face f in model.mesh.faces)
            {
                if(useColorInterpolation == true) CalculateFaceVerticesColors(f, viewDir, light.position);
                Scanline.ScanLine(this, f, bitmap);
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
    }
}
