using System.Collections.Generic;
using UnityEngine;
using static Graph;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private DataController dataController;
    [SerializeField]
    private GridGenerator gridGenerator;
    [SerializeField]
    private Room smallRoom;
    [SerializeField]
    private Room mediumRoom;
    [SerializeField]
    private Room bigRoom;

    private void Start()
    {
        dataController.CreateGraphs();
        var graph = dataController.GetSmallestGraph();
        var uniqueNodesInGrap = graph.Item1.GetAllUniqueNodes();
        SetRoomsForGraph(uniqueNodesInGrap);
        CreateRoomsOnGrid(uniqueNodesInGrap, gridGenerator.Cells);
        int x = 0;
    }

    private void SetRoomsForGraph(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            if (node.neighbourIds.Count <= 4)
            {
                node.room = smallRoom;
            }
            if (node.neighbourIds.Count <= 8)
            {
                node.room = mediumRoom;
            }
            if (node.neighbourIds.Count <= 12)
            {
                node.room = bigRoom;
            }
        }
    }

    private void CreateRoomsOnGrid(List<Node> nodes, List<Cell> cells)
    {
        foreach(var node in nodes)
        {
            node.room.CreateRoomOnGrid(cells, gridGenerator.SideSize);
        }
    }
}
