using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goto_Stage1Game : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Stage1"); //Stage1 ?�으�??�동
    }
}
