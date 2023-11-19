using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    Vector3 targetPos;

    public override void Start()
    {
        base.Start();
        Managers.Sound.Play("School", SoundManager.Sound.Bgm);
    }
    private void Update()
    {
        if (!gameStarted) return;

        currentTime += Time.deltaTime;

        if (item == null)
            targetPos = itemSpawnPos.transform.position;

        if (Input.GetKeyDown(KeyCode.Space)) //Ư��Ű
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //���� 
                //ȿ���� ���
                //
                targetPos = generalPos.transform.position;
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) //�ö�ƽ
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //���� 
                targetPos = plasticPos.transform.position;
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.W)) //ĵ
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //���� 
                targetPos = canPos.transform.position;
            }
            else
            {
                //Ʋ��
            }
        }

        else if (Input.GetKeyDown(KeyCode.E) ) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //���� 
                targetPos = glassPos.transform.position;
            }
            else
            {
                //Ʋ��
            }
        }
        else if (Input.GetKeyDown(KeyCode.R)) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //���� 
                targetPos = paperPos.transform.position;
            }
            else
            {
                //Ʋ��
            }
        }

        if (item)
        {
            float distance = Vector2.Distance(item.transform.position, targetPos);
            if(distance > 0.1f)
            {
                item.transform.position = Vector2.Lerp(item.transform.position, targetPos, itemMoveSpeed * Time.deltaTime);
            }
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

                Managers.Sound.Play("ItemSpawn");
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
