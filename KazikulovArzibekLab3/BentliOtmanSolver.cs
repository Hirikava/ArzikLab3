
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KazikulovArzibekLab3
{ 
    public static class BentliOtmanSolver
    {
        public class Segment
        {
            public int Begin { get; set; }
            public int End { get; set; }
        }
        
        public static List<List<PointF>> Solve(List<PointF> points)
        {
            var figurePoints = new List<PointF>(points);

            var nextPoints = new List<int>();
            for (int i = 0; i < points.Count; i++)
                nextPoints.Add((i + 1) % figurePoints.Count);

            List<Segment> segments = new List<Segment>(figurePoints.Count);
            for (int i = 0; i < points.Count; i++)
            {
                segments.Add(new Segment
                {
                    Begin = i,
                    End = nextPoints[i]
                });
            }


            for (int i = 0; i < segments.Count; i++)
            {
                for (int j = 0; j < segments.Count; j++)
                {
                    if (j == i)
                        continue;

                    var segment_i = segments[i];
                    var segment_j = segments[j];
                    var segment_i_begin_point = figurePoints[segment_i.Begin];
                    var segment_i_end_point = figurePoints[segment_i.End];
                    var segment_j_begin_point = figurePoints[segment_j.Begin];
                    var segment_j_end_point = figurePoints[segment_j.End];

                    if (segment_i_begin_point == segment_j_end_point || segment_j_begin_point == segment_i_end_point || segment_i_begin_point == segment_j_begin_point || segment_i_end_point == segment_j_end_point)
                        continue;


                    FindIntersection(segment_i_begin_point, segment_i_end_point, segment_j_begin_point,
                        segment_j_end_point, out var intersects, out var intersectionPoint);

                    if (intersects)
                    {
                        figurePoints.Add(intersectionPoint);
                        figurePoints.Add(intersectionPoint);
                        int segment_i_middle = figurePoints.Count - 1;
                        int segment_j_middle = figurePoints.Count - 2;


                        var segment_i_begin = segment_i.Begin;
                        var segment_i_end = segment_i.End;
                        var segment_j_begin = segment_j.Begin;
                        var segment_j_end = segment_j.End;
                        
                        
                        nextPoints.Add(segment_j.End);
                        nextPoints.Add(segment_i.End);
                        nextPoints[segment_i_begin] = segment_j_middle;
                        nextPoints[segment_j_begin] = segment_i_middle;

                        segments[i].End = segment_j_middle;
                        segments[j].End = segment_i_middle;
                        segments.Add(new Segment()
                        {
                            Begin = segment_i_middle,
                            End = segment_i_end
                        });
                        segments.Add(new Segment()
                        {
                            Begin = segment_j_middle,
                            End = segment_j_end
                        });
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