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

        if (Input.GetKeyDown(KeyCode.Space)&& gameStarted && !pressed) //Ư��Ű
        {
            if (IsCorrectHit() && item.type == ItemType.General)
            {
                //���� 
                targetPos = generalPos.transform.position;
                Managers.Sound.Play("General");
            }
            else
            {
                //Ʋ��
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && gameStarted && !pressed) //�ö�ƽ
        {
            if (IsCorrectHit() && item.type == ItemType.Plastic)
            {
                //���� 
                targetPos = plasticPos.transform.position;
                Managers.Sound.Play("Plastic");
            }
            else
            {
                //Ʋ��
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && gameStarted && !pressed) //ĵ
        {
            if (IsCorrectHit() && item.type == ItemType.Can)
            {
                //���� 
                targetPos = canPos.transform.position;
                Managers.Sound.Play("Can");
            }
            else
            {
                //Ʋ��
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }

        else if (Input.GetKeyDown(KeyCode.E) && gameStarted && !pressed) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Glass)
            {
                //���� 
                targetPos = glassPos.transform.position;
                Managers.Sound.Play("Glass");
            }
            else
            {
                //Ʋ��
                Managers.Sound.Play("Fail");
            }
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && gameStarted && !pressed) //����
        {
            if (IsCorrectHit() && item.type == ItemType.Paper)
            {
                //���� 
                targetPos = paperPos.transform.position;
                Managers.Sound.Play("Paper");
            }
            else
            {
                //Ʋ��
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

        if (currentTime >= 60d / bpm) //�� ���ڸ���
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                Item randomItem = Managers.Resource.GetRandomItem();
                item = GameObject.Instantiate(randomItem);

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

    
}
