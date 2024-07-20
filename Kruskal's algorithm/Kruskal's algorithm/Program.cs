// C# Code for above approach 
using System;

class Graph
{

    // A class to represent a graph edge 
    class Edge : IComparable<Edge>
    {
        public int src, dest, weight;

        // Comparator function used for sorting edges 
        // based on their weight 
        public int CompareTo(Edge compareEdge)
        {
            return this.weight - compareEdge.weight;
        }
    }

    // A class to represent a subset for union-find 
    public class subset
    {
        public int parent, rank;
    };

    int V, E; // V-> no. of vertices & E->no.of edges 
    Edge[] edge; // collection of all edges 

    // Creates a graph with V vertices and E edges 
    Graph(int v, int e)
    {
        V = v;
        E = e;
        edge = new Edge[E];
        for (int i = 0; i < e; ++i)
            edge[i] = new Edge();
    }

    // A utility function to find set of an element i 
    // (uses path compression technique) 
    int find(subset[] subsets, int i)
    {
        // find root and make root as 
        // parent of i (path compression) 
        if (subsets[i].parent != i)
            subsets[i].parent = find(subsets,
                                    subsets[i].parent);

        return subsets[i].parent;
    }

    // A function that does union of 
    // two sets of x and y (uses union by rank) 
    void Union(subset[] subsets, int x, int y)
    {
        int xroot = find(subsets, x);
        int yroot = find(subsets, y);

        // Attach smaller rank tree under root of 
        // high rank tree (Union by Rank) 
        if (subsets[xroot].rank < subsets[yroot].rank)
            subsets[xroot].parent = yroot;
        else if (subsets[xroot].rank > subsets[yroot].rank)
            subsets[yroot].parent = xroot;

        // If ranks are same, then make one as root 
        // and increment its rank by one 
        else
        {
            subsets[yroot].parent = xroot;
            subsets[xroot].rank++;
        }
    }

    // The main function to construct MST 
    // using Kruskal's algorithm 
    void KruskalMST()
    {
        Edge[] result = new Edge[V]; // This will store the resultant MST 
        int e = 0; // An index variable, used for result[] 
        int i = 0; // An index variable, used for sorted edges 
        for (i = 0; i < V; ++i)
            result[i] = new Edge();

        // Step 1: Sort all the edges in non-decreasing 
        // order of their weight. If we are not allowed 
        // to change the given graph, we can create 
        // a copy of array of edges 
        Array.Sort(edge);

        // Allocate memory for creating V ssubsets 
        subset[] subsets = new subset[V];
        for (i = 0; i < V; ++i)
            subsets[i] = new subset();

        // Create V subsets with single elements 
        for (int v = 0; v < V; ++v)
        {
            subsets[v].parent = v;
            subsets[v].rank = 0;
        }

        i = 0; // Index used to pick next edge 

        // Number of edges to be taken is equal to V-1 
        while (e < V - 1)
        {
            // Step 2: Pick the smallest edge. And increment 
            // the index for next iteration 
            Edge next_edge = new Edge();
            next_edge = edge[i++];

            int x = find(subsets, next_edge.src);
            int y = find(subsets, next_edge.dest);

            // If including this edge does't cause cycle, 
            // include it in result and increment the index 
            // of result for next edge 
            if (x != y)
            {
                result[e++] = next_edge;
                Union(subsets, x, y);
            }
            // Else discard the next_edge 
        }

        // print the contents of result[] to display 
        // the built MST 
        Console.WriteLine("Following are the edges in " +
                                "the constructed MST");
        for (i = 0; i < e; ++i)
            Console.WriteLine(result[i].src + " -- " +
            result[i].dest + " == " + result[i].weight);
        Console.ReadLine();
    }

