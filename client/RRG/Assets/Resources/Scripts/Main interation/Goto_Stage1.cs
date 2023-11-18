using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Stage1 : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Stage1"); //Stage1 씬으로 이동
    }

}
