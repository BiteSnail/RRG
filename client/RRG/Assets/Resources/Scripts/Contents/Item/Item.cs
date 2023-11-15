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

    // Mixed老 版快 备己窍绰 Item
    [SerializeField]
    public Item pair_first = null;
    [SerializeField]
    public Item pair_second = null;
}