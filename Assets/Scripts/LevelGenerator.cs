using System.Collections.Generic;
using UnityEngine;

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
        CreateEdgesForRooms(uniqueNodesInGrap, gridGenerator.Cells);
        //BFS.RunBFS();
        int x = 0;
    }

    private void SetRoomsForGraph(List<Node> nodes)
    {
        foreach (var node in nodes)
        {
            if (node.neighbourIds.Count <= 4)
            {
                node.room = new Room(smallRoom);
            }
            else if (node.neighbourIds.Count <= 8)
            {
                node.room = new Room(mediumRoom);
            }
            else if (node.neighbourIds.Count <= 12)
            {
                node.room = new Room(bigRoom);
            }
        }
    }

    private void CreateRoomsOnGrid(List<Node> nodes, List<Cell> cells)
    {
        foreach (var node in nodes)
        {
            node.room.CreateRoomOnGrid(cells, gridGenerator.SideSize);
        }
    }

    private void CreateEdgesForRooms(List<Node> nodes, List<Cell> cells)
    {
        var paths = new List<Path>();
        var path = new Path();
        foreach (var node in nodes)
        {
            foreach (var neighbour in node.neighbourIds)
            {
                var neighbourCell = nodes.Find(x => x.nodeId == neighbour);
                path.FindPath(node, neighbourCell, cells, gridGenerator.SideSize);
                paths.Add(path);
                path = new Path();
            }
        }
        int x = 0;
    }
}
