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
    public GameObject FinalWin;

    void Start()
    {
        OpenMenu();
    }

    bool resultSent = false;

    public void OpenMenu()
    {
        MainMenu.SetActive(true);
        WinWindow.SetActive(false);
        FailWindow.SetActive(false);
        GameWindow.SetActive(false);
        LeaderBoard.SetActive(false);
        FinalWin.SetActive(false);
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
        EnterNameWindow.SetActive(false);
        LeaderList.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        LeaderBoard.SetActive(false);
        if (resultSent) {
            FindObjectOfType<Game>().StartAgain();
            OpenMenu();
            resultSent = false;
        }
    }

    public void EnterName()
    {
        resultSent = true;
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
        Sound.Play("money");
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
        GameWindow.SetActive(false);
        GameHUD.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ShowFinalWin() {
        FinalWin.SetActive(true);
    }
}
