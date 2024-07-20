using System;

public class AllPairShortestPath
{
    readonly static int INF = 999, V = 8;

    void floydWarshall(int[,] graph)
    {
        int[,] dist = new int[V, V];
        int i, j, k;

        // Initialize the solution matrix  
        // same as input graph matrix 
        // Or we can say the initial  
        // values of shortest distances 
        // are based on shortest paths  
        // considering no intermediate 
        // vertex 
        for (i = 0; i < V; i++)
        {
            for (j = 0; j < V; j++)
            {
                dist[i, j] = graph[i, j];
            }
        }

        /* Add all vertices one by one to  
        the set of intermediate vertices. 
        ---> Before start of a iteration, 
             we have shortest distances 
             between all pairs of vertices 
             such that the shortest distances 
             consider only the vertices in 
             set {0, 1, 2, .. k-1} as  
             intermediate vertices. 
        ---> After the end of a iteration,  
             vertex no. k is added 
             to the set of intermediate 
             vertices and the set 
             becomes {0, 1, 2, .. k} */
        for (k = 0; k < V; k++)
        {
            // Pick all vertices as source 
            // one by one 
            for (i = 0; i < V; i++)
            {
                // Pick all vertices as destination 
                // for the above picked source 
                for (j = 0; j < V; j++)
                {
                    // If vertex k is on the shortest 
                    // path from i to j, then update 
                    // the value of dist[i][j] 
                    if (dist[i, k] + dist[k, j] < dist[i, j])
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }
        }

        // Print the shortest distance matrix 
        printSolution(dist);
    }

    void printSolution(int[,] dist)
    {
        Console.WriteLine("Kelesi matrica tobelerdin ar jubi arasindagi en kiska kashiktikti korsetedi:");
        for (int i = 0; i < V; ++i)
        {
            for (int j = 0; j < V; ++j)
            {
                if (dist[i, j] == INF)
                {
                    Console.Write("INF ");
                }
                else
                {
                    Console.Write(dist[i, j] + " ");
                }
            }

            Console.WriteLine();
        }
    }

    public static void Main(string[] args)
    {
        int[,] graph = { {INF, 1, 3, 7, INF, INF, INF, INF, INF, INF, INF},
                        {1, INF, 3, INF, 3, INF, INF, INF, INF, INF, INF},
                        {3, 3, INF, INF, 1, 2, INF, INF, INF, INF, INF},
                        {7, INF, INF, INF, INF, 1, 1, INF, INF, INF, INF},
                        {INF, 1, 1, INF, INF, 2, INF, 7, INF, INF, INF},
                        {INF, INF, 2, 1, 2, INF, 3, 1, 4, INF, INF},
                        {INF, INF, INF, 1, INF, INF, INF, INF, 1, 6, INF},
                        {INF, INF, INF, INF, 7, 1, INF, INF, 2, INF, 3},
                        {INF, INF, INF, INF, INF, 4, 1, 2, INF, 6, 6},
                        {INF, INF, INF, INF, INF, INF, 6, INF, 1, INF, 1},
                        {INF, INF, INF, INF, INF, INF, INF, 3, 6, 1, INF}
                        };

        AllPairShortestPath a = new AllPairShortestPath();

        a.floydWarshall(graph);

        Console.ReadKey();
    }
}