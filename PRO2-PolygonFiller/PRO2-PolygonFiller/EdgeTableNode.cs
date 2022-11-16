using System;
using System.Collections.Generic;
using System.Text;

namespace PRO2_PolygonFiller
{
    public class EdgeTableNode
    {
        public int id;
        public float y, x; // index for EdgeTable, has to be int
        public float m_inv;

        public EdgeTableNode(int _id, int _x, int _y, float _m_inv)
        {
            id = _id;
            x = _x;
            y = _y;
            m_inv = _m_inv;
        }

    }
}
