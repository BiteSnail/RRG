using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Main : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Main"); //Stage3 씬으로 이동
    }

}
