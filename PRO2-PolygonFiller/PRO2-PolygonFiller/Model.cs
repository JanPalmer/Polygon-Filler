using System;
using System.Collections.Generic;
using System.Text;

namespace PRO2_PolygonFiller
{
    public class Model
    {
        public Mesh mesh;
        public colorvalue color;
        public float kd, ks, m;

        public Model(Mesh _mesh, colorvalue _color, float _kd = 1.0f, float _ks = 1.0f, float _m = 32.0f)
        {
            mesh = _mesh;
            color = _color;
            kd = _kd; ks = _ks; m = _m;


        }
    }
}
