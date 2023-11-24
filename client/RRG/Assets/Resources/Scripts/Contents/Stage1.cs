using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : StageBase
{
    public GameObject itemGotoPos;
    public TextMeshProUGUI respondText;
    Vector3 targetPos;
    public GameObject itemSpawnPos;
    public GameObject[] itemPositions;

    public GameObject hitBox;
    int nowPosIdx = 0;

    bool pressed = true;


    public override void Start()
    {
        base.Start();
        Managers.Sound.Play("School_cut", SoundManager.Sound.Bgm);
        StartCoroutine(EndStage());
    }
    private void Update()
    {
    
        currentTime += Time.deltaTime;

        if (item == null)
            targetPos = itemSpawnPos.transform.position;

        if (Input.GetKeyDown(KeyCode.Space)&& !pressed) //Ư��Ű
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.General)
                {
                    //���� 
                    targetPos = generalPos.transform.position;
                    Managers.Sound.Play("General");
                    SetCorrectText();
                }
                else //���ڴ� �¾Ҵµ� �з��� Ʋ��
                {
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }   
            }
            else //�����⸦ ��ħ
            {
                SetMissText();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Q) && !pressed) //�ö�ƽ
        {
            if (IsCorrectHit())            
            {
                if(item.type == ItemType.Plastic)
                {   
                    //���� 
                    targetPos = plasticPos.transform.position;
                    Managers.Sound.Play("Plastic");
                    SetCorrectText();
                }
                else
                {
                    //Ʋ��
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }
            }
            else
            {
                SetMissText();
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && !pressed) //ĵ
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Can)
                {
                    //���� 
                    targetPos = canPos.transform.position;
                    Managers.Sound.Play("Can");
                    SetCorrectText();
                }
                else
                {
                    //Ʋ��
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }
            }
            else
            {
                SetMissText();
            }
            
            pressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.E) && !pressed) //����
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Glass)
                {
                    //���� 
                    targetPos = glassPos.transform.position;
                    Managers.Sound.Play("Glass");
                    SetCorrectText();
                }
                else
                {
                    //Ʋ��
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }
            }
            else
            {
                SetMissText();
            }
            
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !pressed) //����
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Paper)
                {
                    //���� 
                    targetPos = paperPos.transform.position;
                    Managers.Sound.Play("Paper");
                    SetCorrectText();
                }
                else
                {
                    //Ʋ��
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }
            }
            else
            {
                SetMissText();
            }
            pressed = true;
        }

        if (item)
        {
            float distance = Vector2.Distance(item.transform.position, targetPos);
            if (distance > 0.1f && targetPos != itemGotoPos.transform.position)
            {
                Vector2 direction = (targetPos - item.transform.position).normalized;
                item.transform.Translate(direction * itemMoveSpeed * Time.deltaTime);

                item.transform.localScale = Vector2.Lerp(item.transform.localScale, Vector2.zero, Time.deltaTime);
            }
            else if (distance < 0.1f && targetPos != itemGotoPos.transform.position)
                DestroyItem();    
        }

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                if (item) DestroyItem();
                Item randomItem = Managers.Resource.GetRandomItem();
                item = GameObject.Instantiate(randomItem);
                randomItem.isEncounter = true;

                item.transform.parent = itemSpawnPos.transform;
                targetPos =itemGotoPos.transform.position;
             
                pressed = false;
                item.transform.localPosition = new Vector2(0, 0);
                Managers.Sound.Play("ItemSpawn");
                nowPosIdx = 0;
            }
            if (targetPos == itemGotoPos.transform.position)
            {
                item.transform.position = itemPositions[nowPosIdx].transform.position;
                nowPosIdx++;
            }
        }
    }

    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(Managers.Resource.GetAudio("School_cut").length);
        SceneManager.LoadScene("Main");
    }

    void SetCorrectText()
    {
        respondText.SetText("����!");
    }
    void SetWrongtText()
    {
        respondText.SetText("Ʋ�Ⱦ�");
    }
    void SetMissText()
    {
        respondText.SetText("���ƾ�");
    }


    protected bool IsCorrectHit()
    {

        return Vector2.Distance(item.transform.position, hitBox.transform.position) < exceedRange;
    }
}
