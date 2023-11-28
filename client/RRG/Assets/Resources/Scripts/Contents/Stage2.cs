using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : StageBase
{
    [SerializeField] int itemSpawnXStart;
    [SerializeField] int itemSpawnXEnd;
    [SerializeField] float itemSpawnY;
    [SerializeField] float hitY;

    int pressed = 0;

    List<Item> itemList = new List<Item>();
    Dictionary<Item, Vector3> itemTargetPos = new Dictionary<Item, Vector3>();
    public override void Start()
    {
        base.Start();
        Managers.Sound.Play("Apart", SoundManager.Sound.Bgm);
        Managers.Save.startRecording(this);
        StartCoroutine(EndStage());
    }

    private void Update()
    {
        currentTime += Time.deltaTime;


        Item currentItem = null;
        if (itemList.Count > 0)
            currentItem = itemList[0];

      
        if (Input.GetKey(KeyCode.Space) && pressed < 2 ) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.General)
                {
                    //맞음 
                    itemTargetPos[currentItem] = generalPos.transform.position;
                    Managers.Sound.Play("General");

                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.General
                        || currentItem.pair_second.type == ItemType.General)
                    {
                        if(pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
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
        if (Input.GetKey(KeyCode.Q) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Plastic)
                {
                    //맞음 
                    itemTargetPos[currentItem] = plasticPos.transform.position;
                    Managers.Sound.Play("Plastic");

                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Plastic
                        || currentItem.pair_second.type == ItemType.Plastic)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
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
        if (Input.GetKey(KeyCode.W) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Can)
                {
                    //맞음 
                    itemTargetPos[currentItem] = canPos.transform.position;
                    Managers.Sound.Play("Can");

                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Can
                        || currentItem.pair_second.type == ItemType.Can)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
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
        if (Input.GetKey(KeyCode.E) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Glass)
                {
                    //맞음 
                    itemTargetPos[currentItem] = glassPos.transform.position;
                    Managers.Sound.Play("Glass");

                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Glass
                        || currentItem.pair_second.type == ItemType.Glass)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
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

        if (Input.GetKey(KeyCode.R) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Paper)
                {
                    //맞음 
                    itemTargetPos[currentItem] = paperPos.transform.position;
                    Managers.Sound.Play("Paper");

                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Paper
                        || currentItem.pair_second.type == ItemType.Paper)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
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
            else
            {
                for(int i = 0;i < itemList.Count; i++)
                {
                    if (itemTargetPos[itemList[i]] == Vector3.zero)
                        DropItem(itemList[i]);
                }
            }
        }
        
        for (int i = 0 ;i < itemList.Count;i++)
        {
            Item item = itemList[i];
            float distance = Vector2.Distance(item.transform.position, itemTargetPos[item]);
            if (distance > 0.1f && itemTargetPos[item] != Vector3.zero)
            {
                Vector2 direction = (itemTargetPos[item] - item.transform.position).normalized;
                item.transform.Translate(direction * itemMoveSpeed * Time.deltaTime);

                item.transform.localScale = Vector2.Lerp(item.transform.localScale, Vector2.zero, Time.deltaTime);
            }
            else if (distance < 0.1f && itemTargetPos[item] != Vector3.zero)
            {
                itemList.Remove(item);
                itemTargetPos.Remove(item);
                Destroy(item);
                i--;
            }
        }
       
    }

    void ItemSpawn()
    {  
        Item randomItem = Managers.Resource.GetRandomItem();
        item = GameObject.Instantiate(randomItem);
        randomItem.isEncounter = true;

        int randX = Random.Range(itemSpawnXStart, itemSpawnXEnd);
        item.transform.position = new Vector2(randX, itemSpawnY);

        itemList.Add(item);
        itemTargetPos.Add(item, Vector2.zero);
        
    }
    void DropItem(Item item)
    {
        item.transform.position = new Vector2(item.transform.position.x, item.transform.position.y -1);
    }

    void SeperateItem(Item item)
    {
        Item first = GameObject.Instantiate(item.pair_first);
        Item second = GameObject.Instantiate(item.pair_second);
        first.transform.position = item.transform.position;
        second.transform.position = item.transform.position;
        itemList.Add(first);
        itemList.Add(second);
        itemTargetPos.Add(first, Vector3.zero);
        itemTargetPos.Add(second, Vector3.zero);

        itemList.Remove(item);
        itemTargetPos.Remove(item);
        Destroy(item);
        //쓰레기 종류에 따라 분리된 쓰레기를 쓰레기통에 보냄
        MoveItemtoBox(first);
        MoveItemtoBox(second);
    }

    void MoveItemtoBox(Item tem)
    {
        if(tem.type == ItemType.General)
        {
            itemTargetPos[item] = generalPos.transform.position;
        }
        else if(tem.type == ItemType.Paper)
        {
            itemTargetPos[item] = paperPos.transform.position;
        }
        else if(item.type == ItemType.Can)
        {
            itemTargetPos[item] = canPos.transform.position;
        }
        else if(item.type == ItemType.Glass)
        {
            itemTargetPos[item] = glassPos.transform.position;    
        }
        else if (item.type == ItemType.Plastic)
        {
            itemTargetPos[item] = plasticPos.transform.position;  
        }

    }

    bool IsCorrectHit(Item item)
    {
        if (item == null) return false;
        return item.transform.position.y == hitY;
    }
    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(Managers.Resource.GetAudio("Apart").length);
        SceneManager.LoadScene("Result");
    }
}
