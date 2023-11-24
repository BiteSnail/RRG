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

        if (Input.GetKeyDown(KeyCode.Space)&& !pressed) //특수키
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.General)
                {
                    //맞음 
                    targetPos = generalPos.transform.position;
                    Managers.Sound.Play("General");
                    SetCorrectText();
                }
                else //박자는 맞았는데 분류가 틀림
                {
                    Managers.Sound.Play("Fail");
                    SetWrongtText();
                }   
            }
            else //쓰레기를 놓침
            {
                SetMissText();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Q) && !pressed) //플라스틱
        {
            if (IsCorrectHit())            
            {
                if(item.type == ItemType.Plastic)
                {   
                    //맞음 
                    targetPos = plasticPos.transform.position;
                    Managers.Sound.Play("Plastic");
                    SetCorrectText();
                }
                else
                {
                    //틀림
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
        else if (Input.GetKeyDown(KeyCode.W) && !pressed) //캔
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Can)
                {
                    //맞음 
                    targetPos = canPos.transform.position;
                    Managers.Sound.Play("Can");
                    SetCorrectText();
                }
                else
                {
                    //틀림
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

        else if (Input.GetKeyDown(KeyCode.E) && !pressed) //유리
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Glass)
                {
                    //맞음 
                    targetPos = glassPos.transform.position;
                    Managers.Sound.Play("Glass");
                    SetCorrectText();
                }
                else
                {
                    //틀림
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
        else if (Input.GetKeyDown(KeyCode.R) && !pressed) //종이
        {
            if(IsCorrectHit())
            {
                if (item.type == ItemType.Paper)
                {
                    //맞음 
                    targetPos = paperPos.transform.position;
                    Managers.Sound.Play("Paper");
                    SetCorrectText();
                }
                else
                {
                    //틀림
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

        if (currentTime >= 60d / bpm) //매 박자마다
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
        respondText.SetText("좋아!");
    }
    void SetWrongtText()
    {
        respondText.SetText("틀렸어");
    }
    void SetMissText()
    {
        respondText.SetText("놓쳤어");
    }


    protected bool IsCorrectHit()
    {

        return Vector2.Distance(item.transform.position, hitBox.transform.position) < exceedRange;
    }
}
