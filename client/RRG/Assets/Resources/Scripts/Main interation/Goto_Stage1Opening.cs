using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_Stage1Opening : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Stage1_Opening"); //Stage1 오프닝으로 이동
    }

}
