using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public string itemInfo;

    [SerializeField]
    public ItemType type = ItemType.General;

    // Mixed일 경우 구성하는 Item
    [SerializeField]
    public Item pair_first = null;
    [SerializeField]
    public Item pair_second = null;
    public bool isEncounter = false;
    public Item preActionItem = null;
}