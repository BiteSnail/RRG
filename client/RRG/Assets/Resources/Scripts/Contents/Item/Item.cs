using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite sprite;

    [SerializeField]
    string itemName;

    [SerializeField]
    string itemInfo;

    [SerializeField]
    ItemType type = ItemType.General;

    // Mixed�� ��� �����ϴ� Item
    [SerializeField]
    Item pair_first = null;
    [SerializeField]
    Item pair_second = null;

}