using System;

namespace PenaltyMethod
{
    class Program
    {
        public static double F(double x1, double x2) //your function
        {
            return 0.5 * ((x1 - 1) * (x1 - 1) + x2 * x2);
        }
        public static double g(double x1, double x2, double a) //restriction function
        {
            return -x1 + a * x2 * x2;
        }
        public static double P(double x1, double x2, double r, double a) //penalty function
        {
            return F(x1, x2) + (r / 2) * Math.Pow(g(x1, x2, a), 2);
        }
        public static double X1(double r, double a)
        {
            return 1 - 1 / 2 * a;
        }
        public static double X2(double r, double a)
        {
            return Math.Sqrt((1 / a) - (1 / 2 * a * a) - 1 / (4 * r * a * a));
        }

        static void Main(string[] args)
        {
            ConsoleKeyInfo ForExit;
            do
            {
                Console.WriteLine("Penalty method.");
                int i, N, minF;
                double r, a;
                Console.Write("N = ");
                N = int.Parse(Console.ReadLine());
                Console.Write("a = ");
                a = double.Parse(Console.ReadLine());
                double[] f = new double[N];
                double[] x1 = new double[N];
                double[] x2 = new double[N];
                double[] R = new double[N];
                for (i = 0; i < N; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nr = ");
                    r = double.Parse(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nr = {r}");
                    Console.WriteLine($"F({X1(r, a)}; {X2(r, a)}) = {F(X1(r, a), X2(r, a))}");
                    Console.WriteLine($"P({X1(r, a)}; {X2(r, a)}; {r}) = {P(X1(r, a), X2(r, a), r, a)}");
                    f[i] = F(X1(r, a), X2(r, a));
                    x1[i] = X1(r, a);
                    x2[i] = X2(r, a);
                    R[i] = r;
                }
                minF = 0;
                for (i = 0; i < N; i++)
                {
                    if (f[i] < f[minF]) { minF = i; }
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\nSolution: minFP({x1[minF]}; {x2[minF]}; {R[minF]}) = {f[minF]} in {minF + 1} iteration");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nPress any key to try again. Press Escape to exit.");
                Console.ForegroundColor = ConsoleColor.White;
                ForExit = Console.ReadKey();
                Console.Clear();
            } while (ForExit.Key != ConsoleKey.Escape);

            Console.ReadKey();
        }
    }
}

