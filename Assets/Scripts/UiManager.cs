using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text moveCountLeftText;
    [SerializeField]
    private Text blastCellAimText;
    [SerializeField]
    private Text maxBlastedCellText;

    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private Text levelText;

    public void ReplayGame()
    {

    }

    public void NextLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnEnable()
    {
        GameManager.OnUpdateGameMetrics += UpdateUIElements;
        GameManager.OnWinCondition += ShowWinPanel;
        GameManager.OnLoseCondition += ShowLosePanel;
    }

    private void OnDestroy()
    {
        GameManager.OnUpdateGameMetrics -= UpdateUIElements;
        GameManager.OnWinCondition -= ShowWinPanel;
        GameManager.OnLoseCondition -= ShowLosePanel;
    }

    private void Start()
    {
        blastCellAimText.text = GameManager.Instance.blastAim.ToString();
        moveCountLeftText.text = GameManager.Instance.moveCount.ToString();
        levelText.text = "Level " + GameManager.Instance.currentLevel.ToString();

    }

    private void UpdateUIElements(int moveCountLeft, int maxBlastedCell)
    {
        moveCountLeftText.text = moveCountLeft.ToString();
        maxBlastedCellText.text = maxBlastedCell.ToString();
    }
    
    private void ShowLosePanel(bool lost)
    {
        if (lost)
        {
            GameManager.Instance.canPlay = false;
            losePanel.SetActive(true);
        }
    }
    
    private void ShowWinPanel(bool win)
    {
        if (win)
        {
            GameManager.Instance.canPlay = false;
            winPanel.SetActive(true);
        }
    }
}
