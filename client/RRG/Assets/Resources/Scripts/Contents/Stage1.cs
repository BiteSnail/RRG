using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    private void Update()
    {
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
