using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase
{
    public static Action<int> OnBlastedTileCount;

    public int blastedTileCount;

    private int _width;
    private int _height;
    private float _cellSize;
    private GridCell[,] _gridArray;
    private GameObject[] _tileObjects;
    private GameObject _parentObject;
    private GameObject _tileTrash;
    private List<Vector2> _checkedPositions;

    public GridBase(int width, int height, float cellSize, GameObject[] tileObjects, GameObject parentObject, GameObject tileTrash) 
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._tileObjects = tileObjects;
        this._parentObject = parentObject;
        this._tileTrash = tileTrash;

        _gridArray = new GridCell[_width, _height];
        _checkedPositions = new List<Vector2>();
    }

    public void SetGrid(int x, int y)
    {
        var randomObj = _tileObjects[UnityEngine.Random.Range(0, 4)];

        GridCell gridCell = new GridCell(x, y, _height, _cellSize, randomObj, _parentObject, _tileTrash);
        _gridArray[x, y] = gridCell;
    }

    public void BlastCells(int x, int y)
    {
        _checkedPositions.Clear();
        blastedTileCount = 0;
        //int x, y;
        //GetGridPos(woldPosition, out x, out y);
        CheckGridObjectNeighbors(x, y);
        RepositionTiles();
        SpawnNewTiles();
        OnBlastedTileCount(blastedTileCount);
    }

    public void GetGridPos(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        y = Mathf.FloorToInt(worldPosition.y / _cellSize);
    }

    private void CheckGridObjectNeighbors(int x, int y)
    {
        int rightPos = x + 1;
        int leftPos = x - 1;
        int upPos = y + 1;
        int downPos = y - 1;

        var cell = _gridArray[x, y];
        var breakFunction = false;

        for (int i = 0; i < _checkedPositions.Count; i++)
        {
            if (_checkedPositions[i].x == x && _checkedPositions[i].y == y)
            {
                breakFunction = true;
                break;
            }
        }
        
        _checkedPositions.Add(new Vector2(x, y));

        if (!breakFunction)
        {
            var rightNeigbour = CheckNeighborColor(rightPos, y, cell);
            var leftNeigbour = CheckNeighborColor(leftPos, y, cell);
            var upNeigbour = CheckNeighborColor(x, upPos, cell);
            var downNeigbour = CheckNeighborColor(x, downPos, cell);

            if (
                rightNeigbour == true ||
                leftNeigbour == true ||
                upNeigbour == true ||
                downNeigbour == true
                )
            {
                cell.RemoveCellObject();
                blastedTileCount++;
            }
        }

    }

    private bool CheckNeighborColor(int x, int y, GridCell cell)
    {
        if (x >= 0 && x < _width && y >= 0 && y < _height)
        {
            var neighbor = _gridArray[x, y];
            if (neighbor.tileColor == cell.tileColor)
            {
                CheckGridObjectNeighbors(x, y);
                return true;
            }
        }
        return false;
    }

    private void RepositionTiles()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_gridArray[i, j].isGridNull)
                {
                    //Debug.Log("Tile is empty: " + i + " " + j);
                    for (int y = j + 1; y < _width; y++)
                    {
                        if (!_gridArray[i, y].isGridNull)
                        {
                            _gridArray[i, j].FillCellObject(_gridArray[i, y]);
                            _gridArray[i, y].isGridNull = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void SpawnNewTiles()
    {
        while (true)
        {
            var breakLoop = true;
            for (int i = 0; i < _width; i++)
            {
                if(_gridArray[i, _height-1].isGridNull)
                {
                    breakLoop = false;
                    //Debug.Log("Loop " + i + " "+ _height);
                    SetGrid(i, _height-1);
                }
            }

            RepositionTiles();

            if (breakLoop)
            {
                break;
            }
        }
    }
}
