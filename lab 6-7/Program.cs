using ScottPlot.Interactivity.UserActionResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6_7
{
    public class Program
    {
        public static List<List<double>> IPL(double x0, double x1, double x2, double y0, double y1, double y2, int iters, double a, double b)
        {
            double a0 = y0 / ((x0 - x1) * (x0 - x2));
            double a1 = y1 / ((x1 - x0) * (x1 - x2));
            double a2 = y2 / ((x2 - x0) * (x2 - x1));

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>()); 
            res.Add(new List<double>()); 
            res.Add(new List<double>());

            double y;

            double x = a; 
            for (int i = 0; i <= iters; i++)
            {
                y = a0 * (x - x1) * (x - x2) + a1 * (x - x0) * (x - x2) + a2 * (x - x0) * (x - x1);

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0);

                x = x + ((b - a) / iters);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IPL_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> IN(double x0, double x1, double x2, double y0, double y1, double y2, int iters, double a, double b)
        {
            double dy0 = y1 - y0;
            double dy1 = y2 - y1;
            double d2y0 = dy1 - dy0;

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            double x = a;
            for (int i = 0; i <= iters; i++)
            {
                double t = (x - x0) / (Math.Abs((a-b))/2);

                y = y0 + t*dy0 + (t * (t-1) * d2y0)/2;

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(t);

                x = x + ((b - a) / iters);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IN_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> IIN(double x0, double x1, double x2, double y0, double y1, double y2, int iters, double a, double b)
        {
            double dy0 = y1 - y0;
            double dy1 = y2 - y1;
            double d2y0 = dy1 - dy0;

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            double x = a;
            for (int i = 0; i <= iters; i++)
            {
                double t = (x - x2)/ (Math.Abs((a - b)) / 2);

                y = y2 + t * dy1 + (t * (t + 1) * d2y0) / 2;

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(t);

                x = x + ((b - a) / iters);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IIN_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> AM(double x0, double x1, double x2, double y0, double y1, double y2, int iters, double a, double b)
        {
            double c00 = 3.0;
            double c01 = x0 + x1 + x2;
            double c02 = Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2);
            double c10 = x0 + x1 + x2;
            double c11 = Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2);
            double c12 = Math.Pow(x0, 3) + Math.Pow(x1, 3) + Math.Pow(x2, 3);
            double c20 = Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2);
            double c21 = Math.Pow(x0, 3) + Math.Pow(x1, 3) + Math.Pow(x2, 3);
            double c22 = Math.Pow(x0, 4) + Math.Pow(x1, 4) + Math.Pow(x2, 4);

            double cnst0 = y0 + y1 + y2;
            double cnst1 = x0 * y0 + x1 * y1 + x2 * y2;
            double cnst2 = Math.Pow(x0, 2) * y0 + Math.Pow(x1, 2) * y1 + Math.Pow(x2, 2) * y2;

            double D = c00 * (c11 * c22 - c21 * c12)
                       - c01 * (c10 * c22 - c20 * c12)
                       + c02 * (c10 * c21 - c20 * c11);

            double D1 = cnst0 * (c11 * c22 - c21 * c12)
                        - c01 * (cnst1 * c22 - cnst2 * c12)
                        + c02 * (cnst1 * c21 - cnst2 * c11);
            
            double D2 = c00 * (cnst1 * c22 - cnst2 * c12)
                        - cnst0 * (c10 * c22 - c20 * c12)
                        + c02 * (c10 * cnst2 - c20 * cnst1);

            double D3 = c00 * (c11 * cnst2 - c21 * cnst1)
                       - c01 * (c10 * cnst2 - c20 * cnst1)
                       + cnst0 * (c10 * c21 - c20 * c11);

            double a0 = D1 / D;
            double a1 = D2 / D;
            double a2 = D3 / D;

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            double x = a;
            for (int i = 0; i <= iters; i++)
            {
                y = a2 * Math.Pow(x, 2) + a1 * x + a0;
                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0);

                x = x + ((b - a) / iters);
            }
        ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("AM_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> F(int iters, double a, double b)  
        {
            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());
            
            double y;

            double x = a;
            for (int i = 0; i <= iters; i++)
            {
                y = 2.4*Math.Cos(Math.Exp(-1*(x/3)));

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0);

                x = x + ((b - a) / iters);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("F_plot.png", 1080, 1080);

            return res; 
        }

        static void Main(string[] args)
        {
            double a = 0.3;
            double b = 0.9;

            double x0 = a;
            double x1 = (a + b) / 2;
            double x2 = b;
            double y0 = 2.4 * Math.Cos(Math.Exp(-1 * (x0 / 3)));
            double y1 = 2.4 * Math.Cos(Math.Exp(-1 * (x1 / 3)));
            double y2 = 2.4 * Math.Cos(Math.Exp(-1 * (x2 / 3)));
            
            int iters = 6;

            Console.WriteLine("n\t|      xn      |     f(xn)    |    L2(xn)    ||f(xn)-L2(xn)||    N1(xn)    ||f(xn)-N1(xn)||    N2(xn)    ||f(xn)-N2(xn)||    P2(xn)    ||f(xn)-P2(xn)||       t      |       t'     |");
            Console.WriteLine("________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|");
            
            List<List<double>> resF = F(iters, a, b);
            List<List<double>> resIPL = IPL(x0, x1, x2, y0, y1, y2, iters, a, b);
            List<List<double>> resIN = IN(x0, x1, x2, y0, y1, y2, iters, a, b);
            List<List<double>> resIIN = IIN(x0, x1, x2, y0, y1, y2, iters, a, b);
            List<List<double>> resAM = AM(x0, x1, x2, y0, y1, y2, iters, a, b); 

            for (int i = 0; i < iters+1; i++)
            {
                Console.Write((i+1).ToString() + "\t"); 
                Console.Write("| " +  resF[0][i].ToString($"F{11}"));
                Console.Write("| " + resF[1][i].ToString($"F{11}"));
                Console.Write("| " + resIPL[1][i].ToString($"F{11}"));
                Console.Write("| " + (Math.Abs(resF[1][i] - resIPL[1][i])).ToString($"F{11}"));
                Console.Write("| " + resIN[1][i].ToString($"F{11}"));
                Console.Write("| " + (Math.Abs(resF[1][i] - resIN[1][i])).ToString($"F{11}"));
                Console.Write("| " + resIIN[1][i].ToString($"F{11}"));
                Console.Write("| " + (Math.Abs(resF[1][i] - resIIN[1][i])).ToString($"F{11}"));
                Console.Write("| " + resAM[1][i].ToString($"F{11}"));
                Console.Write("| " + (Math.Abs(resF[1][i] - resAM[1][i])).ToString($"F{11}"));
                Console.Write("| " + resIN[2][i].ToString($"F{11}")); 
                Console.Write("| " + resIIN[2][i].ToString($"F{11}"));
                Console.WriteLine(); 
            }
        }
    }
}
