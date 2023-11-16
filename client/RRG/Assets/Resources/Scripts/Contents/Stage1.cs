using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    private void Update()
    {
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
            if (isHitBeat[nowBeatIndex] == true)
            {
                item = Managers.Resource.GetRandomItem();
            }

            currentTime -= 60d / bpm;
        }
        //���� ���ڿ� ����������
        if(currentTime > exceedRange && isHitBeat[nowBeatIndex - 1] && item)
        {
            //Ʋ��(��ħ)

            DestroyItem();
        }
    }
}
