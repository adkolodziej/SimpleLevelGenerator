using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Material roomMaterial;
    [SerializeField]
    private MeshRenderer renderer;
    [SerializeField]
    private CellType cellType = CellType.Empty;
    [SerializeField]
    private int row;
    [SerializeField]
    private int column;

    private Material defaultMaterial;

    public CellType CellType
    {
        get
        {
            return cellType;
        }
        set
        {
            cellType = value;
        }
    }
    public int Row => row;
    public int Column => column;

    public void SetCell(int row, int column)
    {
        this.row = row;
        this.column = column;
        transform.position = new Vector3(column, -1, row);
    }

    public void TurnRoom()
    {
        renderer.sharedMaterial = roomMaterial;
    }

    public void TurnEmpty()
    {
        renderer.sharedMaterial = defaultMaterial;
    }

    public void HideCell()
    {
        cube.gameObject.SetActive(false);
    }

    public void ShowCell()
    {
        cube.gameObject.SetActive(true);
    }

    private void OnValidate()
    {
        if(defaultMaterial == null)
        {
            defaultMaterial = renderer.sharedMaterial;
        }
    }
}
