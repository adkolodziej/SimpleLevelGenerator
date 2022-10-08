using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphList : MonoBehaviour
{
    private List<Graph> graphs;

    public List<Graph> Graphs { get { return graphs; } set { graphs = value; } }

    public Graph GetRandomGraph()
    {
        return graphs[Random.Range(0, graphs.Count)];
    }

    public Graph GetSmallestGraph()
    {
        if (graphs == null)
        {
            return null;
        }
        var smallestGraph = graphs[0];
        foreach (var graph in graphs)
        {
            if (graph.Nodes.Count() < smallestGraph.Nodes.Count())
            {
                smallestGraph = graph;
            }
        }
        return smallestGraph;
    }

    public Graph GetBiggestGraph()
    {
        if (graphs == null)
        {
            return null;
        }
        var biggestGraph = graphs[0];
        foreach (var graph in graphs)
        {
            if (graph.Nodes.Count() > biggestGraph.Nodes.Count())
            {
                biggestGraph = graph;
            }
        }
        return biggestGraph;
    }

    public Graph GetGraphWithNodesCount(int nodesCount)
    {
        var graph = graphs.Find(x => x.Nodes.Count() == nodesCount);
        if (graph == null)
        {
            graph = graphs.Find(x => x.Nodes.Count() == nodesCount - 1 || x.Nodes.Count() == nodesCount + 1);
        }
        return graph;
    }
}
