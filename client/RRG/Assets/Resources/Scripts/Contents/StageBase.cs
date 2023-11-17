using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    public int bpm = 60; //설정 필요
    protected double currentTime = 0;
    public double exceedRange = 0.5f;

    public int[] hitBeatNums; //스테이지별로 작성 필요
    protected bool[] isHitBeat = new bool[200];
    protected int nowBeatIndex = 0;

    protected Item item = null;
    public GameObject itemPos;

    public Sprite gameBackground;
    public Sprite openingBackground;

    public float itemMoveSpeed = 10f;
    public GameObject itemSpawnPos;
    public GameObject canPos;
    public GameObject plasticPos;
    public GameObject paperPos;
    public GameObject glassPos;
    public GameObject generalPos;

    protected bool gameStarted = false;

    void Start()
    {
        StartCoroutine(Opening());
        foreach (int num in hitBeatNums)
            isHitBeat[num] = true;
    }


    protected bool IsCorrectHit()
    {
        return (currentTime < exceedRange && isHitBeat[nowBeatIndex - 1]) 
            || (60d/bpm - currentTime < exceedRange && isHitBeat[nowBeatIndex]);
    }

    protected void DestroyItem()
    {
        if (item != null)
        {
            Destroy(item.gameObject);
            item = null;
        }
    }

   IEnumerator Opening()
    {
        GameObject  
            .Find("Background")
            .GetComponent<SpriteRenderer>()
            .sprite = openingBackground;

        yield return new WaitForSeconds(3.0f);
        GameObject
            .Find("Background")
            .GetComponent<SpriteRenderer>()
            .sprite = gameBackground;

        gameStarted = true;
    }
}
