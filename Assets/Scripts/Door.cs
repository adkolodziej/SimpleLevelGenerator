using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject openDoor;
    [SerializeField]
    private GameObject closedDoor;

    public void OpenDoor(bool open)
    {
        openDoor.SetActive(open);
        closedDoor.SetActive(!open);
    }

#if UNITY_EDITOR
    [ContextMenu("OpenDoor")]
    public void OpenDoor()
    {
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    [ContextMenu("CloseDoor")]
    public void CloseDoor()
    {
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
    }
#endif
}
