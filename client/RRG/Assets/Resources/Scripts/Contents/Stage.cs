using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int bpm = 60; //���� �ʿ�
    double currentTime = 0;
    public double exceedRange = 0.3f;

    public int[] hitBeatNums; //������������ �ۼ� �ʿ�
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

        if (Input.GetKeyDown(KeyCode.Space)) //��ư�� ������ ��-> 4���� Ű�� Ȯ���ؾ� ��
        {
            if (Mathf.Abs((float)currentTime - 60 / bpm) < exceedRange && isHitBeat[nowBeatIndex - 1] == true)
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
        }

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            if (isHitBeat[nowBeatIndex] == true)
            {
                item = Managers.Resource.GetRandomItem();
                Item itemInstance =GameObject.Instantiate(item);
            }
        }
        else if (nowBeatIndex > 0 && isHitBeat[nowBeatIndex - 1] == true && item) //�̹��� �� ���� ���ڿ���
        {
            Color color = item.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            item.GetComponent<SpriteRenderer>().color = color;
        }
        nowBeatIndex++;
    }
}
