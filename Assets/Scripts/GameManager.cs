using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    // --------
    public static Action<int, int> OnUpdateGameMetrics;
    public static Action<bool> OnWinCondition;
    public static Action<bool> OnLoseCondition;

    public bool manualSetup = false;

    public GameObject[] tileObjects;
    public GameObject tileParent;
    public GameObject tileTrash;
    public float cellSize;
    public Vector2Int boardSize;
    public GridBase grid;

    public LevelManager levelManager;
    public int blastAim = 0;
    public int moveCount = 0;
    public int currentLevel = 1;
    public bool canPlay = true;

    private int _maxBlastedTile = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        InitializeGameStats();
        InitializeGrid();
        GridBase.OnBlastedTileCount += CalculateMetrics;
    }

    private void Start()
    {
        RepositionCamera();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPlay)
        {
            StartCoroutine(PreventTouchInput());
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.BlastCells(mousePos);
        }
    }

    private void OnDestroy()
    {
        GridBase.OnBlastedTileCount -= CalculateMetrics;
    }

    private void InitializeGameStats()
    {
        if (!manualSetup)
        {
            currentLevel = PlayerPrefs.GetInt("Level", 1);
            LevelManager levelManager = new LevelManager(currentLevel);
            levelManager.LevelDesign(out moveCount, out blastAim, out boardSize);
        }
    }

    private void InitializeGrid()
    {
        grid = new GridBase(boardSize.x, boardSize.y, cellSize, tileObjects, tileParent, tileTrash);

        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                grid.SetGrid(i, j);
            }
        }
    }

    private void RepositionCamera()
    {
        var xPos = boardSize.x * cellSize / 2;
        var yPos = boardSize.y * cellSize / 2;
        Camera.main.transform.position = new Vector3(xPos, yPos + cellSize, -10);
    }

    private void CalculateMetrics(int tileBlasted)
    {
        if (tileBlasted > 0)
        {
            moveCount--;
        }

        if (tileBlasted > _maxBlastedTile)
        {
            _maxBlastedTile = tileBlasted;
        }

        OnUpdateGameMetrics(moveCount, _maxBlastedTile);

        var winCondition = CheckWinCondition();
        CheckLoseCondition(winCondition);

    }

    private void CheckLoseCondition(bool winCondition)
    {
        if (moveCount == 0 && !winCondition)
        {
            Debug.Log("LOST");
            OnLoseCondition(true);
        }
    }

    private bool CheckWinCondition()
    {
        if(_maxBlastedTile >= blastAim)
        {
            Debug.Log("WIN");
            PlayerPrefs.SetInt("Level", currentLevel + 1);
            OnWinCondition(true);
            return true;
        }
        return false;
    }

    private IEnumerator PreventTouchInput()
    {
        canPlay = false;
        yield return new WaitForSeconds(1.0f);
        canPlay = true;
    }
}
