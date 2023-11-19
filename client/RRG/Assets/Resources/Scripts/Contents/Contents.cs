using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Contents : MonoBehaviour
{
    public GameObject Button;
    private int rowSize = 735;
    private int columnCountPerRow = 5;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Item item in Managers.Encyclopedia.Items.Keys)
        {
            createButton(item, Managers.Encyclopedia.Items[item]);
        }
        setComponents();
    }

    private void createButton(Item item, EncyclopediaInfo info)
    {
        // 아이템 각각에 대한 액자 생성
        GameObject button = Instantiate(Button);
        button.transform.SetParent(gameObject.transform);
        button.transform.position.Set(0, 0, 0);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.GetComponent<Button>().onClick.AddListener(() => { OnClickButton(item); });

        GameObject panel = button.transform.Find("Panel").gameObject;
        Image panelImage = panel.GetComponent<Image>();
        Item panelItem = panel.GetComponent<Item>();
        panelImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        panelImage.color = info.IsEncounter ? Color.white : Color.black;
        panelItem.itemName = item.itemName;
        panelItem.itemInfo = item.itemInfo;
    }
    private void setComponents()
    {
        gameObject.GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rowSize * (Managers.Encyclopedia.Items.Keys.Count / columnCountPerRow));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickButton(Item item)
    {
        Managers.Encyclopedia.setCurrentItem(item);
        SceneManager.LoadScene("Item_PictorialBook"); //Item_PictorialBook 씬으로 이동
    }
}
