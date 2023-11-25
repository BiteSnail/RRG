using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEditor.SearchService;
using UnityEngine;

public class Stage2 : StageBase
{
    [SerializeField] int itemSpawnXStart;
    [SerializeField] int itemSpawnXEnd;
    [SerializeField] float itemSpawnY;
    [SerializeField] float hitY;

    Vector3 targetPos;
    int pressed = 0;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (item == null)
            targetPos = Vector3.zero;


        if (Input.GetKeyDown(KeyCode.Space) && pressed < 2 ) //특수키
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.General)
                {
                    //맞음 
                    targetPos = generalPos.transform.position;
                    Managers.Sound.Play("General");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.General
                        || item.pair_second.type == ItemType.General)
                    {
                        if(pressed == 1) //맞음(최종) 
                        {
                            //쓰레기 종류에 따라 분리된 쓰레기를 쓰레기통에 보냄
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //쓰레기를 놓침
            {

            }
            pressed++;
        }
   


        if (currentTime >= 60d / bpm) //매 박자마다
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                ItemSpawn();
                pressed = 0;
                Managers.Sound.Play("ItemSpawn");
            }
            //if (targetPos == itemGotoPos.transform.position)
            {
                DropItem();
            }
        }
    }

    void ItemSpawn()
    {
        if (item) DestroyItem();
        Item randomItem = Managers.Resource.GetRandomItem();
        item = GameObject.Instantiate(randomItem);
        randomItem.isEncounter = true;

        int randX = Random.Range(itemSpawnXStart, itemSpawnXEnd);
        item.transform.position = new Vector2(randX, itemSpawnY);
        
    }
    void DropItem()
    {
        item.transform.position = new Vector2(item.transform.position.x, transform.position.y + 1);
    }

    void MoveItemtoBox(Item tem)
    {
        if(tem.type == ItemType.General)
        {
            targetPos = generalPos.transform.position;
        }
        else if(tem.type == ItemType.Paper)
        {
            targetPos = paperPos.transform.position;
        }
        else if(item.type == ItemType.Can)
        {
            targetPos = canPos.transform.position;
        }
        else if(item.type == ItemType.Glass)
        {
            targetPos = glassPos.transform.position;    
        }
        else if (item.type == ItemType.Plastic)
        {
            targetPos = plasticPos.transform.position;  
        }

    }

    bool IsCorrectHit()
    {
        return true;
    }
}
