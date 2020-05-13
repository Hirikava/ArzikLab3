using System.Collections.Generic;
using System.Drawing;

namespace KazikulovArzibekLab3
{ 
    public static class BentliOtmanSolver
    {
        private static void FindIntersection(
            PointF p1, PointF p2, PointF p3, PointF p4, out bool intersects,
            out PointF intersection)
        {
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                / denominator;

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                / -denominator;
            

            intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

            intersects =
                ((t1 >= 0) && (t1 <= 1) &&
                 (t2 >= 0) && (t2 <= 1));
        }
    }
}