using System;
using System.Collections.Generic;

public class MaximumFlow
{
    static readonly int V = 8; //Графикте төбелердің саны 

    /*  true қайтарады,
     * егер қалған графта 's' бастауынан 't' науасына жол болса. 
     * parent [] жолды сақтау үшін толтырылып отырады */
    bool bfs(int[,] rGraph, int s, int t, int[] parent) //breadth-first search - енінен іздеу
    {
        //Жүріп шыққан төбелер массивін құрамыз және барлық төбелерін жүріп шықпаған деп беріп кетеміз
        bool[] visited = new bool[V];
        for (int i = 0; i < V; ++i)
            visited[i] = false;

        //Кезек құру, кезекке ағымдағы төбені қойып және оны жүріп шыққан деп беру
        List<int> queue = new List<int>();
        queue.Add(s);
        visited[s] = true;
        parent[s] = -1;

        // Стандартты BFS циклі 
        while (queue.Count != 0)
        {
            int u = queue[0];
            queue.RemoveAt(0);

            for (int v = 0; v < V; v++)
            {
                if (visited[v] == false && rGraph[u, v] > 0)
                {
                    queue.Add(v);
                    parent[v] = u;
                    visited[v] = true;
                }
            }
        }

        // Егер біз бастаудан BFS-те науаға жетсек, true қайтарамыз, кері жағдайда false
        return (visited[t] == true);
    }

    //Берілген графтағы s-тен t-ға дейінгі максимал тасқынды қайтарады
    int FordFulkerson(int[,] graph, int s, int t)
    {
        int u, v;

        // Қалдық графты құру және графтағы берілген көлемдермен толтыру 

        // Қалдық граф rGraph[i,j] i-ден j-ге дейінгі қабырғаның қалдық көлемін көрсетеді
        // (егер қабырға бар болса. Егер i-ден j-ге дейін жол болмаса, rGraph[i,j] = 0). 


        int[,] rGraph = new int[V, V];

        for (u = 0; u < V; u++)
            for (v = 0; v < V; v++)
                rGraph[u, v] = graph[u, v];

        // Бұл массив BFS-пен толтырылады және жолымызды сақтайды
        int[] parent = new int[V];

        int max_flow = 0; // Бастапқы максимал тасқын жоқ

        // Бастаудан науаға шейін жол болып жатса, тасқынды ұлғайтамыз

        while (bfs(rGraph, s, t, parent))
        {
            // Қабырғалардың минималды қалдық көлемін BFS-пен толтырылған жол бойымен табамыз
            // Немесе, қарапайым тілмен айтқанда, максималды тасқынды табылған жол арқылы табамыз
            int path_flow = int.MaxValue;
            for (v = t; v != s; v = parent[v])
            {
                u = parent[v];
                path_flow = Math.Min(path_flow, rGraph[u, v]);
            }

            // Қабырғалардың және кері қабырғалардың қалдық көлемін жол бойымен жаңартамыз
            for (v = t; v != s; v = parent[v])
            {
                u = parent[v];
                rGraph[u, v] -= path_flow;
                rGraph[v, u] += path_flow;
            }

            // Жалпы тасқынға жол тасқынын қосамыз
            max_flow += path_flow;
        }

        // Жалпы тасқынды қайтарамыз
        return max_flow;
    }
    public static void Run()
    {
        //Графымызды құрамыз
        /*int[,] graph1 = new int[,] { {0, 16, 13, 0, 0, 0, 0, 0 },
                                    {0, 0, 10, 12, 0, 0, 0, 0 },
                                    {0, 4, 0, 0, 14, 0, 0, 0 },
                                    {0, 0, 9, 0, 0, 20, 0, 0 },
                                    {0, 0, 0, 7, 0, 4, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 }
                                };
        int[,] graph2 = new int[,] { {0, 20, 30, 10, 0, 0, 0, 0 },
                                    {0, 0, 40, 0, 30, 0, 0, 0},
                                    {0, 0, 0, 10, 20, 0, 0, 0},
                                    {0, 0, 5, 0, 20, 0, 0, 0},
                                    {0, 0, 0, 0, 0, 0, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 },
                                    {0, 0, 0, 0, 0, 0, 0, 0 }
                                };*/
        int[,] graph = new int[,] { {0, 4, 5, 8, 0, 0, 0, 0 },
                                    {4, 0, 4, 0, 6, 7, 0, 0 },
                                    {5, 4, 0, 2, 0, 4, 6, 0 },
                                    {8, 0, 2, 0, 0, 0, 3, 0 },
                                    {0, 6, 0, 0, 0, 0, 0, 5 },
                                    {0, 7, 4, 0, 4, 0, 9, 4 },
                                    {0, 0, 6, 3, 0, 7, 0, 4 },
                                    {0, 0, 0, 0, 5, 4, 4, 0 }
                                 };

        /*MaxFlow m1 = new MaxFlow();
        MaxFlow m2 = new MaxFlow();
        Console.WriteLine("Maksimal mumkin taskin " +
                        m1.FordFulkerson(graph1, 0, 5));
        Console.WriteLine("Maksimal mumkin taskin " +
                        m2.FordFulkerson(graph2, 0, 4));*/
        MaximumFlow m = new MaximumFlow();
        Console.WriteLine("Maksimal mumkin taskin " +
        m.FordFulkerson(graph, 0, 7));
        Console.ReadKey();
    }
}