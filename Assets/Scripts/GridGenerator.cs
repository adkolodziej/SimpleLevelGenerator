using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private int sideSize = 200;
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private List<Cell> cells = new List<Cell>();

    public int SideSize => sideSize;
    public List<Cell> Cells => cells;

    [ContextMenu("GenerateGrid")]
    private void GenerateGrid()
    {
        for (int i = 0; i < sideSize; i++)
        {
            for (int j = 0; j < sideSize; j++)
            {
                var go = Instantiate(cellPrefab, parent: transform);
                var cell = go.GetComponent<Cell>();
                cell.SetCell(i, j);
                cells.Add(cell);
                cell.Id = cells.Count - 1;
            }
        }
    }

    [ContextMenu("DestroyGrid")]
    private void DestroyGrid()
    {
        foreach (var cell in cells)
        {
            DestroyImmediate(cell.gameObject);
        }
        cells.Clear();
    }

    [ContextMenu("HideGrid")]
    private void HideGrid()
    {
        foreach (var cell in cells)
        {
            cell.HideCell();
        }
    }

    [ContextMenu("ShowGrid")]
    private void ShowGrid()
    {
        foreach (var cell in cells)
        {
            cell.ShowCell();
        }
    }

    [ContextMenu("ClearList")]
    private void ClearList()
    {
        cells.Clear();
    }
}
