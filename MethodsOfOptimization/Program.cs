using System.Diagnostics;

namespace MethodsOfOptimization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey consoleKey;
            while (true)
            {
                Console.WriteLine($"Enter the number of the algorithm you want to run:" +
                $"\n1. Dijkstra's algorithm" +
                $"\n2. Floyd's algorithm" +
                $"\n3. Kruskal's algorithm" +
                $"\n4. MaximumFlow" +
                $"\n5. NumericalMethodsForFindingStationaryPoints" +
                $"\n6. OneDimensionalOptimization" +
                $"\n7. PenaltyMethod" +
                $"\n8. Prim's algorithm" +
                $"");
                string choosenNumber = Console.ReadLine();
                switch (choosenNumber)
                {
                    case "1":
                        Dijkstra_s_algorithm.Run(); break;
                    case "2":
                        Floyd_s_algorithm.Run(); break;
                    case "3":
                        Kruskal_s_algorithm.Run(); break;
                    case "4":
                        MaximumFlow.Run(); break;
                    case "5":
                        NumericalMethodsForFindingStationaryPoints.Run(); break;
                    case "6":
                        OneDimensionalOptimization.Run(); break;
                    case "7":
                        PenaltyMethod.Run(); break;
                    case "8":
                        Prim_s_algorithm.Run(); break;
                    case "9":
                        Dijkstra_s_algorithm.Run(); break;
                    default:
                        Console.WriteLine("Try again. Enter one of the suggested algorithm numbers.");
                        break;
                    
                }

                Console.WriteLine("Enter any key to restart. Press Escape to exit the application");
                consoleKey = Console.ReadKey().Key;
                if (consoleKey == ConsoleKey.Escape)
                    break;
                Console.ReadLine();
            }
        }
    }
}
