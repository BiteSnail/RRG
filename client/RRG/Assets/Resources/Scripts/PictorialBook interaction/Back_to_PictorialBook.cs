using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Back_to_PictorialBook : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("PictorialBook"); //PictorialBook 씬으로 이동
    }

}
