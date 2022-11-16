using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PRO2_PolygonFiller
{
    public class Light
    {
        public Vertex position;
        public colorvalue color;

        public Light(Vertex _position, Color _color)
        {
            position = _position;
            color = new colorvalue(_color);
        }
        public Light(Vertex _position, colorvalue _color)
        {
            position = _position;
            color = _color;
        }
        public Light(Vertex _position)
        {
            position = _position;
            color = new colorvalue();
        }
        public Light()
        {
            position = new Vertex(0, 0, 0);
            color = new colorvalue(Color.White);
        }

        public void SetHeight(float height)
        {
            position.z = height;
        }
    }
}
