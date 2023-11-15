using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    protected int bpm = 60; //설정 필요
    protected double currentTime = 0;
    protected double exceedRange = 0.3f;

    protected int[] hitBeatNums; //스테이지별로 작성 필요
    protected bool[] isHitBeat = new bool[200];
    protected int nowBeatIndex = 0;

    protected Item item = null;


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
}
