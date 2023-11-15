using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //특수키
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //맞음 
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && item.type == ItemType.Plastic) //플라스틱
        {
            if (IsCorrectHit())
            {
                //맞음 
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && item.type == ItemType.Can) //캔
        {
            if (IsCorrectHit())
            {
                //맞음 
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && item.type == ItemType.Paper) //종이
        {
            if (IsCorrectHit())
            {
                //맞음 
            }
            else
            {
                //틀림
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && item.type == ItemType.Glass) //유리
        {
            if (IsCorrectHit())
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
                Item itemInstance = GameObject.Instantiate(item);
            }
        }
        else if (nowBeatIndex > 0 && isHitBeat[nowBeatIndex - 1] == true && item) //이미지 뜬 다음 박자에서
        {
            Destroy(item);
            item = null;
        }
    }


}
