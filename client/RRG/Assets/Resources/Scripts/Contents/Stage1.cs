using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{

    private void Update()
    {
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
