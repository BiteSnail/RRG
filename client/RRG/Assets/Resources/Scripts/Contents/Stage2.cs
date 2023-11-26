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

    Vector3 targetPos;
    int pressed = 0;
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

        if (item == null)
            targetPos = Vector3.zero;


        if (Input.GetKeyDown(KeyCode.Space) && pressed < 2 ) //Ư��Ű
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.General)
                {
                    //���� 
                    targetPos = generalPos.transform.position;
                    Managers.Sound.Play("General");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.General
                        || item.pair_second.type == ItemType.General)
                    {
                        if(pressed == 1) //����(����) 
                        {
                            //������ ������ ���� �и��� �����⸦ �������뿡 ����
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //�����⸦ ��ħ
            {

            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.Q) && pressed < 2) //Ư��Ű
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.Plastic)
                {
                    //���� 
                    targetPos = plasticPos.transform.position;
                    Managers.Sound.Play("Plastic");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.Plastic
                        || item.pair_second.type == ItemType.Plastic)
                    {
                        if (pressed == 1) //����(����) 
                        {
                            //������ ������ ���� �и��� �����⸦ �������뿡 ����
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //�����⸦ ��ħ
            {

            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.W) && pressed < 2) //Ư��Ű
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.Can)
                {
                    //���� 
                    targetPos = canPos.transform.position;
                    Managers.Sound.Play("Can");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.Can
                        || item.pair_second.type == ItemType.Can)
                    {
                        if (pressed == 1) //����(����) 
                        {
                            //������ ������ ���� �и��� �����⸦ �������뿡 ����
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //�����⸦ ��ħ
            {

            }
            pressed++;
        }
        if (Input.GetKeyDown(KeyCode.E) && pressed < 2) //Ư��Ű
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.Glass)
                {
                    //���� 
                    targetPos = glassPos.transform.position;
                    Managers.Sound.Play("Glass");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.Glass
                        || item.pair_second.type == ItemType.Glass)
                    {
                        if (pressed == 1) //����(����) 
                        {
                            //������ ������ ���� �и��� �����⸦ �������뿡 ����
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //�����⸦ ��ħ
            {

            }
            pressed++;
        }

        if (Input.GetKeyDown(KeyCode.R) && pressed < 2) //Ư��Ű
        {
            if (IsCorrectHit())
            {
                if (item.type == ItemType.Paper)
                {
                    //���� 
                    targetPos = paperPos.transform.position;
                    Managers.Sound.Play("Paper");

                }
                else if (item.type == ItemType.Mixed)
                {
                    if (item.pair_first.type == ItemType.Paper
                        || item.pair_second.type == ItemType.Paper)
                    {
                        if (pressed == 1) //����(����) 
                        {
                            //������ ������ ���� �и��� �����⸦ �������뿡 ����
                            MoveItemtoBox(item.pair_first);
                            MoveItemtoBox(item.pair_second);
                        }
                    }
                    else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                    {
                        Managers.Sound.Play("Fail");
                    }
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                }
            }
            else //�����⸦ ��ħ
            {

            }
            pressed++;
        }

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                ItemSpawn();
                pressed = 0;
                Managers.Sound.Play("ItemSpawn");
                targetPos = Vector3.zero;
            }
            else if (targetPos == Vector3.zero)
            {
                DropItem();
            }
        }
        
        if(item)
        {
            float distance = Vector2.Distance(item.transform.position, targetPos);
            if (distance > 0.1f && targetPos != Vector3.zero)
            {
                Vector2 direction = (targetPos - item.transform.position).normalized;
                item.transform.Translate(direction * itemMoveSpeed * Time.deltaTime);

                item.transform.localScale = Vector2.Lerp(item.transform.localScale, Vector2.zero, Time.deltaTime);
            }
            else if (distance < 0.1f && targetPos != Vector3.zero)
                DestroyItem();
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
        item.transform.position = new Vector2(item.transform.position.x, item.transform.position.y -1);
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
        return item.transform.position.y == hitY;
    }
    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(Managers.Resource.GetAudio("Apart").length);
        SceneManager.LoadScene("Result");
    }
}
