using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Stage2Opening : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Stage2_Opening"); //Stage1 오프닝으로 이동
    }

}
