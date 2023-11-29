using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
    Queue<Item> itemQ = new Queue<Item>();

    [SerializeField] GameObject[] man;

    enum ManType { Bad, Normal, Good, Great}
    int score = 0;
    bool manUp = true;

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
            
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemTargetPos[itemList[i]] == Vector3.zero)
                        DropItem(itemList[i]);
                }
            }

            for(int i = 0; i < man.Length; i++)
            {
                if (manUp)
                    man[i].transform.position = new Vector2(man[i].transform.position.x, man[i].transform.position.y-0.1f);
                else
                    man[i].transform.position = new Vector2(man[i].transform.position.x, man[i].transform.position.y+0.1f);
            }
            manUp = !manUp;
        }

        for (int i = 0; i < itemList.Count; i++)
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
                Destroy(item.gameObject);
                i--;
            }
        }


        Item currentItem = null;
        if (itemQ.Count > 0)
        {
            currentItem = itemQ.Peek();
        }

        if (currentItem.transform.position.y < hitY - 1)
        {
            itemQ.Dequeue();
            currentItem = itemQ.Peek();
            pressed = 0;
        }
            
        if (currentItem.transform.position.y > hitY + 2)
            return;


        if (Input.GetKeyDown(KeyCode.Space) && pressed < 2 ) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.General)
                {
                    //맞음 
                    itemTargetPos[currentItem] = generalPos.transform.position;
                    Managers.Sound.Play("General");
                    score++;
                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.General
                        || currentItem.pair_second.type == ItemType.General)
                    {
                        if(pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
                            score++;
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                        score--;
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    score--;
                }
            }
            else //쓰레기를 놓침
            {
                Managers.Sound.Play("Fail");
                score--;
            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.Q) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Plastic)
                {
                    //맞음 
                    itemTargetPos[currentItem] = plasticPos.transform.position;
                    Managers.Sound.Play("Plastic");
                    score++;
                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Plastic
                        || currentItem.pair_second.type == ItemType.Plastic)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
                            score++;
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                        score--;
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    score--;
                }
            }
            else //쓰레기를 놓침
            {
                Managers.Sound.Play("Fail");
                score--;
            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.W) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Can)
                {
                    //맞음 
                    itemTargetPos[currentItem] = canPos.transform.position;
                    Managers.Sound.Play("Can");
                    score++;
                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Can
                        || currentItem.pair_second.type == ItemType.Can)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
                            score++;
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                        score--;
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    score--;
                }
            }
            else //쓰레기를 놓침
            {
                Managers.Sound.Play("Fail");
                score--;
            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.E) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Glass)
                {
                    //맞음 
                    itemTargetPos[currentItem] = glassPos.transform.position;
                    Managers.Sound.Play("Glass");
                    score++;
                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Glass
                        || currentItem.pair_second.type == ItemType.Glass)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
                            score++;
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                        score--;
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    score--;
                }
            }
            else //쓰레기를 놓침
            {
                Managers.Sound.Play("Fail");
                score--;
            }
            pressed++;
        }

        if (Input.GetKeyDown(KeyCode.R) && pressed < 2) //특수키
        {
            if (IsCorrectHit(currentItem))
            {
                if (currentItem.type == ItemType.Paper)
                {
                    //맞음 
                    itemTargetPos[currentItem] = paperPos.transform.position;
                    Managers.Sound.Play("Paper");
                    score++;
                }
                else if (currentItem.type == ItemType.Mixed)
                {
                    if (currentItem.pair_first.type == ItemType.Paper
                        || currentItem.pair_second.type == ItemType.Paper)
                    {
                        if (pressed == 1) //맞음(최종) 
                        {
                            SeperateItem(item);
                            score++;
                        }
                    }
                    else //박자는 맞았는데 분류가 틀림
                    {
                        Managers.Sound.Play("Fail");
                        score--;
                    }
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    score--;
                }
            }
            else //쓰레기를 놓침
            {
                Managers.Sound.Play("Fail");
                score--;
            }
            pressed++;
        }

       if(score < -2)
        {
            man[(int)ManType.Bad].SetActive(true);
            man[(int)ManType.Normal].SetActive(false);
        }
       else if (score < 5)
        {
            man[(int)ManType.Bad].SetActive(false);
            man[(int)ManType.Normal].SetActive(true);
            man[(int)ManType.Good].SetActive(false);
        }
       else if (score < 10)
        {
            man[(int)ManType.Normal].SetActive(false);
            man[(int)ManType.Good].SetActive(true);
            man[(int)ManType.Great].SetActive(false);
        }
        else
        {
            man[(int)ManType.Good].SetActive(false);
            man[(int)ManType.Great].SetActive(true);
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
        itemQ.Enqueue(item); 
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
        MoveItemtoBox(first);
        MoveItemtoBox(second);

        itemList.Remove(item);
        itemTargetPos.Remove(item);
        itemQ.Dequeue();
        Destroy(item.gameObject);
        //쓰레기 종류에 따라 분리된 쓰레기를 쓰레기통에 보냄
       
    }

    void MoveItemtoBox(Item tem)
    {
        if(tem.type == ItemType.General)
        {
            itemTargetPos[tem] = generalPos.transform.position;
        }
        else if(tem.type == ItemType.Paper)
        {
            itemTargetPos[tem] = paperPos.transform.position;
        }
        else if(tem.type == ItemType.Can)
        {
            itemTargetPos[tem] = canPos.transform.position;
        }
        else if(tem.type == ItemType.Glass)
        {
            itemTargetPos[tem] = glassPos.transform.position;    
        }
        else if (tem.type == ItemType.Plastic)
        {
            itemTargetPos[tem] = plasticPos.transform.position;  
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
