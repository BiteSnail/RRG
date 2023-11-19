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

        if (Input.GetKeyDown(KeyCode.Space)) //특수키
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //맞음 
                //효과음 재생
                //
                targetPos = generalPos.transform.position;
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) //플라스틱
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //맞음 
                targetPos = plasticPos.transform.position;
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.W)) //캔
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //맞음 
                targetPos = canPos.transform.position;
            }
            else
            {
                //틀림
            }
        }

        else if (Input.GetKeyDown(KeyCode.E) ) //유리
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //맞음 
                targetPos = glassPos.transform.position;
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.R)) //종이
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //맞음 
                targetPos = paperPos.transform.position;
            }
            else
            {
                //틀림
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

        if (currentTime >= 60d / bpm) //매 박자마다
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
        //다음 박자에 못눌렀으면
        if(currentTime > exceedRange && item && isHitBeat[nowBeatIndex-1] == true)
        {
            //틀림(놓침)

            DestroyItem();
        }
    }
}
