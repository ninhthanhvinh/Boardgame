using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    [SerializeField] GameObject piecesOnBoard;
    static int piecesNumber;
    [SerializeField] TextMeshProUGUI numberPiecesPlayerCanMoveUI;
    [SerializeField] TextMeshProUGUI numberPiecesPlayerCantMoveUI;
    public static int numberPiecesPlayerCanMove;

    private void Awake()
    {
        piecesNumber = piecesOnBoard.transform.childCount;
        numberPiecesPlayerCanMove = piecesNumber / 2;
        numberPiecesPlayerCanMoveUI.SetText(numberPiecesPlayerCanMove.ToString());
        numberPiecesPlayerCantMoveUI.SetText((piecesNumber - numberPiecesPlayerCanMove).ToString());
    }
    public void GetPieceChangeHalf(int value)
    {
        numberPiecesPlayerCanMove += value;
        numberPiecesPlayerCanMoveUI.SetText(numberPiecesPlayerCanMove.ToString());
        numberPiecesPlayerCantMoveUI.SetText((piecesNumber - numberPiecesPlayerCanMove).ToString());
        if (numberPiecesPlayerCanMove == piecesNumber)
            OnLose?.Invoke();
        if (numberPiecesPlayerCanMove == 0)
            OnWin?.Invoke();
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log("Resume");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
