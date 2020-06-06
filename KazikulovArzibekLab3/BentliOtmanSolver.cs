using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KazikulovArzibekLab3
{ 
    public static class BentliOtmanSolver
    {

        public static List<List<PointF>> Solve(List<PointF> points)
        {
            var figurePoints = new List<PointF>(points);

            var nextPoints = new List<int>();
            for (int i = 0; i < points.Count; i++)
                nextPoints.Add((i + 1) % figurePoints.Count);

            List<int>[] segments = new List<int>[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                segments[i] = new List<int>();
                segments[i].Add(i);
                segments[i].Add(nextPoints[i]);
            }


            for (int i = 0; i < points.Count; i++)
                for (int j = 0; j < points.Count; j++)
                {
                    if (j <= i + 1 || (j == points.Count - 1 && i == 0))
                        continue;

                    
                    bool intersects = false;
                    for (int segi = 0; segi < segments[i].Count - 1; segi++)
                    {
                        if(intersects)
                            break;
                       
                        for (int segj = 0; segj < segments[j].Count - 1; segj++)
                        {
                            var p1 = figurePoints[segments[i][segi]];
                            var p2 = figurePoints[segments[i][segi + 1]];
                            var p3 = figurePoints[segments[j][segj]];
                            var p4 = figurePoints[segments[j][segj + 1]];
                            
                            FindIntersection(p1, p2, p3, p4, out intersects,out var intersectionPoint);

                            if (intersects)
                            {
                                figurePoints.Add(intersectionPoint);
                                figurePoints.Add(intersectionPoint);
                                nextPoints.Add(segments[j][segj + 1]);
                                nextPoints.Add(segments[i][segi + 1]);
                                int ipoint = figurePoints.Count - 2;
                                int jpoint = figurePoints.Count - 1;

                                nextPoints[segments[i][segi]] = ipoint;
                                nextPoints[segments[j][segj]] = jpoint;

                                segments[i].Insert(segi + 1, ipoint);
                                segments[j].Insert(segj + 1, jpoint);
                                break;
                            }
                        }
                    }
                }

            
            var result = new List<List<PointF>>();
            var visited = nextPoints.Select(x => false).ToList();
            for (int i = 0; i < visited.Count; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    var newPolygon = new List<PointF>();
                    newPolygon.Add(figurePoints[i]);
                    var currentPoint = i;
                    while (!visited[nextPoints[currentPoint]])
                    {
                        visited[nextPoints[currentPoint]] = true;
                        newPolygon.Add(figurePoints[nextPoints[currentPoint]]);
                        currentPoint = nextPoints[currentPoint];
                    }

                    result.Add(newPolygon);
                }
            }

            return result;
        }


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