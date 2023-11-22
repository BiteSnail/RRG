using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextCanvas : MonoBehaviour
{
    public GameObject Title;
    public GameObject Description;
    public GameObject ItemImage;
    private Item currentItem;
    private Item CurrentItem { get { return Managers.Encyclopedia.CurrentItem; } }
    void Start()
    {
        if(CurrentItem == null)
        {
            return;
        }
        ItemImage.GetComponent<Image>().sprite = CurrentItem.GetComponent<SpriteRenderer>().sprite;
        ItemImage.GetComponent<Image>().color = Color.black;
        
        if (CurrentItem.isEncounter)
        {
            Title.GetComponent<TextMeshProUGUI>().text = CurrentItem.itemName;
            Description.GetComponent<TextMeshProUGUI>().text = CurrentItem.itemInfo;
            ItemImage.GetComponent<Image>().color = Color.white;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
