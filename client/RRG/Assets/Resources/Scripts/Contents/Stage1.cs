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

        if (Input.GetKeyDown(KeyCode.Space)) //특수키
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //맞음 
                //효과음 재생
                //
            }
            else
            {
                //틀림

            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.Q)) //플라스틱
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.W)) //캔
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.E)) //종이
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
            DestroyItem();
        }
        else if (Input.GetKeyDown(KeyCode.R) ) //유리
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
            DestroyItem();
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
