using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private List<Node> nodes = new List<Node>();

    public List<Node> Nodes { get { return nodes; } set { nodes = value; } }

    public void CreateGraph(string[] lines)
    {
        int nodeId;
        int neighbourId;
        string[] text;
        string line;
        for (int i = 0; i < lines.Length - 2; i++)
        {
            line = lines[i];
            text = line.Split("\t");
            nodeId = Int32.Parse(text[0]);
            neighbourId = Int32.Parse(text[1]);

            if (nodes.Count < 1)
            {
                CreateNode(nodeId, neighbourId);
            }
            else
            {
                var soughtNode = nodes.Find(x => x.nodeId == nodeId);
                if (soughtNode != null)
                {
                    soughtNode.neighbourIds.Add(neighbourId);
                }
                else
                {
                    CreateNode(nodeId, neighbourId);
                }
            }
        }
    }

    public List<Node> GetAllUniqueNodes()
    {
        var uniqueNodes = new List<Node>();
        uniqueNodes.AddRange(nodes);

        foreach (var node in nodes)
        {
            foreach (var neighbour in node.neighbourIds)
            {
                if (!uniqueNodes.Exists(x => x.nodeId == neighbour))
                {
                    var newN = new Node();
                    newN.nodeId = neighbour;
                    newN.neighbourIds.Add(node.nodeId);
                    uniqueNodes.Add(newN);
                }
            }
        }

        foreach (var uniqueNode in uniqueNodes)
        {
            foreach (var node in nodes)
            {
                foreach (var neighbour in node.neighbourIds)
                {
                    if(neighbour == uniqueNode.nodeId)
                    {
                        uniqueNode.neighbourIds.Add(node.nodeId);
                    }
                }
            }
        }

        return uniqueNodes;
    }

    private void CreateNode(int nodeId, int neighbourId)
    {
        Node node = new Node();
        node.nodeId = nodeId;
        node.neighbourIds.Add(neighbourId);
        nodes.Add(node);
    }
}
