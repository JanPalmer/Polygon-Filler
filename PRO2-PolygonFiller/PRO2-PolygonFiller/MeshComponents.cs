using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace PRO2_PolygonFiller
{
    // This 'using' is made for purely philosophical reasons, to differenciate
    // direction vectors like the direction from a lightsource to a surface
    // from a surface's slope
    using NormalVector = Vector;

    public class Vector
    {
        // Fields
        public float x, y, z;

        // Constructors
        public Vector()
        {
            x = 0; y = 0; z = 0;
        }
        public Vector(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public Vector(Vertex start, Vertex end)
        {
            x = start.x - end.x;
            y = start.y - end.y;
            z = start.z - end.z;
        }

        // Operator Overloads
        static public Vector operator-(Vector v)
        {
            return new Vector(-v.x, -v.y, -v.z);
        }
        static public Vector operator+(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        static public Vector operator-(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        static public Vector operator*(Vector v, float t)
        {
            return new Vector(v.x * t, v.y * t, v.z * t);
        }
        static public Vector operator*(float t, Vector v)
        {
            return v * t;
        }

        // Object Methods
        public float LengthSquare()
        {
            return x * x + y * y + z * z;
        }
        public float Length()
        {
            return (float)Math.Sqrt(LengthSquare());
        }
        public void Normalize()
        {
            float len = Length();
            x /= len; y /= len; z /= len;
        }

        // Static Methods
        static public float Dot(Vector u, Vector v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }
        static public Vector Cross(Vector u, Vector v)
        {
            return new Vector(
                u.y * v.z - u.z * v.y,
                u.z * v.x - u.x * v.z,
                u.x * v.y - u.y * v.x);
        }
        static public float Distance(Vector u, Vector v)
        {
            Vector tmp = u - v;
            return tmp.Length();
        }
        static public Vector Multiply3x3MatrixByVector((Vector v1, Vector v2, Vector v3) matrix, Vector u)
        {
            Vector vx = new Vector(matrix.v1.x, matrix.v2.x, matrix.v3.x);
            Vector vy = new Vector(matrix.v1.y, matrix.v2.y, matrix.v3.y);
            Vector vz = new Vector(matrix.v1.z, matrix.v2.z, matrix.v3.z);

            Vector result = new Vector();
            result.x = Vector.Dot(vx, u);
            result.y = Vector.Dot(vy, u);
            result.z = Vector.Dot(vz, u);
            return result;
        }

    }

    public class Vertex : Vector
    {
        public Point cast; // Cast exists as a PictureBox-context side of a Vertex
        public colorvalue color;
        public Vertex(float _x, float _y, float _z) : base(_x, _y, _z) { }

        public Vertex(Point p) : base(0, 0, 0)
        {
            cast = p;
        }

        public void CastVertex(Point canvasCenter, float scale)
        {
            cast = new Point(canvasCenter.X + (int)(x * scale), canvasCenter.Y + (int)(y * scale));
        }
        public void CastVertexBack(Point canvasCenter, float scale)
        {
            x = ((float)(cast.X - canvasCenter.X)) / scale;
            y = ((float)(cast.Y - canvasCenter.Y)) / scale;
        }
    }

    public class Face
    {
        // v - index of a Vertex in its Face's parent (Mesh), nv - index of a Normal Vector in its Face's parent (Mesh)
        public List<(int v, int nv)> vertices;
        public Mesh parent;
        public colorvalue[] vertexColors;

        public Face()
        {
            vertices = new List<(int, int)>();
        }

        // Getter for a Vertex Object which the Face references
        public Vertex GetVertex(int i)
        {
            return parent.vertices[vertices[i].v];
        }
        // Getter for a NormalVector Object which the Face references
        public NormalVector GetNormal(int i)
        {
            return parent.normalVectors[vertices[i].nv];
        }
        public colorvalue GetColor(int i)
        {
            return vertexColors[i];
        }
        public void AddVertex(int v, int nv)
        {
            vertices.Add((v, nv));
        }
    }

    public class Mesh
    {
        public List<Vertex> vertices;
        public List<NormalVector> normalVectors;
        public List<Face> faces;

        public Mesh()
        {
            vertices = new List<Vertex>();
            normalVectors = new List<NormalVector>();
            faces = new List<Face>();
        }

        public void CastVertices(Point canvasCenter, float scale)
        {
            foreach(Vertex v in vertices)
            {
                v.CastVertex(canvasCenter, scale);
            }
        }
    }

    public static class MeshCreator
    {
        public static Mesh CreateMeshFromObj(string path)
        {
            if (!File.Exists(path)) return null;

            FileStream fs = File.OpenRead(path);
            StreamReader reader = new StreamReader(fs);

            Mesh mesh = new Mesh();

            string line;
            string[] div;
            CultureInfo ci = CultureInfo.InvariantCulture;

            while((line = reader.ReadLine()) != null)
            {
                div = line.Split(' ');

                switch (div[0])
                {
                    case "v": 
                        mesh.vertices.Add(new Vertex(float.Parse(div[1], ci), float.Parse(div[2], ci), float.Parse(div[3], ci)));
                        break;
                    case "vn":
                        mesh.normalVectors.Add(new NormalVector(float.Parse(div[1], ci), float.Parse(div[2], ci), float.Parse(div[3], ci)));
                        break;
                    case "f":
                        Face tmpFace = new Face();
                        tmpFace.parent = mesh;

                        int vert, norm;
                        string[] indexString;
                        for (int i = 1; i < div.Length; i++)
                        {
                            indexString = div[i].Split('/');
                            switch (indexString.Length)
                            {
                                //case 1:
                                //    vert = int.Parse(indexString[0], ci) - 1;
                                //    tmpFace.AddVertex(vert);
                                //    break;
                                case 3:
                                    vert = int.Parse(indexString[0], ci) - 1;
                                    norm = int.Parse(indexString[2], ci) - 1;
                                    tmpFace.AddVertex(vert, norm);
                                    break;
                                default:
                                    break;
                            }
                        }
                        mesh.faces.Add(tmpFace);
                        break;
                    default:
                        break;
                }
            }
            return mesh;
        }
    }
}
