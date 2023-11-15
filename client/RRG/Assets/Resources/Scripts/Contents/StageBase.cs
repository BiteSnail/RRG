using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    protected int bpm = 60; //���� �ʿ�
    protected double currentTime = 0;
    protected double exceedRange = 0.3f;

    protected int[] hitBeatNums; //������������ �ۼ� �ʿ�
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
