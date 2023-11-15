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

    // Mixed�� ��� �����ϴ� Item
    [SerializeField]
    public Item pair_first = null;
    [SerializeField]
    public Item pair_second = null;
}