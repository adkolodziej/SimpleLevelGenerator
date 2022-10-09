using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Door> doors;
    [SerializeField]
    private RoomType roomType;

    private Dictionary<Room, Path> connectedRooms = new();
}
