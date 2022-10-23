using System.Collections.Generic;
using System.IO;

public class DataLoader
{
    public void LoadData(string dataPath, GraphList graphList)
    {
        var files = Directory.GetFiles(dataPath, "*.txt");

        List<Graph> newGraphs = new List<Graph>();
        Graph graph = new Graph();
        foreach (var file in files)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            graph = new Graph();
            graph.CreateGraph(lines);
            newGraphs.Add(graph);
        }
        graphList.Graphs = newGraphs;
    }
}
