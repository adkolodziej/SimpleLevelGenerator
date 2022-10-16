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
    private Material doorMaterial;
    [SerializeField]
    private Material pathMaterial;
    [SerializeField]
    private MeshRenderer renderer;
    [SerializeField]
    private CellType cellType = CellType.Empty;
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    [SerializeField]
    private int id;

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
    public int X => x;
    public int Y => y;
    public int Id { get { return id; } set { id = value; } }

    public void SetCell(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, -1, y);
    }

    public void TurnRoom()
    {
        renderer.sharedMaterial = roomMaterial;
    }

    public void TurnEmpty()
    {
        renderer.sharedMaterial = defaultMaterial;
    }

    public void TurnDoor()
    {
        renderer.sharedMaterial = doorMaterial;
    }

    public void TurnPath()
    {
        renderer.sharedMaterial = pathMaterial;
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
        if (defaultMaterial == null)
        {
            defaultMaterial = renderer.sharedMaterial;
        }
    }
}
