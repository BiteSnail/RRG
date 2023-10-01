using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameState
    { 
        Main,
        Menu,
        Stage1, Stage2, Stage3,
        Result
    }

    private GameState gameState;

    public void Start()
    {
        ChangeGameState(GameState.Main);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //게임 상태 변경
    public void ChangeGameState(GameState state)
    {
        gameState = state;

        switch(gameState)
        {
            case GameState.Main:
                LoadScene("Main");
                break;
            case GameState.Menu:
                //TODO : Menu 띄우기(Main Scene)
                break;
            case GameState.Stage1:
                LoadScene("Stage1");
                break;
            case GameState.Stage2:
                LoadScene("Stage2");
                break;
            case GameState.Stage3:
                LoadScene("Stage3");
                break;
            case GameState.Result:
                LoadScene("Result");
                break;
        }
    }

}
