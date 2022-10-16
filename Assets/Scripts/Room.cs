using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Serializable]
    private struct DoorCoordinates
    {
        public int X;
        public int Y;
    }

    [SerializeField]
    private RoomType type;
    [SerializeField]
    private List<DoorCoordinates> doorCoordinates;

    private List<(Cell cell, bool occupied)> doorCells = new();
    private Dictionary<Room, Path> connectedRooms = new();
    private int roomSideSize;
    private int retryCounter;

    public Room(Room room)
    {
        this.type = room.type;
        this.doorCoordinates = room.doorCoordinates;
        this.roomSideSize = room.roomSideSize;
        this.retryCounter = room.retryCounter;
    }

    public void CreateRoomOnGrid(List<Cell> cells, int gridSideSize)
    {
        int randomX = UnityEngine.Random.Range(roomSideSize + 2, gridSideSize - roomSideSize - 2);
        int randomY = UnityEngine.Random.Range(roomSideSize + 2, gridSideSize - roomSideSize - 2);
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

    public int GetFirstFreeDoor()
    {
        for (int i = 0; i < doorCells.Count; i++)
        {
            if (doorCells[i].occupied == false)
            {
                doorCells[i] = new(doorCells[i].cell, true);
                return doorCells[i].cell.Id;
            }
        }
        return -1;
    }

    private bool IsCellValid(List<Cell> cells, int x, int y)
    {
        for (int i = x - 2; i < x + roomSideSize + 2; i++)
        {
            for (int j = y - 2; j < y + roomSideSize + 2; j++)
            {
                var cell = cells.Find(x => x.X == i && x.Y == j);
                if (cell.CellType != CellType.Empty)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void CreateRoom(List<Cell> cells, int x, int y)
    {
        for (int i = x; i < x + roomSideSize; i++)
        {
            for (int j = y; j < y + roomSideSize; j++)
            {
                var cell = cells.Find(x => x.X == i && x.Y == j);
                cell.CellType = CellType.Room;
                cell.TurnRoom();
            }
        }

        CreateDoors(cells, x, y);
    }

    private void CreateDoors(List<Cell> cells, int x, int y)
    {
        foreach (var doorCoord in doorCoordinates)
        {
            var cell = cells.Find(c => c.X == doorCoord.X + x && c.Y == doorCoord.Y + y);
            if (cell.CellType == CellType.Empty)
            {
                cell.CellType = CellType.DoorBlock;
                cell.TurnDoor();
                doorCells.Add((cell, false));
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