    // Driver Code 
    public static void Main(String[] args)
    {

        /* Let us create following weighted graph 
                10 
            0--------1 
            | \ | 
        6| 5\ |15 
            | \ | 
            2--------3 
                4 */
        int V = 9; // Number of vertices in graph 
        int E = 36; // Number of edges in graph 
        Graph graph = new Graph(V, E);

        // add edge 0-1 
        graph.edge[0].src = 0;
        graph.edge[0].dest = 1;
        graph.edge[0].weight = 50;

        // add edge 0-2 
        graph.edge[1].src = 0;
        graph.edge[1].dest = 2;
        graph.edge[1].weight = 70;

        // add edge 0-3 
        graph.edge[2].src = 0;
        graph.edge[2].dest = 3;
        graph.edge[2].weight = 75;

        // add edge 0-4
        graph.edge[3].src = 0;
        graph.edge[3].dest = 4;
        graph.edge[3].weight = 45;

        // add edge 0-5
        graph.edge[4].src = 0;
        graph.edge[4].dest = 5;
        graph.edge[4].weight = 90;

        // add edge 0-6 
        graph.edge[5].src = 0;
        graph.edge[5].dest = 6;
        graph.edge[5].weight = 85;

        // add edge 0-7
        graph.edge[6].src = 0;
        graph.edge[6].dest = 7;
        graph.edge[6].weight = 80;

        // add edge 0-8 
        graph.edge[7].src = 0;
        graph.edge[7].dest = 8;
        graph.edge[7].weight = 5;

        graph.edge[8].src = 1;
        graph.edge[8].dest = 2;
        graph.edge[8].weight = 25;

        graph.edge[9].src = 1;
        graph.edge[9].dest = 3;
        graph.edge[9].weight = 35;

        graph.edge[10].src = 1;
        graph.edge[10].dest = 4;
        graph.edge[10].weight = 55;

        graph.edge[11].src = 1;
        graph.edge[11].dest = 5;
        graph.edge[11].weight = 60;

        graph.edge[12].src = 1;
        graph.edge[12].dest = 6;
        graph.edge[12].weight = 10;

        graph.edge[13].src = 1;
        graph.edge[13].dest = 7;
        graph.edge[13].weight = 85;

        graph.edge[14].src = 1;
        graph.edge[14].dest = 8;
        graph.edge[14].weight = 70;

        graph.edge[15].src = 2;
        graph.edge[15].dest = 3;
        graph.edge[15].weight = 50;

        graph.edge[16].src = 2;
        graph.edge[16].dest = 4;
        graph.edge[16].weight = 1000;

        graph.edge[17].src = 2;
        graph.edge[17].dest = 5;
        graph.edge[17].weight = 30;

        graph.edge[18].src = 2;
        graph.edge[18].dest = 6;
        graph.edge[18].weight = 90;

        graph.edge[19].src = 2;
        graph.edge[19].dest = 7;
        graph.edge[19].weight = 80;

        graph.edge[20].src = 2;
        graph.edge[20].dest = 8;
        graph.edge[20].weight = 35;

        graph.edge[21].src = 3;
        graph.edge[21].dest = 4;
        graph.edge[21].weight = 25;

        graph.edge[22].src = 3;
        graph.edge[22].dest = 5;
        graph.edge[22].weight = 5;

        graph.edge[23].src = 3;
        graph.edge[23].dest = 6;
        graph.edge[23].weight = 55;

        graph.edge[24].src = 3;
        graph.edge[24].dest = 7;
        graph.edge[24].weight = 60;

        graph.edge[25].src = 3;
        graph.edge[25].dest = 8;
        graph.edge[25].weight = 65;

        graph.edge[26].src = 4;
        graph.edge[26].dest = 5;
        graph.edge[26].weight = 45;

        graph.edge[27].src = 4;
        graph.edge[27].dest = 6;
        graph.edge[27].weight = 75;

        graph.edge[28].src = 4;
        graph.edge[28].dest = 7;
        graph.edge[28].weight = 95;

        graph.edge[29].src = 4;
        graph.edge[29].dest = 8;
        graph.edge[29].weight = 40;

        graph.edge[30].src = 5;
        graph.edge[30].dest = 6;
        graph.edge[30].weight = 15;

        graph.edge[31].src = 5;
        graph.edge[31].dest = 7;
        graph.edge[31].weight = 20;

        graph.edge[32].src = 5;
        graph.edge[32].dest = 8;
        graph.edge[32].weight = 90;

        graph.edge[33].src = 6;
        graph.edge[33].dest = 7;
        graph.edge[33].weight = 30;

        graph.edge[34].src = 6;
        graph.edge[34].dest = 8;
        graph.edge[34].weight = 1000;

        graph.edge[35].src = 7;
        graph.edge[35].dest = 8;
        graph.edge[35].weight = 70;


        graph.KruskalMST();

        Console.ReadKey();
    }
}