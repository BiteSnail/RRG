using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Stage2 : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Stage2"); //Stage2 씬으로 이동
    }

}
