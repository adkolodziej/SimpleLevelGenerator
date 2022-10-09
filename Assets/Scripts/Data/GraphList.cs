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

    public (Graph, int) GetSmallestGraph()
    {
        if (graphs.Count < 1)
        {
            return (null, 0);
        }
        var smallestGraph = graphs[0];
        foreach (var graph in graphs)
        {
            if (GetGraphNodeCount(graph) < GetGraphNodeCount(smallestGraph))
            {
                smallestGraph = graph;
            }
        }
        return (smallestGraph, GetGraphNodeCount(smallestGraph));
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
            if (GetGraphNodeCount(graph) > GetGraphNodeCount(biggestGraph))
            {
                biggestGraph = graph;
            }
        }
        return biggestGraph;
    }

    public Graph GetGraphWithNodesCount(int nodesCount)
    {
        var graph = graphs.Find(x => GetGraphNodeCount(x) == nodesCount);
        if (graph == null)
        {
            graph = graphs.Find(x => GetGraphNodeCount(x) == nodesCount - 1 || GetGraphNodeCount(x) == nodesCount + 1);
        }
        return graph;
    }

    private int GetGraphNodeCount(Graph graph)
    {
        int nodeCount = graph.Nodes[0].nodeId;
        foreach (var node in graph.Nodes)
        {
            if (node.nodeId > nodeCount)
            {
                nodeCount = node.nodeId;
            }
            foreach (var nId in node.neighbourIds)
            {
                if (nId > nodeCount)
                {
                    nodeCount = nId;
                }
            }
        }
        return nodeCount;
    }
}
