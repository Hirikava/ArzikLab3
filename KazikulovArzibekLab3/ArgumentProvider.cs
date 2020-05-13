using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace KazikulovArzibekLab3
{
    public struct Arguments
    {
        public int Width;
        public int Height;
        public List<PointF> Points;
        public string FileName;
    }
    
    public static class ArgumentProvider
    {
        public static Arguments ReadArguments()
        {
            var args = new Arguments();
            Console.Write("Введите ширину исходного изображения в px:");
            var width = 0;
            while (!int.TryParse(Console.ReadLine(), out width) || !ValidateResolution(width))
                Console.Write("Ширина должна быть целочисленым значением с минимальным значением 100:");
            args.Width = width;
            
            Console.Write("Введите высоту исходного изображения в px:");
            var height = 0;
            while (!int.TryParse(Console.ReadLine(), out height) || !ValidateResolution(height))
                Console.Write("Высота должна быть целочисленым значением с минимальным значением 100:");
            args.Height = height;
            
            Console.Write("Введите количество точек многоугольника:");
            var size = 0;
            while (!int.TryParse(Console.ReadLine(), out size) || size < 3)
                Console.Write("Количество точек должно быть целочисленым значением больше 2:");
            
            Console.WriteLine("Введите координаты точек разделённые пробелом, целую часть от дробной отделяйте символом ',':");
            args.Points = new List<PointF>(size);
            while (args.Points.Count < size)
            {
                var tokens = Console.ReadLine()?.Split(' ').ToList();
                if (tokens == null || tokens.Count != 2)
                {
                    Console.WriteLine("Неверный ввод");
                    continue;
                }
                
                if(float.TryParse(tokens[0],out var x) && float.TryParse(tokens[1], out var y))
                    args.Points.Add(new PointF(x,y));
            }
            
            Console.Write("Введите название файла без указания расширения:");
            args.FileName = Console.ReadLine();
            return args;
        }

        private static bool ValidateResolution(int size)
        {
            return size >= 100;
        }
    }
}