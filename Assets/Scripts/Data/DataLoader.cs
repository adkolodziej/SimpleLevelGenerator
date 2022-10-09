using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataLoader
{
    public void LoadData(string dataPath, GraphList graphList)
    {
        var files = Directory.GetFiles(dataPath, "*.txt");

        //DirectoryInfo dir = new DirectoryInfo(dataPath);
        //FileInfo[] info = dir.GetFiles("*.txt");

        List<Graph> newGraphs = new List<Graph>();
        Graph graph = new Graph();
        foreach (var file in files)
        {
            if(file.Contains("268"))
            {
                int x = 0;
            }
            string[] lines = System.IO.File.ReadAllLines(file);
            graph = new Graph();
            graph.CreateGraph(lines);
            newGraphs.Add(graph);
        }
        graphList.Graphs = newGraphs;
    }
}
