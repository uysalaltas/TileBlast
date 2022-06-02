using UnityEngine;

public class GridCell
{
    public Tile.TileObjectColor tileColor;
    public Tile tileObjectScript;
    public GameObject tileObject;
    public bool isGridNull = false;

    private float gridCellPosX;
    private float gridCellPosY;
    private GameObject _tileTrash;

    public GridCell(int x, int y, int boardHeight, float cellSize, GameObject tile, GameObject parentObject, GameObject tileTrash)
    {
        gridCellPosX = (x * cellSize) + (cellSize / 2);
        gridCellPosY = (y * cellSize) + (cellSize / 2);
        _tileTrash = tileTrash;

        tile.transform.localScale = new Vector3(cellSize, cellSize, 1);

        tileObject = GameObject.Instantiate(tile, new Vector3(gridCellPosX, gridCellPosY, 0), Quaternion.identity, parentObject.transform);
        tileObjectScript = tileObject.GetComponent<Tile>();
        tileColor = tileObjectScript.color;
        tileObject.SetActive(true);
        tileObjectScript.StartCoroutine(tileObjectScript.SpawnAnimation((boardHeight * cellSize), 0.4f));
    }

    public void RemoveCellObject()
    {
        tileObject.SetActive(false);
        tileObject.transform.parent = _tileTrash.transform;
        isGridNull = true;
    }

    public void FillCellObject(GridCell gridCell)
    {
        tileObject = gridCell.tileObject;
        tileColor = gridCell.tileColor;
        tileObjectScript = gridCell.tileObjectScript;

        var targetPosition = new Vector3(gridCellPosX, gridCellPosY, 0);
        tileObjectScript.StartCoroutine(tileObjectScript.MoveTileToPoint(targetPosition, 0.1f));
        isGridNull = false;
    }
}
