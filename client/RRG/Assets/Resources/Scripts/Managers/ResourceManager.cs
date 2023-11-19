using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string /*id*/, Item /*item*/> items = new Dictionary<string, Item>();
    private List<Item> itemList = new List<Item>();

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


    }

    private void LoadAudioClips()
    {

        audios.Add("School", Resources.Load<AudioClip>("Sounds/School"));
        audios.Add("School_cut", Resources.Load<AudioClip>("Sounds/School_cut"));
        audios.Add("ItemSpawn", Resources.Load<AudioClip>("Sounds/SE/blop"));
        audios.Add("Beat", Resources.Load<AudioClip>("Sounds/SE/beat"));

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

    public Item GetRandomItem()
    {
        int randNum = Random.Range(0, itemList.Count);
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
