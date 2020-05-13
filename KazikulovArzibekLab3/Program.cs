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
        }
    }
}