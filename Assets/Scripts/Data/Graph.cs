using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    [Serializable]
    public class Node
    {
        public int nodeId;
        public List<int> neighbourIds = new();
    }

    private List<Node> nodes = new List<Node>();

    public List<Node> Nodes { get { return nodes; } set { nodes = value; } }

    public void CreateGraph(string file)
    {
        string[] lines = System.IO.File.ReadAllLines(file);

        int nodeId;
        int neighbourId;
        string[] text;
        foreach (string line in lines)
        {
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
        int x = 0;
    }

    private void CreateNode(int nodeId, int neighbourId)
    {
        Node node = new Node();
        node.nodeId = nodeId;
        node.neighbourIds.Add(neighbourId);
        nodes.Add(node);
    }
}