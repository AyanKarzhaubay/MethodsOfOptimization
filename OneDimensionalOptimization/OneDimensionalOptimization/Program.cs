using System;

namespace OneDimensionalOptimization
{
    class Program
    {
        public static double J(double u) //your function
        {
            return u * u + 2 * u + 2;
        }
        static void Main()
        {
            int t;
            double a = -1, b = 1; //your search interval
            Console.WriteLine("Choose a way to solve the problem:\n1)Dichotomous search;\n2)Golden ratio;\n3)The method of dividing the interval in half;\n4)Uniform search.");
            t = int.Parse(Console.ReadLine());
            switch(t)
            {
                case 1:
                    DichotomousSearch(a, b);
                    break;
                case 2:
                    GoldenRatio(a, b);
                    break;
                case 3:
                    DividingTheIntervalInHalf(a, b);
                    break;
                case 4:
                    UniformSearch(a, b);
                    break;
                default: 
                    Console.WriteLine("Invalid number. Try again");
                    break;
            }
            Console.ReadKey();

        }

        public static void DichotomousSearch(double a, double b)
        {
            int k = 0;
            double l, m, eps, delta;
            Console.Write("eps = ");
            eps = double.Parse(Console.ReadLine());
            Console.Write("delta = ");
            delta = double.Parse(Console.ReadLine());
            Console.WriteLine();
            while (Math.Abs(b - a) > eps)
            {
                Console.WriteLine($"{k + 1}-iteration");
                l = ((a + b) / 2) - delta;
                m = ((a + b) / 2) + delta;
                Console.WriteLine($"a = {a}, b = {b}");
                Console.WriteLine($"l = {l}, m = {m}\n");
                if (J(l) < J(m)) { b = m; }
                else { a = l; }
                k++;
            }
            Console.WriteLine($"minJ({(a + b) / 2}) = {J((a + b) / 2)}, on {k}-iteration");
        }
        public static void GoldenRatio(double a, double b)
        {
            int k = 0;
            double l, m, eps, ak;
            ak = (Math.Sqrt(5) - 1) / 2;
            Console.Write("eps = ");
            eps = double.Parse(Console.ReadLine());
            Console.WriteLine();
            while (Math.Abs(b - a) > eps)
            {
                Console.WriteLine($"{k + 1}-iteration");
                k++;
                l = a + (1 - ak) * (b - a);
                m = a + ak * (b - a);
                Console.WriteLine($"a = {a}, b = {b}");
                Console.WriteLine($"l = {l}, m = {m}\n");
                if (J(l) <= J(m)) { b = m; m = l; l = a + (1 - ak) * (b - a); }
                else { a = l; l = m; m = a + ak * (b - a); }
            }
            Console.WriteLine($"minJ({(a + b) / 2}) = {J((a + b) / 2)}, on {k}-iteration");
        }
        public static void DividingTheIntervalInHalf(double a, double b)
        {
            double avgu, L, y, z, eps;
            int k = 0;
            Console.Write("eps = ");
            eps = double.Parse(Console.ReadLine());
            Console.WriteLine();
            do
            {
                Console.WriteLine($"{k + 1}-iteration");
                L = b - a;
                avgu = (a + b) / 2;
                y = a + L / 4;
                z = b - L / 4;
                Console.WriteLine($"avgu = {avgu}, L = {L}");
                Console.WriteLine($"a = {a}, b = {b}");
                Console.WriteLine($"y = {y}, z = {z}\n");
                if (J(y) < J(avgu)) { b = avgu; }
                else if (J(z) < J(avgu)) { a = avgu; }
                else { a = y; b = z; }
                k++;
            } while (Math.Abs(L) > eps);
            Console.WriteLine($"minJ({(a + b) / 2}) = {J((a + b) / 2)}, on {k}-iteration");
        }
        public static void UniformSearch(double a, double b)
        {
            int minu, N, i;
            Console.Write("N = ");
            N = int.Parse(Console.ReadLine());
            double[] u = new double[N];
            double[] Ju = new double[N];
            for (i = 0; i < N; i++)
            {
                u[i] = a + (i + 1) * (b - a) / (N + 1);
                Ju[i] = J(u[i]);
                Console.WriteLine($"J({u[i]}) = {J(u[i])}");
            }
            minu = 0;
            for (i = 0; i < N; i++)
            {
                if (Ju[i] < Ju[minu]) { minu = i; }
            }
            Console.WriteLine($"\nminJ({u[minu]}) = {J(u[minu])}");
        }
    }
}