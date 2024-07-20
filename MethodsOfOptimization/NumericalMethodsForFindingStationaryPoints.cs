using System;

class NumericalMethodsForFindingStationaryPoints
{
    public static double J(double u1, double u2) //your function
    {
        return 19 * u1 - 0.1 * u2 + Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
    }
    public static double[,] Multiplication(double[,] a, double[,] b)
    {
        if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Matrices cannot be multiplied");
        double[,] r = new double[a.GetLength(0), b.GetLength(1)];
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < b.GetLength(1); j++)
            {
                for (int k = 0; k < b.GetLength(0); k++)
                {
                    r[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return r;
    }
    public static double[,] MultiplicationByNumber(double[,] a, double c)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                a[i, j] *= c;
            }
        }
        return a;
    }
    public static double Det(double[,] H)
    {
        return H[0, 0] * H[1, 1] - H[1, 0] * H[0, 1];
    }
    public static double[,] Inverse(double[,] H)
    {
        double[,] InvM = new double[,] {   { H[1, 1], -H[1, 0]},
                                      { -H[0, 1], H[0, 0] }   };
        return MultiplicationByNumber(InvM, 1 / Det(H));
    }
    public static void Run()
    {
        int t;
        ConsoleKeyInfo ForExit;
        do
        {
            Console.WriteLine("Choose a way to solve the problem:\n1)Gradient descent method;\n2)Newton's method.");
            Console.Write("Enter the relevant number: ");
            t = int.Parse(Console.ReadLine());
            if (t == 1)
            {
                int k = 0;
                double u1, u2, eps, delta, un1, un2, grad1, grad2;
                Console.Write("u1 = ");
                u1 = double.Parse(Console.ReadLine());
                Console.Write("u2 = ");
                u2 = double.Parse(Console.ReadLine());
                Console.Write("eps = ");
                eps = double.Parse(Console.ReadLine());
                Console.Write("delta = ");
                delta = double.Parse(Console.ReadLine());
                do
                {
                    k++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{k}-iteration\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"U{k}=({Math.Round(u1, 3)} ; {Math.Round(u2, 3)})T");
                    grad1 = 19 + 7.62 * u1 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    grad2 = -0.1 + 5.8 * u2 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    if (Math.Sqrt(grad1 * grad1 + grad2 * grad2) < eps) { Console.WriteLine($"||grad{k}|| = {Math.Sqrt(grad1 * grad1 + grad2 * grad2)} < {eps}"); break; }
                    un1 = u1 - delta * grad1;
                    un2 = u2 - delta * grad2;
                    Console.WriteLine($"U{k + 1}=({Math.Round(un1, 3)}; {Math.Round(un2, 3)})T");
                    Console.WriteLine($"delta = {delta}");
                    Console.WriteLine($"grad{k}=({grad1}; {grad2})T , ||grad{k}|| = {Math.Round(Math.Sqrt(grad1 * grad1 + grad2 * grad2), 3)} > {eps}");
                    Console.WriteLine($"J({Math.Round(u1, 3)}; {Math.Round(u2, 3)}) = {Math.Round(J(u1, u2), 3)}, J({Math.Round(un1, 3)}; {Math.Round(un2, 3)}) = {Math.Round(J(un1, un2), 3)}");
                    if (J(u1, u2) > J(un1, un2)) { u1 = un1; u2 = un2; }
                    else { delta /= 2; }
                } while (Math.Sqrt(grad1 * grad1 + grad2 * grad2) > eps);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\nSolution: minJ({u1}; {u2}) = {J(u1, u2)} in {k} iteration");
                Console.ForegroundColor = ConsoleColor.White;
            }

            else if (t == 2)
            {
                int k = 0;
                double u1, u2, eps, un1, un2, grad1, grad2;
                Console.Write("u1 = ");
                u1 = double.Parse(Console.ReadLine());
                Console.Write("u2 = ");
                u2 = double.Parse(Console.ReadLine());
                Console.Write("eps = ");
                eps = double.Parse(Console.ReadLine());
                double[,] H = new double[2, 2];
                double[,] Ju = new double[2, 1];
                double[,] HJ = new double[2, 1];
                double[,] InvH = new double[2, 2];
                do
                {
                    k++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{k}-iteration\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    grad1 = 19 + 7.62 * u1 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    grad2 = -0.1 + 5.8 * u2 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    Console.WriteLine($"U{k}=({Math.Round(u1, 3)} ; {Math.Round(u2, 3)})T");
                    if (Math.Sqrt(grad1 * grad1 + grad2 * grad2) < eps) { Console.WriteLine($"\n||grad{k}|| = {Math.Round(Math.Sqrt(grad1 * grad1 + grad2 * grad2), 6)} < {eps}"); break; }
                    Console.WriteLine($"\ngrad{k}=({Math.Round(grad1, 6)}; {Math.Round(grad2, 6)})T , ||grad{k}|| = {Math.Round(Math.Sqrt(grad1 * grad1 + grad2 * grad2), 6)} > {eps}");
                    Ju[0, 0] = grad1;
                    Ju[1, 0] = grad2;
                    H[0, 0] = (7.62 + 58.0644 * u1 * u1) * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    H[0, 1] = 44.196 * u1 * u2 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    H[1, 0] = 44.196 * u1 * u2 * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    H[1, 1] = (5.8 + 33.64 * u2 * u2) * Math.Exp(3.81 * u1 * u1 + 2.9 * u2 * u2);
                    InvH = Inverse(H);
                    HJ = Multiplication(InvH, Ju);
                    un1 = u1 - HJ[0, 0];
                    un2 = u2 - HJ[1, 0];
                    Console.WriteLine($"\nH(U)^-1=|{Math.Round(InvH[0, 0], 5)}, {Math.Round(InvH[0, 1], 5)}|\n        |{Math.Round(InvH[1, 0], 5)}, {Math.Round(InvH[1, 1], 5)}|\n");
                    u1 = un1;
                    u2 = un2;
                } while (Math.Sqrt(grad1 * grad1 + grad2 * grad2) > eps);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\nSolution: minJ({u1};{u2}) = {J(u1, u2)} in {k} iteration");
                Console.ForegroundColor = ConsoleColor.White;
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid number");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPress any key to try again. Press Escape to exit.");
            Console.ForegroundColor = ConsoleColor.White;
            ForExit = Console.ReadKey();
            Console.Clear();

        } while (ForExit.Key != ConsoleKey.Escape);

        Console.ReadKey();

    }
}