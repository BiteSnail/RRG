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
        Title.GetComponent<TextMeshProUGUI>().text = CurrentItem.itemName;
        Description.GetComponent<TextMeshProUGUI>().text = CurrentItem.itemInfo;
        ItemImage.GetComponent<Image>().sprite = CurrentItem.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
