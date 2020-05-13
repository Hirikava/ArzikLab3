using System;
using System.Drawing;

namespace KazikulovArzibekLab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var arguments = ArgumentProvider.ReadArguments();
            var polygons = BentliOtmanSolver.Solve(arguments.Points);
            var bitmap = new Bitmap(arguments.Width, arguments.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var random = new Random();
                foreach (var polygon in polygons)
                {
                    var pen = new Pen(Color.FromArgb(random.Next(255),random.Next(255),random.Next(255)),2);
                    for (int i = 0; i < polygon.Count; i++)
                    {
                        var p1 = ToScreenCords(polygon[i],arguments.Width,arguments.Height);
                        var p2 = ToScreenCords(polygon[(i + 1) % (polygon.Count)],arguments.Width,arguments.Height);
                        graphics.DrawLine(pen,p1,p2);
                    }
                }
                
            }
            
            bitmap.Save(arguments.FileName + ".bmp");
        }

        public static PointF ToScreenCords(PointF point, int width, int height)
        {
            return new PointF(point.X + width / 2, (-point.Y) + height / 2);
        }
    }
}