using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Stage3 : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Stage3"); //Stage3 씬으로 이동
    }

}
