using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    private CellType cellType = CellType.Empty;
    private int row;
    private int column;

    public void SetCell(int row, int column)
    {
        this.row = row;
        this.column = column;
        transform.position = new Vector3(row, -1, column);
    }

    public void HideCell()
    {
        cube.gameObject.SetActive(false);
    }

    public void ShowCell()
    {
        cube.gameObject.SetActive(true);
    }
}
