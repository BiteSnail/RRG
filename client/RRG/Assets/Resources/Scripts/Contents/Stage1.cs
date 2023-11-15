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
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && item.type == ItemType.Plastic) //�ö�ƽ
        {
            if (IsCorrectHit())
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && item.type == ItemType.Can) //ĵ
        {
            if (IsCorrectHit())
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && item.type == ItemType.Paper) //����
        {
            if (IsCorrectHit())
            {
                //���� 
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && item.type == ItemType.Glass) //����
        {
            if (IsCorrectHit())
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
                Item itemInstance = GameObject.Instantiate(item);
            }
        }
        else if (nowBeatIndex > 0 && isHitBeat[nowBeatIndex - 1] == true && item) //�̹��� �� ���� ���ڿ���
        {
            Destroy(item);
            item = null;
        }
    }


}
