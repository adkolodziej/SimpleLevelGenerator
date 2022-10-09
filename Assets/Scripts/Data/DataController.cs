using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    [SerializeField]
    private string graphsPath;

    private GraphList graphList = new();
    private DataLoader loader;

    private void Start()
    {
        loader = new DataLoader();
        loader.LoadData(graphsPath, graphList);
        var graph = GetSmallestGraph();
        int x = 0;
    }

    public Graph GetRandomGraph()
    {
        return graphList.GetRandomGraph();
    }

    public (Graph, int) GetSmallestGraph()
    {
        return graphList.GetSmallestGraph();
    }

    public Graph GetBiggestGraph()
    {
        return graphList.GetBiggestGraph();
    }

    public Graph GetGraphWithNodesCount(int nodesCount)
    {
        return graphList.GetGraphWithNodesCount(nodesCount);
    }
}
