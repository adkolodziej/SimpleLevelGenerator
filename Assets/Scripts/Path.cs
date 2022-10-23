using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private int length;
    private List<Cell> pathCells = new List<Cell>();
    private List<int?> pathIds;
    public (int start, int end) connectedCells;

    int[] dx = { -1, 1, 0, 0 };
    int[] dy = { 0, 0, 1, -1 };

    public void FindPath(Node startNode, Node endNode, List<Cell> cells, int sideSize)
    {
        var doors = GetDoorCells(startNode, endNode);
        List<(int start, int end)> ps = FindPossibleEdges(doors.startCell, doors.endCell, cells, sideSize);
        if (ps == null)
        {
            return;
        }
        pathIds = BFS.RunBFS(ps, doors.startCell, doors.endCell);
        for (int i = 0; i < pathIds.Count; i++)
        {
            var cell = cells.Find(x => x.Id == pathIds[i]);
            cell.CellType = CellType.Path;
            cell.TurnPath();
            pathCells.Add(cell);
        }
        if(pathCells.Count > 0)
        {
            connectedCells.start = pathCells[0].Id;
            connectedCells.end = pathCells[pathCells.Count - 1].Id;
        }
        length = pathCells.Count;
        Cell.pathMaterialIterator++;
    }

    private List<(int start, int end)> FindPossibleEdges(int startCell, int endCell, List<Cell> cells, int sideSize)
    {
        List<(int start, int end)> ps = new List<(int start, int end)>();
        if (startCell == -1 || endCell == -1)
        {
            return null;
        }
        var cell = cells.Find(c => c.Id == startCell);
        cell.CellType = CellType.Empty;
        cell = cells.Find(c => c.Id == endCell);
        cell.CellType = CellType.Empty;
        for (int i = 0; i < cells.Count; i++)
        {
            cell = cells.Find(c => c.Id == i);
            for (int j = 0; j < 4; j++)
            {
                var cx = cell.X + dx[j];
                var cy = cell.Y + dy[j];

                if (cx < 0 || cy < 0) continue;
                if (cx >= sideSize || cy >= sideSize) continue;

                var c = cells.Find(c => c.X == cx && c.Y == cy);
                if (c.CellType != CellType.Empty) continue;
                if (cell.CellType != CellType.Empty) continue;
                if (ps.Contains((cell.Id, c.Id))) continue;
                ps.Add((cell.Id, c.Id));
            }
        }
        return ps;
    }

    private (int startCell, int endCell) GetDoorCells(Node startNode, Node endNode)
    {
        int startCell = 0;
        int endCell = 0;

        startCell = startNode.room.GetFirstFreeDoor();
        endCell = endNode.room.GetFirstFreeDoor();

        return (startCell, endCell);
    }
    public bool IsItThisPath(int startCellId, int endCellId)
    {
        if (connectedCells.start == startCellId && connectedCells.end == endCellId)
        {
            return true;
        }
        if(connectedCells.start == endCellId && connectedCells.end == startCellId)
        {
            return true;
        }
        return false;
    }
}
