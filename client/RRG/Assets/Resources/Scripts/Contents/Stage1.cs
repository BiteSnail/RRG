using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : StageBase
{
    public GameObject itemPos_L;
    public GameObject itemPos_R;
    public TextMeshProUGUI respondText;
    Vector3 targetPos;

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

        if (Input.GetKeyDown(KeyCode.Space)&& gameStarted && !pressed) //Ư��Ű
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
        else if (Input.GetKeyDown(KeyCode.Q) && gameStarted && !pressed) //�ö�ƽ
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
        else if (Input.GetKeyDown(KeyCode.W) && gameStarted && !pressed) //ĵ
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

        else if (Input.GetKeyDown(KeyCode.E) && gameStarted && !pressed) //����
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
        else if (Input.GetKeyDown(KeyCode.R) && gameStarted && !pressed) //����
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
            if (distance > 0.1f)
            {
                Vector2 direction = (targetPos - item.transform.position).normalized;
                item.transform.Translate(direction * itemMoveSpeed * Time.deltaTime);

                if (targetPos != itemPos_L.transform.position && targetPos != itemPos_R.transform.position)
                    item.transform.localScale = Vector2.Lerp(item.transform.localScale, Vector2.zero, Time.deltaTime);
            }
            else if (targetPos != hitBox.transform.position)
            {
                DestroyItem();
            }
                
        }

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                Item randomItem = Managers.Resource.GetRandomItem();
                item = GameObject.Instantiate(randomItem);
                randomItem.isEncounter = true;

                //������ ���� ��ġ (�� / ��) 
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    item.transform.parent = itemPos_L.transform;
                    targetPos =itemPos_R.transform.position;
                }
                else
                {
                    item.transform.parent = itemPos_R.transform;
                    targetPos = itemPos_L.transform.position;
                }

                pressed = false;
                item.transform.localPosition = new Vector2(0, 0);
                Managers.Sound.Play("ItemSpawn");
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
}
