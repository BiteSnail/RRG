using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Stage1 : StageBase
{
    public GameObject itemPos_L;
    public GameObject itemPos_R;

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

        if (Input.GetKeyDown(KeyCode.Space)&& gameStarted && !pressed) //특수키
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //맞음 
                targetPos = generalPos.transform.position;
                Managers.Sound.Play("General");
            }
            else
            {
                //틀림
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && gameStarted && !pressed) //플라스틱
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //맞음 
                targetPos = plasticPos.transform.position;
                Managers.Sound.Play("Plastic");
            }
            else
            {
                //틀림
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && gameStarted && !pressed) //캔
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //맞음 
                targetPos = canPos.transform.position;
                Managers.Sound.Play("Can");
            }
            else
            {
                //틀림
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.E) && gameStarted && !pressed) //유리
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //맞음 
                targetPos = glassPos.transform.position;
                Managers.Sound.Play("Glass");
            }
            else
            {
                //틀림
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && gameStarted && !pressed) //종이
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //맞음 
                targetPos = paperPos.transform.position;
                Managers.Sound.Play("Paper");
            }
            else
            {
                //틀림
                Managers.Sound.Play("Fail");
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
                DestroyItem();
        }

        if (currentTime >= 60d / bpm) //매 박자마다
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                Item randomItem = Managers.Resource.GetRandomItem();
                item = GameObject.Instantiate(randomItem);

                //아이템 스폰 위치 (좌 / 우) 
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

    
}
