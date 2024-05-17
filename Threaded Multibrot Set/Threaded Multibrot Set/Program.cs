using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.IO;

namespace Mandelrot_Set
{
    class Program
    {
        static class Global
        {
            public static int xSize = 3000;
            public static int ySize = 1500;

            public static Bitmap mandelbrotImage = new Bitmap(xSize, ySize);

            public static int relativeXAxis = mandelbrotImage.Width / 2;
            public static int relativeYAxis = mandelbrotImage.Height / 2;

            public static int[] pixels = new int[xSize * ySize];
        }

        public static void squareFill(int startX, int endX, int startY, int endY, double power)
        {
            Complex z;
            Complex c;
            int maxIterations = 255 * 3;

            int i = 0;

            for (double x = startX; x < endX + 1; x++)
            {
                for (double y = startY; y < endY + 1; y++)
                {
                    z = 0;
                    double newX = (x - Global.relativeXAxis) / (Global.xSize / 8.6);
                    double newY = (y - Global.relativeYAxis) / (Global.ySize / 4.3);

                    c = new Complex(newX, newY);

                    int colour = 255;
                    for (int n = 0; n < maxIterations; n++)
                    {
                        z = Complex.Add(Complex.Pow(z, power), c);
                    }

                    
                    if (Complex.Abs(z) < 2)
                    {
                        colour = 0;
                    }
                    Global.pixels[i + (startX * 1500)] = colour;

                    /*int n = 0;
                    while (Complex.Abs(z) < 2 && n < maxIterations)
                    {
                        Complex complexPower = new Complex(0, power);

                        z = Complex.Add(Complex.Pow(z, complexPower), c);

                        n++;
                    }
                    Global.pixels[i + (startX * 1500)] = n;*/

                    i++;
                }
                //Console.WriteLine("Still running at x {0}.", x);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("probably running");
            for (double i = 2; i <= 4; i += 0.01)
            {
                makeSetOfPower(Math.Round(i,2));
            }
        }

        private static void makeSetOfPower(double power)
        {
            Console.WriteLine(power);
            Thread t1 = new Thread(() => squareFill(0, 499, 0, 1499, power));
            Thread t2 = new Thread(() => squareFill(500, 999, 0, 1499, power));
            Thread t3 = new Thread(() => squareFill(1000, 1499, 0, 1499, power));
            Thread t4 = new Thread(() => squareFill(1500, 1999, 0, 1499, power));
            Thread t5 = new Thread(() => squareFill(2000, 2499, 0, 1499, power));
            Thread t6 = new Thread(() => squareFill(2500, 2999, 0, 1499, power));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();

            /*StreamWriter sw = new StreamWriter("69" + ".txt");
            sw.Write(Global.pixels[0]);
            for (int p = 1; p < Global.pixels.Length; p++)
            {
                sw.Write("," + Global.pixels[p]);
            }
            sw.Close();*/

            //int red = 0;
            //int green = 0;
            //int blue = 0;

            for (int i = 0; i < (Global.xSize * Global.ySize); i++)
            {
                /*if (Global.pixels[i] > 255)
                {
                    red = 255;
                    if (Global.pixels[i] > 510)
                    {
                        green = 255;
                        blue = Global.pixels[i] - 510;
                    }
                    else
                    {
                        green = Global.pixels[i] - 255;
                    }
                }
                else
                {
                    red = Global.pixels[i];
                }*/

                Color newColor;

                if (Global.pixels[i] >= 255)
                {
                    newColor = Color.FromArgb(255, 0, 0);
                }
                else
                {
                    newColor = Color.FromArgb(0, 0, 0);
                }

                //Color newColor = Color.FromArgb(Global.pixels[i], 0, 0);

                Global.mandelbrotImage.SetPixel(i / Global.ySize, i % Global.ySize, newColor);
            }

            string powerText = Convert.ToString(power);

            if (power % 1 == 0)
            {
                powerText += ".0";
            }

            Global.mandelbrotImage.Save(powerText + ".bmp");
        }
    }
}
