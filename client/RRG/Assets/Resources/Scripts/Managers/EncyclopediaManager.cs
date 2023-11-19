using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EncyclopediaManager
{
    private Dictionary<Item, EncyclopediaInfo> items;
    public Dictionary<Item, EncyclopediaInfo> Items { get { return this.items; } }
    private string path = "encyclopediaData.json";

    private Item currentItem;
    public Item CurrentItem { get { return this.currentItem; } }

    public void Start()
    {
       if(File.Exists(Path.Combine(Application.dataPath, path)))
        {
            items = Managers.Save.loadFromLocal<Dictionary<Item, EncyclopediaInfo>>(path);
            return;
        }
        Init();
    }

    private void Init()
    {
        List<Item> itemList = Managers.Resource.ItemList;
        items = new Dictionary<Item, EncyclopediaInfo>();
        foreach (Item item in itemList)
        {
            items.Add(item, new EncyclopediaInfo());
        }
    }
    private void saveToLocal()
    {
        Managers.Save.saveToLocal(path, items);
    }

    public void setCurrentItem(Item item)
    {
        this.currentItem = item;
    }

}
