using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField]
    string itemName;

    [SerializeField]
    string itemInfo;

    [SerializeField]
    ItemType type = ItemType.General;

    // Mixed일 경우 구성하는 Item
    [SerializeField]
    Item pair_first = null;
    [SerializeField]
    Item pair_second = null;
}