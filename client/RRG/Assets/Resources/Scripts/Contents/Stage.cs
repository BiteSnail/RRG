using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int bpm = 60; //설정 필요
    double currentTime = 0;
    public double exceedRange = 0.3f;

    public int[] hitBeatNums; //스테이지별로 작성 필요
    public bool[] isHitBeat = new bool[200];
    public int nowBeatIndex = 0;

    Item item = null;


    void Start()
    {
        foreach (int num in hitBeatNums)
            isHitBeat[num] = true;
    }


    void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) //버튼이 눌렸을 때-> 4개의 키로 확장해야 함
        {
            if (Mathf.Abs((float)currentTime - 60 / bpm) < exceedRange && isHitBeat[nowBeatIndex - 1] == true)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
        }

        if (currentTime >= 60d / bpm) //매 박자마다
        {
            if (isHitBeat[nowBeatIndex] == true)
            {
                item = Managers.Resource.GetRandomItem();
                Item itemInstance =GameObject.Instantiate(item);
            }
        }
        else if (nowBeatIndex > 0 && isHitBeat[nowBeatIndex - 1] == true && item) //이미지 뜬 다음 박자에서
        {
            Color color = item.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            item.GetComponent<SpriteRenderer>().color = color;
        }
        nowBeatIndex++;
    }
}
