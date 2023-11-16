using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) //Ư��Ű
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //���� 
                //ȿ���� ���
                //
            }
            else
            {
                //Ʋ��

            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.Q)) //�ö�ƽ
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.W)) //ĵ
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.E)) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.R) ) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
            DestroyItem();
        }

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            if (isHitBeat[nowBeatIndex] == true)
            {
                Item randomItem = Managers.Resource.GetRandomItem();
                item = GameObject.Instantiate(randomItem);
                item.transform.parent = itemPos.transform;
                item.transform.localPosition = new Vector2(0, 0);
            }

        }
        //���� ���ڿ� ����������
        if(currentTime > exceedRange && item && isHitBeat[nowBeatIndex-1] == true)
        {
            //Ʋ��(��ħ)

            DestroyItem();
        }
    }
}
