using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private DataController dataController;
    [SerializeField]
    private GridGenerator gridGenerator;
    [SerializeField]
    private GameObject smallRoom;
    [SerializeField]
    private GameObject mediumRoom;
    [SerializeField]
    private GameObject bigRoom;

    private void Start()
    {
        dataController.CreateGraphs();
        var graph = dataController.GetSmallestGraph();

    }
}
