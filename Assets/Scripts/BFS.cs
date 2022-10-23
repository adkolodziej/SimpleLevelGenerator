using System.Collections.Generic;
using UnityEngine;

public class BFS
{

    public class Edge
    {
        public int from, to, cost;

        public Edge(int from, int to, int cost)
        {
            this.from = from;
            this.to = to;
            this.cost = cost;
        }
    }

    private int n;
    private int?[] prev;
    private List<List<Edge>> graph;

    public BFS(List<List<Edge>> graph)
    {
        if (graph == null)
        {
            Debug.LogError("Graph can not be null");
        }
        n = graph.Count;
        this.graph = graph;
    }

    /**
     * Reconstructs the path (of nodes) from 'start' to 'end' inclusive. If the edges are unweighted
     * then this method returns the shortest path from 'start' to 'end'
     *
     * @return An array of nodes indexes of the shortest path from 'start' to 'end'. If 'start' and
     *     'end' are not connected then an empty array is returned.
     */
    public List<int?> ReconstructPath(int start, int end)
    {
        RunBFS(start);
        List<int?> path = new List<int?>();
        for (int? at = end; at != null; at = prev[(int)at])
        {
            path.Add(at);
        }
        path.Reverse();
        if (path[0] == start)
        {
            return path;
        }
        path.Clear();
        return path;
    }

    // Perform a breadth first search on a graph a starting node 'start'.
    private void RunBFS(int start)
    {
        prev = new int?[n];
        bool[] visited = new bool[n];
        LinkedList<int?> queue = new LinkedList<int?>();

        // Start by visiting the 'start' node and add it to the queue.
        queue.AddLast(start);
        visited[start] = true;

        // Continue until the BFS is done.
        while (queue.Count > 0)
        {
            int node = (int)queue.First.Value;
            queue.RemoveFirst();
            List<Edge> edges = graph[node];

            // Loop through all edges attached to this node. Mark nodes as visited once they're
            // in the queue. This will prevent having duplicate nodes in the queue and speedup the BFS.
            foreach (var edge in edges)
            {
                if (!visited[edge.to])
                {
                    visited[edge.to] = true;
                    prev[edge.to] = node;
                    queue.AddLast(edge.to);
                }
            }
        }
    }

    // Initialize an empty adjacency list that can hold up to n nodes.
    public static List<List<Edge>> createEmptyGraph(int n)
    {
        List<List<Edge>> graph = new List<List<Edge>>(n);
        for (int i = 0; i < n; i++)
        {
            graph.Add(new List<Edge>());
        }
        return graph;
    }

    // Add a directed edge from node 'u' to node 'v' with cost 'cost'.
    public static void addDirectedEdge(List<List<Edge>> graph, int u, int v, int cost)
    {
        graph[u].Add(new Edge(u, v, cost));
    }

    // Add an undirected edge between nodes 'u' and 'v'.
    public static void addUndirectedEdge(List<List<Edge>> graph, int u, int v, int cost)
    {
        addDirectedEdge(graph, u, v, cost);
        addDirectedEdge(graph, v, u, cost);
    }

    // Add an undirected unweighted edge between nodes 'u' and 'v'. The edge added
    // will have a weight of 1 since its intended to be unweighted.
    public static void addUnweightedUndirectedEdge(List<List<Edge>> graph, int u, int v)
    {
        addUndirectedEdge(graph, u, v, 1);
    }

    /* BFS example. */

    public static List<int?> RunBFS(List<(int start, int end)> edges, int start, int end)
    {
        // BFS example #1 from slides.
        int n = edges.Count;
        List<List<Edge>> graph = createEmptyGraph(n);
        foreach (var edge in edges)
        {
            addUnweightedUndirectedEdge(graph, edge.start, edge.end);
        }

        BFS solver;
        solver = new BFS(graph);
        var paht = solver.ReconstructPath(start, end);
        return solver.ReconstructPath(start, end);
    }
}