using System;
using System.Drawing;
using System.Linq;

namespace KazikulovArzibekLab3
{
    internal class Program
    {
        
        private static Color[] colors = new Color[] {Color.Brown, Color.Aqua, Color.Crimson, Color.Gold, Color.Green, Color.Indigo, };
        
        public static void Main(string[] args)
        {
            var arguments = ArgumentProvider.ReadArguments();
            var polygons = BentliOtmanSolver.Solve(arguments.Points);
            var bitmap = new Bitmap(arguments.Width, arguments.Height);
            
         

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var random = new Random();
                for(int polygonNumber = 0; polygonNumber < polygons.Count; polygonNumber++)
                {
                    var pen = new Pen(colors[polygonNumber % colors.Length] ,2);
                    for (int i = 0; i < polygons[polygonNumber].Count; i++)
                    {
                        var p1 = ToScreenCords(polygons[polygonNumber][i],arguments.Width,arguments.Height);
                        var p2 = ToScreenCords(polygons[polygonNumber][(i + 1) % (polygons[polygonNumber].Count)],arguments.Width,arguments.Height);
                        graphics.DrawLine(pen,p1,p2);
                    }
                }
                
            }
            
            bitmap.Save(arguments.FileName + ".bmp");
        }

        private static PointF ToScreenCords(PointF point, int width, int height)
        {
            return new PointF(point.X + width / 2, (-point.Y) + height / 2);
        }
    }
}