using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    public int bpm = 60; //설정 필요
    protected double currentTime = 0;
    public double exceedRange = 0.3f;

    public int[] hitBeatNums; //스테이지별로 작성 필요
    protected bool[] isHitBeat = new bool[200];
    protected int nowBeatIndex = 0;

    protected Item item = null;
    public GameObject itemPos;


    void Start()
    {
        foreach (int num in hitBeatNums)
            isHitBeat[num] = true;
    }

    void Update()
    {
        currentTime += Time.deltaTime;

    }

    private void LateUpdate()
    {
        nowBeatIndex++;
    }

    protected bool IsCorrectHit()
    {
        return Mathf.Abs((float)currentTime - 60 / bpm) < exceedRange && isHitBeat[nowBeatIndex - 1];
    }

    protected void DestroyItem()
    {
        if (item != null)
        {
            Destroy(item);
            item = null;
        }
    }
}
