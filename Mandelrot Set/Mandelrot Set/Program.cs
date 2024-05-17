using System;
using System.Drawing;
using System.Numerics;

namespace Mandelrot_Set
{
    class Program
    {
        static class Globals
        {
            public static Color pixelColor;
        }

        static void Main(string[] args)
        {
            // Iterate every point by 255*3 times, and see if they converge.
            // If they do, asign the pixel the colour based on how many interations through they went.

            Console.WriteLine("probably running");

            int xSize = 300;
            int ySize = 150;
            Bitmap mandelbrotImage = new Bitmap(xSize, ySize);

            int relativeXAxis = mandelbrotImage.Width / 2;
            int relativeYAxis = mandelbrotImage.Height / 2;
            int n;
            Complex z;
            Complex c;
            int red;
            int green = 0;
            int blue = 0;
            int maxIterations = 1000;

            for (double x = 0; x < mandelbrotImage.Width; x++)
            {
                for (double y = 0; y < mandelbrotImage.Height; y++)
                {
                    n = 0;
                    z = 0;
                    double newX = (x - relativeXAxis) / (xSize/6);
                    double newY = (y - relativeYAxis) / (ySize/3);

                    c = new Complex(newX, newY);

                    red = 255;
                    Globals.pixelColor = Color.FromArgb(red, green, blue);

                    while (n < maxIterations)
                    {
                        z = Complex.Add(Complex.Multiply(z,z),c);
                        n++;
                    }

                    if (Complex.Abs(z) < 10)
                    {
                        red = 0;
                        Globals.pixelColor = Color.FromArgb(red, green, blue);
                    }
                    mandelbrotImage.SetPixel(Convert.ToInt32(x), Convert.ToInt32(y), Globals.pixelColor);
                }
                Console.WriteLine("Still running at x {0}.",x);
            }

            mandelbrotImage.Save("Mandelbrot.bmp");
        }
    }
}
