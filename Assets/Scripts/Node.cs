using System;
using System.Collections.Generic;

[Serializable]
public class Node
{
    public int nodeId;
    public List<int> neighbourIds = new();
    public Room room;
}
