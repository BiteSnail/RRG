using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string /*id*/, Item /*item*/> items = new Dictionary<string, Item>();
    public Dictionary<string, Item> Items { get { return items; } }
    private List<Item> itemList = new List<Item>();
    public List<Item> ItemList { get { return itemList; } }

    private Dictionary<string /*name*/, AudioClip /*Audio*/> audios = new Dictionary<string , AudioClip >();

   
    public void Start()
    {
        LoadItems();
        LoadAudioClips();
    }
     
    private void LoadItems()
    {
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Plastic");
            foreach (Item obj in objects)
            {
                items.Add(obj.name, obj);
                itemList.Add(obj);
            }
                
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Can");
            foreach (Item obj in objects)
            {
                items.Add(obj.name, obj);
                itemList.Add(obj);
            }

        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Paper");
            foreach (Item obj in objects)
            {
                items.Add(obj.name, obj);
                itemList.Add(obj);
            }
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Glass");
            foreach (Item obj in objects)
            {
                items.Add(obj.name, obj);
                itemList.Add(obj);
            }
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/General");
            foreach (Item obj in objects)
            {
                items.Add(obj.name, obj);
                itemList.Add(obj);
            }
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Mixed");
            foreach(Item obj in objects)
            {
                Items.Add(obj.name, obj);
                itemList.Add(obj);
            }
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Dirty");
            foreach (Item obj in objects)
            {
                Items.Add(obj.name, obj);
                itemList.Add(obj);
            }
        }
    }

    private void LoadAudioClips()
    {

        audios.Add("School", Resources.Load<AudioClip>("Sounds/School"));
        audios.Add("School_cut", Resources.Load<AudioClip>("Sounds/School_cut"));
        audios.Add("station_cut", Resources.Load<AudioClip>("Sounds/station_cut"));
        audios.Add("ItemSpawn", Resources.Load<AudioClip>("Sounds/SE/blop"));
        audios.Add("Beat", Resources.Load<AudioClip>("Sounds/SE/beat"));
        audios.Add("Apart", Resources.Load<AudioClip>("Sounds/apartment"));

        audios.Add("Can", Resources.Load<AudioClip>("Sounds/SE/can"));
        audios.Add("Paper", Resources.Load<AudioClip>("Sounds/SE/paper"));
        audios.Add("Plastic", Resources.Load<AudioClip>("Sounds/SE/plastic"));
        audios.Add("Glass", Resources.Load<AudioClip>("Sounds/SE/fresh_snap-37385"));
        audios.Add("General", Resources.Load<AudioClip>("Sounds/SE/punch-boxing"));
        audios.Add("Fail", Resources.Load<AudioClip>("Sounds/SE/failure-drum"));
    }

    public Item GetItem(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }
        else
        {
            return null;
        }   
    }

    public Item GetRandomItemExceptMixedAndDirty()
    {
        int randNum = Random.Range(0, itemList.Count);
        while(itemList[randNum].type == ItemType.Mixed || itemList[randNum].type == ItemType.Dirty)
            randNum = Random.Range(0, itemList.Count);
        return itemList[randNum];
    }

    public Item GetRandomItemExceptDirty()
    {
        int randNum = Random.Range(0, itemList.Count);
        while (itemList[randNum].type == ItemType.Dirty)
            randNum = Random.Range(0, itemList.Count);
        return itemList[randNum];
    }

    public AudioClip GetAudio(string name)
    {
        if (audios.ContainsKey(name))
        {
            return audios[name];
        }
        else
        {
            return null;
        }
    }
}
