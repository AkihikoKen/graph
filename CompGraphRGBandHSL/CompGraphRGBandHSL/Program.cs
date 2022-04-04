using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphRGBandHSL
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> res = hsltoRgb(343, 34, 78);
            //List<double> resHSL = rgbtohsl(45, 33, 56);

            Console.WriteLine("r: " + res[0] + " g: " + res[1] + " b: " + res[2]);
            //Console.WriteLine("h: " + resHSL[0] + " s: " + resHSL[1] + " l: " + resHSL[2]);
            Console.ReadLine();
        }

        static List<double> hsltoRgb(double h, double s, double l)
        {
            List<double> result = new List<double>();

            s = s / 100.0; l = l / 100.0;

            if ((0 <= h & h < 360) & 0 <= s & s <= 1 & l <= 1)
            {
                double r = 0, g = 0, b = 0;

                double c = (1.0 - Math.Abs(2.0 * l - 1.0)) * s;
                double x = c * (1.0 - Math.Abs((h / 60.0) % 2.0 - 1.0));
                double m = l - c / 2.0;


                if (0 <= h & h < 60)
                {
                    r = c; g = x; b = 0;
                }
                if (60 <= h & h < 120)
                {
                    r = x; g = c; b = 0;
                }
                if (120 <= h & h < 180)
                {
                    r = 0; g = c; b = x;
                }
                if (180 <= h & h < 240)
                {
                    r = 0; g = x; b = c;
                }

                if (240 <= h & h < 300)
                {
                    r = x; g = 0; b = c;
                }
                if (300 <= h & h < 360)
                {
                    r = c; g = 0; b = x;
                }

                r = (r + m) * 255;
                g = (g + m) * 255;
                b = (b + m) * 255;

                result.Add(r); result.Add(g); result.Add(b);
            }

            return result;
        }

        static List<double> rgbtohsl(double _r, double _g, double _b)
        {
            List<double> result = new List<double>();
            List<double> listRGB = new List<double>();

            double r = _r / 255.0; listRGB.Add(r);
            double g = _g / 255.0; listRGB.Add(g);
            double b = _b / 255.0; listRGB.Add(b);

            double c_max = listRGB.Max();
            double c_min = listRGB.Min();
            double delta = c_max - c_min;

            double l = (c_max + c_min) / 2.0 * 100.0;

            double h = 0, s = 0;

            if (delta == 0.0)
            {
                s = 0.0;
            }
            if (delta != 0.0)
            {
                s = Math.Abs(delta / (1 - Math.Abs((2 * l) - 1))) * 10000;
            }

            //h
            if (delta == 0.0)
            {
                h = 0.0;
            }
            if (c_max == r)
            {
                h = 60.0 * (((g - b) / delta) % 6.0);
            }
            if (c_max == g)
            {
                h = 60.0 * (((b - r) / delta) + 2.0);
            }
            if (c_max == b)
            {
                h = 60.0 * (((r - g) / delta) + 4.0);
            }

            result.Add(h); result.Add(s); result.Add(l);

            return result;
        }
    }
}
