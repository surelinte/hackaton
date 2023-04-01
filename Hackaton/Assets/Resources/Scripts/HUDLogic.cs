using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLogic : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameWindow;
    public GameObject GameHUD;
    public GameObject WinWindow;
    public GameObject FailWindow;
    public GameObject LeaderBoard;
    public GameObject EnterNameWindow;
    public GameObject LeaderList;


    void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        MainMenu.SetActive(true);
        GameWindow.SetActive(false);
        LeaderBoard.SetActive(false);
    }

    public void StartGame()
    {
        MainMenu.SetActive(false);
        GameWindow.SetActive(true);
        GameHUD.SetActive(true);
    }

    public void ShowLeaderBoards()
    {
      //  MainMenu.SetActive(false);
        GameHUD.SetActive(false);
        LeaderBoard.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        LeaderBoard.SetActive(false);
    }

    public void EnterName()
    {
        EnterNameWindow.SetActive(false);
        LeaderList.SetActive(true);
    }

    public void YouWin()
    {
        GameHUD.SetActive(false);
        WinWindow.SetActive(true);
    }

    public void YouFail()
    {
        GameHUD.SetActive(false);
        FailWindow.SetActive(true);
    }

    public void TakeMoney()
    {
        LeaderBoard.SetActive(true);
        LeaderList.SetActive(false);
        EnterNameWindow.SetActive(true);
    }

    public void GameContinue()
    {
        WinWindow.SetActive(false);
        GameWindow.SetActive(true);
        GameHUD.SetActive(true);
    }

    public void GameAgain()
    {
        FailWindow.SetActive(false);
        GameWindow.SetActive(true);
        GameHUD.SetActive(true);
    }
}
