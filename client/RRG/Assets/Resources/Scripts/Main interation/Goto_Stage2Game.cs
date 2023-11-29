using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goto_Stage2Game : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("Stage2"); //Stage2 전환
    }
}
