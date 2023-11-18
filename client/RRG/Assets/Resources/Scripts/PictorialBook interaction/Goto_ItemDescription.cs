using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 필요

public class Goto_ItemDescription : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Item_PictorialBook"); //Item_PictorialBook 씬으로 이동
    }

}
