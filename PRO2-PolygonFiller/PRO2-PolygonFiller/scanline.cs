using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PRO2_PolygonFiller
{
    using NormalVector = Vector;

    static public class Scanline
    {
        private struct vp
        {
            public int i;
            public int y;
        }

        static public void ScanLine_FacewideShading(Face f, Bitmap bitmap, Color color)
        {
            vp[] P = new vp[f.vertices.Count];
            for (int k = 0; k < f.vertices.Count; k++)
            {
                P[k].i = k;
                P[k].y = f.GetVertex(k).cast.Y;
            }

            Array.Sort(P, (a, b) => a.y.CompareTo(b.y));
            int n = f.vertices.Count;
            int ymin = P[0].y;
            int ymax = P[n - 1].y;
            int i = 0;

            List<EdgeTableNode> aet = new List<EdgeTableNode>();
            for (int y = ymin; y <= ymax; y++)
            {
                while (i < n && y - 1 == P[i].y)
                {
                    int previ = (P[i].i - 1) % n;
                    int curri = P[i].i;
                    int nexti = (P[i].i + 1) % n;
                    if (previ < 0) previ = n - 1;

                    Vertex prevV = f.GetVertex(previ);
                    Vertex currV = f.GetVertex(curri);
                    Vertex nextV = f.GetVertex(nexti);

                    if (prevV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - prevV.cast.X;
                        float dy = currV.cast.Y - prevV.cast.Y;
                        aet.Add(new EdgeTableNode(previ, currV.cast.X, prevV.cast.Y, dx / dy));
                    }

                    if (nextV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - nextV.cast.X;
                        float dy = currV.cast.Y - nextV.cast.Y;
                        aet.Add(new EdgeTableNode(nexti, currV.cast.X, nextV.cast.Y, dx / dy));
                    }

                    if (prevV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }
                    if (nextV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }

                    i++;
                }

                aet.Sort((a, b) => a.x.CompareTo(b.x));
                for (int k = 0; k < aet.Count; k += 2)
                {
                    int startx = Math.Max((int)aet[k].x, 0);
                    int endx = (int)aet[k + 1].x;
                    while (startx <= endx)
                    {
                        bitmap.SetPixel(startx, y, color);
                        startx++;
                    }
                }

                foreach (EdgeTableNode node in aet)
                    node.x += node.m_inv;
            }
        }

        static public void ScanLine(ModelVisualizer vis, Face f, Bitmap bitmap)
        {
            vp[] P = new vp[f.vertices.Count];
            for (int k = 0; k < f.vertices.Count; k++)
            {
                P[k].i = k;
                P[k].y = f.GetVertex(k).cast.Y;
            }

            Array.Sort(P, (a, b) => a.y.CompareTo(b.y));
            int n = f.vertices.Count;
            int ymin = P[0].y;
            int ymax = P[n - 1].y;
            int i = 0;

            List<EdgeTableNode> aet = new List<EdgeTableNode>();
            for (int y = ymin; y <= ymax; y++)
            {
                while (i < n && y - 1 == P[i].y)
                {
                    int previ = (P[i].i - 1) % n;
                    int curri = P[i].i;
                    int nexti = (P[i].i + 1) % n;
                    if (previ < 0) previ = n - 1;

                    Vertex prevV = f.GetVertex(previ);
                    Vertex currV = f.GetVertex(curri);
                    Vertex nextV = f.GetVertex(nexti);

                    if (prevV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - prevV.cast.X;
                        float dy = currV.cast.Y - prevV.cast.Y;
                        aet.Add(new EdgeTableNode(previ, currV.cast.X, prevV.cast.Y, dx / dy));
                    }

                    if (nextV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - nextV.cast.X;
                        float dy = currV.cast.Y - nextV.cast.Y;
                        aet.Add(new EdgeTableNode(nexti, currV.cast.X, nextV.cast.Y, dx / dy));
                    }

                    if (prevV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }
                    if (nextV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }

                    i++;
                }

                aet.Sort((a, b) => a.x.CompareTo(b.x));
                for (int k = 0; k < aet.Count; k += 2)
                {
                    int startx = Math.Max((int)aet[k].x, 0);
                    int endx = (int)aet[k + 1].x;
                    Point tmp_p;
                    while (startx < endx)
                    {
                        tmp_p = new Point(startx, y);
                        bitmap.SetPixel(startx, y, vis.GetColorAtPoint(tmp_p, f));
                        startx++;
                    }
                }

                foreach (EdgeTableNode node in aet)
                    node.x += node.m_inv;
            }
        }

        static public void ScanLine_GetNormalVectorArray(ModelVisualizer vis, Face f, Vector[,] vArrayToFill, NormalVector[,] nvArrayToFill)
        {
            vp[] P = new vp[f.vertices.Count];
            for (int k = 0; k < f.vertices.Count; k++)
            {
                P[k].i = k;
                P[k].y = f.GetVertex(k).cast.Y;
            }

            Array.Sort(P, (a, b) => a.y.CompareTo(b.y));
            int n = f.vertices.Count;
            int ymin = P[0].y;
            int ymax = P[n - 1].y;
            int i = 0;

            List<EdgeTableNode> aet = new List<EdgeTableNode>();
            for (int y = ymin; y <= ymax; y++)
            {
                while (i < n && y - 1 == P[i].y)
                {
                    int previ = (P[i].i - 1) % n;
                    int curri = P[i].i;
                    int nexti = (P[i].i + 1) % n;
                    if (previ < 0) previ = n - 1;

                    Vertex prevV = f.GetVertex(previ);
                    Vertex currV = f.GetVertex(curri);
                    Vertex nextV = f.GetVertex(nexti);

                    if (prevV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - prevV.cast.X;
                        float dy = currV.cast.Y - prevV.cast.Y;
                        aet.Add(new EdgeTableNode(previ, currV.cast.X, prevV.cast.Y, dx / dy));
                    }

                    if (nextV.cast.Y > currV.cast.Y)
                    {
                        float dx = currV.cast.X - nextV.cast.X;
                        float dy = currV.cast.Y - nextV.cast.Y;
                        aet.Add(new EdgeTableNode(nexti, currV.cast.X, nextV.cast.Y, dx / dy));
                    }

                    if (prevV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }
                    if (nextV.cast.Y < currV.cast.Y)
                    {
                        aet.RemoveAll(node => node.id == curri);
                    }

                    i++;
                }

                aet.Sort((a, b) => a.x.CompareTo(b.x));
                for (int k = 0; k < aet.Count; k += 2)
                {
                    int startx = Math.Max((int)aet[k].x, 0);
                    int endx = (int)aet[k + 1].x;
                    Point tmp_p;                    
                    while (startx <= endx)
                    {
                        tmp_p = new Point(startx, y);
                        //Vertex dummyvertex;
                        (vArrayToFill[startx - vis.topleft.X, y - vis.topleft.Y], nvArrayToFill[startx - vis.topleft.X, y - vis.topleft.Y]) = vis.GetNormalVector_PointInterpolation(tmp_p, f);
                        startx++;
                    }
                }

                foreach (EdgeTableNode node in aet)
                    node.x += node.m_inv;
            }
        }

        //static public void ScanLineWithFacewideShading(ModelVisualizer vis, Face f, Bitmap bitmap, Light lightsource)
        //{
        //    Vector lightVector = new Vector(lightsource.position, f.centerOfMass);
        //    lightVector.Normalize();
        //    Color faceColor = vis.GetFaceShadeColor(f, vis.viewDir, lightVector);
        //    ScanLine_FacewideShading(f, bitmap, faceColor);
        //}

    }
}
