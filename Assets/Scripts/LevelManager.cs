using UnityEngine;

public class LevelManager
{
    private int level;

    public LevelManager(int level)
    {
        this.level = level;
    }

    public void LevelDesign(out int moveCount, out int blastAim, out Vector2Int boardSize)
    {
        switch (level)
        {
            case 1:
                moveCount = 5;
                blastAim = 3;
                boardSize = new Vector2Int(5, 5);
                break;
            case 2:
                moveCount = 5;
                blastAim = 4;
                boardSize = new Vector2Int(5, 5);
                break;
            case 3:
                moveCount = 5;
                blastAim = 5;
                boardSize = new Vector2Int(5, 5);
                break;
            case 4:
                moveCount = 5;
                blastAim = 6;
                boardSize = new Vector2Int(5, 5);
                break;
            case 5:
                moveCount = 5;
                blastAim = 7;
                boardSize = new Vector2Int(5, 5);
                break;
            case 6:
                moveCount = 7;
                blastAim = 10;
                boardSize = new Vector2Int(6, 6);
                break;
            case 7:
                moveCount = 8;
                blastAim = 12;
                boardSize = new Vector2Int(6, 6);
                break;
            case 8:
                moveCount = 8;
                blastAim = 15;
                boardSize = new Vector2Int(7, 7);
                break;
            case 9:
                moveCount = 9;
                blastAim = 17;
                boardSize = new Vector2Int(7, 7);
                break;
            case 10:
                moveCount = 9;
                blastAim = 20;
                boardSize = new Vector2Int(8, 8);
                break;
            default:
                moveCount = Random.Range(10, 30);
                blastAim = Random.Range(10, 30);
                boardSize = new Vector2Int(9, 9);
                break;
        }
    }

}
