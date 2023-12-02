using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3 : StageBase
{
    // Update is called once per frame
    class PosItem
    {
        private Item item;
        public Item Item { get { return item; } set { item = value; } }
        private int posIdx;
        public int PosIdx { get { return posIdx; } set { posIdx = value; } }

        public PosItem(Item item, int posIdx=0)
        {
            this.item = item;
            this.posIdx = posIdx;
        }

        public void MoveNext(Transform[] positions)
        {
            item.transform.position = positions[++posIdx].position;
        }
    }
    class PosItemList
    {
        private List<PosItem> items;
        private Transform[] positions;
        private void InstantiateItem(Item origin, int posIdx)
        {
            Item newItem = Instantiate(origin);
            newItem.transform.position = positions[posIdx].position;
            newItem.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
            items[posIdx] = new PosItem(newItem, posIdx);
        }

        private void DestroyItem(PosItem item)
        {
            Destroy(item.Item.gameObject);
        }
        private void DestroyItem(int idx)
        {
            DestroyItem(items[idx]);
            items[idx] = null;
        }

        public PosItemList(Transform[] positions)
        {
            this.positions = positions;
            items = Enumerable.Repeat<PosItem>(null, positions.Length).ToList();
        }

        public void MoveAll()
        {
            if (items[^1] != null)
                Remove(items.Count - 1);
            for (int i = items.Count-1; i >= 0; i--)
            {
                if (items[i] == null)
                    continue;
                items[i].MoveNext(positions);
                items[i + 1] = items[i];
                items[i] = null;
            }
        }

        public void Insert(Item item, int posIdx=0)
        {
            InstantiateItem(item, posIdx);
        }

        public void Remove(int idx=0)
        {
            DestroyItem(idx);
        }

        public bool IsNull(int idx=0)
        {
            return items[idx] == null;
        }

        public Item At(int idx=0)
        {
            return items[idx].Item;
        }
        public void Seperate(int idx)
        {
            PosItem temp = items[idx];
            if (temp.Item.pair_first)
                Insert(temp.Item.pair_first, idx);
            if (temp.Item.pair_second)
                Insert(temp.Item.pair_second, idx - 1);
            DestroyItem(temp);
        }
    }
    public GameObject background;
    public Transform[] itemPositions;
    public Transform itemGotoPos;
    public GameObject keyAnimaiton;
    bool pressed = false;
    private Dictionary<KeyCode, ItemType> codeToItem;
    private Dictionary<ItemType, string> itemToAudio;
    private PosItemList posItems;
    private const int SEPERATE_POS_IDX = 3;
    private const int HIT_POS_IDX = 6;
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
        itemToAudio = new Dictionary<ItemType, string>
        {
            {ItemType.General, "General" },
            {ItemType.Plastic, "Plastic" },
            {ItemType.Can, "Can" },
            {ItemType.Glass, "Glass" },
            {ItemType.Paper, "Paper" }
        };
        posItems = new PosItemList(itemPositions);
    }

    public override void Start()
    {
        base.Start();
        Managers.Sound.Play("station_cut", SoundManager.Sound.Bgm);
        Managers.Save.startRecording(this);
        Init();
        StartCoroutine(EndStage());
    }

    private bool ValidateKeyCode(KeyCode code)
    {
        return Input.GetKeyDown(code);
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

    private void Seperate()
    {
        if (posItems.IsNull(SEPERATE_POS_IDX))
            return;

        if (!ValidateItemType(ItemType.Mixed, posItems.At(SEPERATE_POS_IDX)))
            return;
        keyAnimaiton.SetActive(true);
        if (ValidateKeyCode(KeyCode.Space))
        {
            posItems.Seperate(SEPERATE_POS_IDX);
            keyAnimaiton.SetActive(false);
        }
    }

    private void Hit()
    {
        if (posItems.IsNull(HIT_POS_IDX))
            return;

        if (ValidateItemType(ItemType.Mixed, posItems.At(HIT_POS_IDX)))
            return;

        if (pressed)
            return;

        if (!Input.anyKeyDown)
            return;

        foreach (var codeAndType in codeToItem)
        {
            if (ValidateKeyCode(codeAndType.Key) && 
                ValidateItemType(codeAndType.Value, posItems.At(HIT_POS_IDX)))
            {
                //성공했을 경우 이동하는 것 추가할 수 있음 Remove에서 다른 걸로 바꾸기
                Managers.Save.correct(posItems.At(HIT_POS_IDX));
                Managers.Sound.Play(itemToAudio[codeAndType.Value]);
                posItems.Remove(HIT_POS_IDX);
                return;
            }
        }
        //실패 시 Fail
        pressed = true;
        StartCoroutine(ChangeColorOverTime());
        Managers.Sound.Play("Fail");
        Managers.Save.wrong(posItems.At(HIT_POS_IDX));
    }

    private void ButtonProcess()
    {
        Seperate();
        Hit();
    }

    private void CreateAndMove()
    {
        nowBeatIndex++;
        currentTime -= 60d / bpm;
        pressed = false;
        posItems.MoveAll();
        if (!isHitBeat[nowBeatIndex])
            return;
        Item randomItem = Managers.Resource.GetRandomItemExceptDirty();
        randomItem.isEncounter = true;
        posItems.Insert(randomItem);
        Managers.Sound.Play("ItemSpawn");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        ButtonProcess();

        if (currentTime >= 60d / bpm) //매 박자마다
        {
            keyAnimaiton.SetActive(false);
            CreateAndMove();
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

}
