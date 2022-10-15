using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private RoomType type;

    private List<Cell> doorCells;
    private Dictionary<Room, Path> connectedRooms = new();
    private int roomSideSize;
    private int retryCounter;

    public void CreateRoomOnGrid(List<Cell> cells, int gridSideSize)
    {
        int randomX = Random.Range(roomSideSize, gridSideSize - roomSideSize);
        int randomY = Random.Range(roomSideSize, gridSideSize - roomSideSize);
        if (IsCellValid(cells, randomX, randomY))
        {
            CreateRoom(cells, randomX, randomY);
        }
        else
        {
            retryCounter++;
            CreateRoomOnGrid(cells, gridSideSize);
            if (retryCounter > 10)
            {
                return;
            }
        }
    }

    private bool IsCellValid(List<Cell> cells, int x, int y)
    {
        bool valid = false;
        for (int i = x - 1; i < x + roomSideSize + 1; i++)
        {
            for (int j = y - 1; j < y + roomSideSize + 1; j++)
            {
                var cell = cells.Find(x => x.Row == i && x.Column == j);
                if (cell.CellType == CellType.Empty)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                    break;
                }
            }
        }
        return valid;
    }

    private void CreateRoom(List<Cell> cells, int x, int y)
    {
        for (int i = x; i < x + roomSideSize; i++)
        {
            for (int j = y; j < y + roomSideSize; j++)
            {
                var cell = cells.Find(x => x.Row == i && x.Column == j);
                cell.CellType = CellType.Room;
                cell.TurnRoom();
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (type)
        {
            case RoomType.BigRoom: roomSideSize = 3; break;
            case RoomType.MidRoom: roomSideSize = 2; break;
            case RoomType.SmallRoom: roomSideSize = 1; break;
            default: break;
        }
    }
#endif
}
