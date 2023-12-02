using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3 : StageBase
{
    // Update is called once per frame
    public GameObject background;
    public Transform preactionPos;
    public Transform seperatePos;
    public Transform hitPos;
    public Transform itemSpawnPos;
    public Transform[] itemPositions;
    public Transform itemGotoPos;
    public GameObject keyAnimaiton;
    bool isCorrect = false;
    private Vector3 targetPos;
    int nowPosIdx = 0;
    private Vector3 TargetPos { get { if (Item == null) return itemSpawnPos.position;  return Item.transform.position; } set { if(Item != null) Item.transform.position = value; } }
    private List<Item> items;
    private Item Item { get { 
            if (items == null) 
                return item;
            return items[0];
        } }

    bool pressed = false;
    private Dictionary<KeyCode, ItemType> codeToItem;
    private Dictionary<ItemType, Transform> itemToPos;
    private Dictionary<ItemType, string> itemToAudio;

    private void Init()
    {
        codeToItem = new Dictionary<KeyCode, ItemType> 
        {
            {KeyCode.Space, ItemType.General },
            {KeyCode.Q, ItemType.Plastic },
            {KeyCode.W, ItemType.Can },
            {KeyCode.E, ItemType.Glass },
            {KeyCode.R, ItemType.Paper }
        };
        itemToPos = new Dictionary<ItemType, Transform>
        {
            {ItemType.General, generalPos.transform },
            {ItemType.Plastic, plasticPos.transform },
            {ItemType.Can, canPos.transform },
            {ItemType.Glass, glassPos.transform },
            {ItemType.Paper, paperPos.transform },
        };
        itemToAudio = new Dictionary<ItemType, string>
        {
            {ItemType.General, "General" },
            {ItemType.Plastic, "Plastic" },
            {ItemType.Can, "Can" },
            {ItemType.Glass, "Glass" },
            {ItemType.Paper, "Paper" }
        };
    }

    public override void Start()
    {
        base.Start();
        Managers.Sound.Play("station_cut", SoundManager.Sound.Bgm);
        Managers.Save.startRecording(this);
        Init();
        StartCoroutine(EndStage());
    }

    protected new void DestroyItem()
    {
        Item temp;
        if(items == null)
        {
            temp = item;
            item = null;
        }
        else
        {
            temp = items[0];
            items.Remove(temp);
            nowPosIdx--;
            if (items.Count == 0)
            {
                items = null;
            }
        }
        Destroy(temp.gameObject);
        pressed = false;
    }

    private bool ValidateKeyCode(KeyCode code)
    {
        bool flag = !pressed && Input.GetKeyDown(code);
        return flag;
    }

    private bool ValidateItemType(ItemType type)
    {
        return Item.type == type;
    }

    private void Preaction()
    {
        if (!ValidateKeyCode(KeyCode.Space))
        {
            return;
        }
        if (item.preActionItem == null)
        {
            return;
        }
        //TODO: animation이 추가되면 좋을 듯
        item = item.preActionItem;
        pressed = false;
    }

    private Item instantiateItem(Item origin, Transform pos)
    {
        Item newItem = Instantiate(origin);
        newItem.transform.position = pos.position;
        newItem.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
        return newItem;
    }

    private void Seperate()
    {
        if (!ValidateItemType(ItemType.Mixed))
            return;
        keyAnimaiton.SetActive(true);
        if (ValidateKeyCode(KeyCode.Space))
        {
            List<Item> temp = new List<Item>();
            if (item.pair_second)
                temp.Add(instantiateItem(item.pair_second, itemPositions[nowPosIdx]));
            if (item.pair_first)
                temp.Add(instantiateItem(item.pair_first, itemPositions[nowPosIdx - 1]));

            DestroyItem();
            items = temp;

            pressed = false;
        }
        
        keyAnimaiton.SetActive(false);
    }

    private void Hit()
    {
        if (pressed)
            return;
        // 정확한 키를 눌렀는지 확인
        foreach (var codeAndType in codeToItem)
        {
            if (ValidateKeyCode(codeAndType.Key) && ValidateItemType(codeAndType.Value))
            {
                TargetPos = itemToPos[codeAndType.Value].position;
                Managers.Sound.Play(itemToAudio[codeAndType.Value]);
                Managers.Save.correct(Item);
                return;
            }
        }
        //실패 시 Fail
        pressed = true;
        StartCoroutine(ChangeColorOverTime());
        Managers.Sound.Play("Fail");
        Managers.Save.wrong(Item);
    }

    private void ButtonProcess()
    {
        if (IsPreactionPos())
        {
            Preaction();
            return;
        }
        if (IsSeperatePos())
        {
            Seperate();
            return;
        }
        if (IsCorrectHit())
        {
            if (Input.anyKeyDown)
                Hit();
        }
    }

    private void CleanProcess()
    {
        if (Item != null)
        {
            foreach (var pos in GameObject.FindGameObjectsWithTag("Finish"))
            {
                if (TargetPos.Equals(pos.transform.position))
                    DestroyItem();
            }
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        ButtonProcess();

        CleanProcess();

        //생성 및 이동 관련
        if (currentTime >= 60d / bpm) //매 박자마다
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                while (Item) DestroyItem();
                Item randomItem = Managers.Resource.GetRandomItemExceptDirty();
                randomItem.isEncounter = true;
                item = instantiateItem(randomItem, itemSpawnPos.transform);

                pressed = false;
                isCorrect = false;
                Item.transform.localPosition = new Vector2(0, 0);
                Managers.Sound.Play("ItemSpawn");
                nowPosIdx = 0;
            }
            if(Item != null)
            {
                if (!isCorrect)
                {
                    MoveItems();
                }
            }
        }
    }

    private void MoveItems()
    {
        if (items != null)
        {
            for (int i = items.Count-1; i > 0; i--)
            {
                items[i].transform.position = items[i - 1].transform.position;
            }
        }
        Item.transform.position = itemPositions[nowPosIdx++].position;
    }

    IEnumerator EndStage()
    {
        yield return new WaitForSeconds(Managers.Resource.GetAudio("station_cut").length);
        SceneManager.LoadScene("Result");
    }

    IEnumerator ChangeColorOverTime()
    {
        float durationTime = 0.5f;
        int iterCount = 2;
        while (iterCount>0) // Infinite loop to keep the background flashing
        {
            // Change to red
            background.SetActive(true);
            yield return new WaitForSeconds(durationTime);

            // Change back to original color
            background.SetActive(false);
            yield return new WaitForSeconds(durationTime);
            iterCount--;
        }
    }

    private bool IsPreactionPos()
    {
        return TargetPos.Equals(preactionPos.position);
    }

    private bool IsSeperatePos()
    {
        return TargetPos.Equals(seperatePos.position);
    }

    protected bool IsCorrectHit()
    {
        return TargetPos.Equals(hitPos.position);
    }

    
}
