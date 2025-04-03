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
        public static List<List<double>> IPL(double[,] xy0, int iters, double a, double b)
        {
            double x0 = xy0[0, 0];
            double y0 = xy0[1, 0];
            double x1 = xy0[0, 1];
            double y1 = xy0[1, 1];
            double x2 = xy0[0, 2];
            double y2 = xy0[1, 2];

            double a0 = y0 / ((x0 - x1) * (x0 - x2));
            double a1 = y1 / ((x1 - x0) * (x1 - x2));
            double a2 = y2 / ((x2 - x0) * (x2 - x1));

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>()); 
            res.Add(new List<double>()); 
            res.Add(new List<double>());

            double y;            

            for (double x = a; x <= b; x = x + (b/iters))
            {
                y = a0 * (x - x1) * (x - x2) + a1 * (x - x0) * (x - x2) + a2 * (x - x0) * (x - x1); 

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IPL_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> IN(double[,] xy0, int iters, double a, double b)
        {
            double x0 = xy0[0, 0];
            double y0 = xy0[1, 0];
            double x1 = xy0[0, 1];
            double y1 = xy0[1, 1];
            double x2 = xy0[0, 2];
            double y2 = xy0[1, 2];

            double dy0 = y1 - y0;
            double dy1 = y2 - y1;
            double d2y0 = dy1 - dy0;

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            for (double x = a; x <= b; x = x + (b /iters))
            {
                double t = (x - x0) * (4 / Math.PI);

                y = y0 + t * dy0 + (t * (t - 1) * d2y0) / 2;

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(t); 
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IN_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> IIN(double[,] xy0, int iters, double a, double b)
        {
            double x0 = xy0[0, 0];
            double y0 = xy0[1, 0];
            double x1 = xy0[0, 1];
            double y1 = xy0[1, 1];
            double x2 = xy0[0, 2];
            double y2 = xy0[1, 2];

            double dy0 = y1 - y0;
            double dy1 = y2 - y1;
            double d2y0 = dy1 - dy0;

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            for (double x = a; x <= b; x = x + (b / iters))
            {
                double t = (x - x2) * (4 / Math.PI);

                y = y2 + t * dy1 + (t * (t + 1) * d2y0) / 2;

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(t); 
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("IIN_plot.png", 1080, 1080);

            return res; 
        }

        public static List<List<double>> AM(double[,] xy0, double[] aAM, int iters, double a, double b)
        {
            double a0 = aAM[0];
            double a1 = aAM[1];
            double a2 = aAM[2];

            List<List<double>> res = new List<List<double>>();
            res.Add(new List<double>());
            res.Add(new List<double>());
            res.Add(new List<double>());

            double y;

            for (double x = a; x <= b; x = x + (b / iters))
            {
                y = a2 * Math.Pow(x, 2) + a1 * x + a0;
                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0); 
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

            for (double x = a; x <= b; x = x + (b / iters))
            {
                y = 2.4*Math.Cos(Math.Exp(-1*(x/3)));

                res[0].Add(x);
                res[1].Add(y);
                res[2].Add(0);
            }

            ScottPlot.Plot plot = new ScottPlot.Plot();
            plot.Add.Scatter(res[0], res[1]);
            plot.SavePng("F_plot.png", 1080, 1080);

            return res; 
        }

        static double[] KramerSolve(double[,] coefficients, double[] constants)
        {
            try
            {
                return SolveSystem(coefficients, constants);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new double[] { 0, 0, 0 }; 
            }
        }

        static double[] SolveSystem(double[,] coefficients, double[] constants)
        {
            double D = Determinant(coefficients);

            if (D == 0)
                throw new InvalidOperationException("Система не имеет единственного решения.");

            double D1 = Determinant(CreateModifiedMatrix(coefficients, constants, 0));
            double D2 = Determinant(CreateModifiedMatrix(coefficients, constants, 1));
            double D3 = Determinant(CreateModifiedMatrix(coefficients, constants, 2));

            double x = D1 / D;
            double y = D2 / D;
            double z = D3 / D;

            return new double[] { x, y, z };
        }

        static double[,] CreateModifiedMatrix(double[,] coefficients, double[] constants, int column)
        {
            double[,] modifiedMatrix = (double[,])coefficients.Clone();

            for (int i = 0; i < coefficients.GetLength(0); i++)
                modifiedMatrix[i, column] = constants[i];

            return modifiedMatrix;
        }

        static double Determinant(double[,] matrix)
        {
            return matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1])
                   - matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0])
                   + matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
        }

        static void Main(string[] args)
        {
            double[,] xy0 = new double[2, 3] { { 0, Math.PI/4, Math.PI/2 }, { 2.4 * Math.Cos(Math.Exp(-1*(0 / 3))), 2.4 * Math.Cos(Math.Exp(-1 * (Math.PI/4 / 3))), 2.4 * Math.Cos(Math.Exp(-1 * (Math.PI/2 / 3))) } };

            double x0 = xy0[0, 0];
            double y0 = xy0[1, 0];
            double x1 = xy0[0, 1];
            double y1 = xy0[1, 1];
            double x2 = xy0[0, 2];
            double y2 = xy0[1, 2];

            //вычислить коэффициенты для крамера здесь 
            double[,] coefficients = {
                { 
                    3.0, 
                    x0 + x1 + x2,
                    Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2),
                }, 
                { 
                    x0 + x1 + x2,
                    Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2), 
                    Math.Pow(x0, 3) + Math.Pow(x1, 3) + Math.Pow(x2, 3) 
                },
                {
                    Math.Pow(x0, 2) + Math.Pow(x1, 2) + Math.Pow(x2, 2),
                    Math.Pow(x0, 3) + Math.Pow(x1, 3) + Math.Pow(x2, 3), 
                    Math.Pow(x0, 4) + Math.Pow(x1, 4) + Math.Pow(x2, 4)
                }
            };
            double[] constants = { 
                y0 + y1 + y2, 
                x0*y0 + x1*y1 + x2*y2,
                Math.Pow(x0, 2)*y0 + Math.Pow(x1, 2)*y1 + Math.Pow(x2, 2)*y2
            };

            double[] aAM = KramerSolve(coefficients, constants); 
            
            double a = 0;
            double b = Math.PI / 2; 
            
            int iters = 20; 

            Console.WriteLine("n\t|      xn      |     f(xn)    |    L2(xn)    ||f(xn)-L2(xn)||    N1(xn)    ||f(xn)-N1(xn)||    N2(xn)    ||f(xn)-N2(xn)||    P2(xn)    ||f(xn)-P2(xn)||       t      |       t'     |");
            Console.WriteLine("________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|______________|");
            
            List<List<double>> resF = F(iters-1, a, b);
            List<List<double>> resIPL = IPL(xy0, iters-1, a, b);
            List<List<double>> resIN = IN(xy0, iters - 1, a, b);
            List<List<double>> resIIN = IIN(xy0, iters - 1, a, b);
            List<List<double>> resAM = AM(xy0, aAM, iters - 1, a, b); 

            for (int i = 0; i < iters; i++)
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
