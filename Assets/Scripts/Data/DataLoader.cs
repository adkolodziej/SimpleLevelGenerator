using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader
{
    public void LoadData(string dataPath, GraphList graphList)
    {
        var files = Directory.GetFiles(dataPath);
        List<Graph> newGraphs = new List<Graph>();
        Graph newGraph = new Graph();
        foreach (var file in files)
        {
            newGraph.CreateGraph(file);
            newGraphs.Add(newGraph);
        }
        graphList.Graphs = newGraphs;
    }
}
