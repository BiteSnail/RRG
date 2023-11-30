using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3 : StageBase
{
    // Update is called once per frame
    public Transform preactionPos;
    public Transform seperatePos;
    public Transform hitPos;
    public Transform itemSpawnPos;
    public Transform[] itemPositions;
    public Transform itemGotoPos;
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
            if (items.Count == 0)
            {
                items = null;
            }
        }
        Destroy(temp.gameObject);
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

    private void Seperate()
    {
        if (!ValidateKeyCode(KeyCode.Space))
            return;
        items = new List<Item>();
        if (item.pair_first)
            items.Add(Instantiate(item.pair_first));
        if (item.pair_second)
            items.Add(Instantiate(item.pair_second));
        item = null;
        pressed = false;
    }

    private void Hit()
    {
        if (pressed)
            return;
        foreach (var codeAndType in codeToItem)
        {
            if (ValidateKeyCode(codeAndType.Key) && ValidateItemType(codeAndType.Value))
            {
                TargetPos = Vector3.MoveTowards(TargetPos, itemToPos[codeAndType.Value].position, 1000 * Time.deltaTime);
                Managers.Sound.Play(itemToAudio[codeAndType.Value]);
                Managers.Save.correct(Item);
                isCorrect = true;
                return;
            }
        }
        pressed = true;
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
            Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                if (item) DestroyItem();
                Item randomItem = Managers.Resource.GetRandomItem();
                item = Instantiate(randomItem);
                randomItem.isEncounter = true;

                TargetPos = itemSpawnPos.transform.position;
                Item.GetComponent<SpriteRenderer>().sortingLayerName="Object";

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
            for (int i = 1; i < items.Count; i++)
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
