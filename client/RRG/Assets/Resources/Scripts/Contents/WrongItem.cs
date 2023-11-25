using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrongItem : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemInfo;
    void Start()
    {

    }

    public void registerItem(Item item)
    {
        image.sprite = item.GetComponent<SpriteRenderer>().sprite;
        itemName.text = item.itemName;
        itemInfo.text = item.itemInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
