using UnityEngine;

public class GridCell
{
    public Tile.TileObjectColor tileColor;
    public Tile tileObjectScript;
    public GameObject tileObject;
    public bool isGridNull = false;

    private float gridCellPosX;
    private float gridCellPosY;

    public GridCell(int x, int y, float cellSize, GameObject tile, GameObject parentObject)
    {
        gridCellPosX = (x * cellSize) + (cellSize / 2);
        gridCellPosY = (y * cellSize) + (cellSize / 2);

        tile.transform.localScale = new Vector3(cellSize, cellSize, 1);

        tileObject = GameObject.Instantiate(tile, new Vector3(gridCellPosX, gridCellPosY, 0), Quaternion.identity, parentObject.transform);
        tileObject.SetActive(true);

        tileObjectScript = tileObject.GetComponent<Tile>();
        tileColor = tileObjectScript.color;
    }

    public void RemoveCellObject()
    {
        tileObject.SetActive(false);
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
