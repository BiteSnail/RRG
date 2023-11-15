using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_pictorial_book : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("PictorialBook"); //PictorialBook 씬으로 이동
    }

}