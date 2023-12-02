using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3 : StageBase
{
    // Update is called once per frame
    class PosItem
    {
        Item item;
        int posIdx;
    }
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
    private List<KeyValuePair<Item, int>> items = new List<KeyValuePair<Item, int>>();
    private Item Item { get { 
            if (items.Count == 0) 
                return null;
            return items[0].Key;
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

    protected void DestroyItem(KeyValuePair<Item, int> temp)
    {
        if(items == null)
        {
            return;
        }
        items.Remove(temp);
        Destroy(temp.Key.gameObject);
        pressed = false;
    }

    private bool ValidateKeyCode(KeyCode code)
    {
        return !pressed && Input.GetKeyDown(code);
    }

    private bool ValidateItemType(ItemType type, Item currentItem)
    {
        return type == currentItem.type;
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

    private void Seperate(KeyValuePair<Item, int> currentItem)
    {
        if (!ValidateItemType(ItemType.Mixed, currentItem.Key))
            return;
        if (ValidateKeyCode(KeyCode.Space))
        {
            if (currentItem.Key.pair_second)
                items.Add(KeyValuePair.Create(
                    instantiateItem(currentItem.Key.pair_second, itemPositions[currentItem.Value]), 
                    currentItem.Value)
                    );
            if (currentItem.Key.pair_first)
                items.Add(KeyValuePair.Create(
                    instantiateItem(currentItem.Key.pair_first, itemPositions[currentItem.Value - 1]), 
                    currentItem.Value)
                    );

            DestroyItem();

            pressed = false;
        }
        
        
    }

    private void Hit(KeyValuePair<Item, int> currentItem)
    {
        if (pressed)
            return;
        // 정확한 키를 눌렀는지 확인
        foreach (var codeAndType in codeToItem)
        {
            if (ValidateKeyCode(codeAndType.Key) && ValidateItemType(codeAndType.Value, currentItem.Key))
            {
                TargetPos = itemToPos[codeAndType.Value].position;
                Managers.Sound.Play(itemToAudio[codeAndType.Value]);
                Managers.Save.correct(currentItem.Key);
                return;
            }
        }
        //실패 시 Fail
        pressed = true;
        StartCoroutine(ChangeColorOverTime());
        Managers.Sound.Play("Fail");
        Managers.Save.wrong(currentItem.Key);
    }

    private void ButtonProcess()
    {
        foreach(var currentItem in items)
        {
            if (IsPreactionPos(currentItem.Key.transform))
            {
                Preaction();
            }
            if (IsSeperatePos(currentItem.Key.transform))
            {
                Seperate(currentItem);
            }
            if (IsCorrectHit(currentItem.Key.transform))
            {
                if (Input.anyKeyDown)
                {
                    Hit(currentItem);
                }
            }
        }
    }

    private void CleanProcess()
    {
        foreach (var currentItem in items)
        {
            foreach (var pos in GameObject.FindGameObjectsWithTag("Finish"))
            {
                if (TargetPos.Equals(pos.transform.position))
                    DestroyItem(currentItem);
            }
        }
    }

    private void TurnOnOffGuide()
    {
        foreach (var currentItem in items)
        {
            if (ValidateItemType(ItemType.Mixed, currentItem.Key))
            {
                keyAnimaiton.SetActive(true);
                return;
            }
        }
        keyAnimaiton.SetActive(false);
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
            TurnOnOffGuide();

            if (isHitBeat[nowBeatIndex] == true)
            {
                //while (Item) DestroyItem();
                Item randomItem = Managers.Resource.GetRandomItemExceptDirty();
                randomItem.isEncounter = true;
                items.Add(KeyValuePair.Create(
                    instantiateItem(randomItem, itemSpawnPos.transform),
                    0
                    ));

                pressed = false;
                isCorrect = false;
                Item.transform.localPosition = new Vector2(0, 0);
                Managers.Sound.Play("ItemSpawn");
                nowPosIdx = 0;
            }
            if(items.Count > 0)
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
        for (int i=0; i< items.Count; i++)
        {
            items[i] = KeyValuePair.Create(items[i].Key, items[i].Value + 1);
            items[i].Key.transform.position = itemPositions[items[i].Value].position;
        }
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

    private bool IsPreactionPos(Transform pos)
    {
        return TargetPos.Equals(pos.position);
    }

    private bool IsSeperatePos(Transform pos)
    {
        return TargetPos.Equals(pos.position);
    }

    protected bool IsCorrectHit(Transform pos)
    {
        return TargetPos.Equals(pos.position);
    }

    
}
