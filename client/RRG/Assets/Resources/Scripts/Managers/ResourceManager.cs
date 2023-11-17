using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string /*id*/, Item /*item*/> items = new Dictionary<string, Item>();
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
                items.Add(obj.name, obj);
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Can");
            foreach (Item obj in objects)
                items.Add(obj.name, obj);

        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Paper");
            foreach (Item obj in objects)
                items.Add(obj.name, obj);
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/Glass");
            foreach (Item obj in objects)
                items.Add(obj.name, obj);
        }
        {
            Item[] objects = Resources.LoadAll<Item>("Prefabs/Items/General");
            foreach (Item obj in objects)
                items.Add(obj.name, obj);
        }


    }

    private void LoadAudioClips()
    {
        //TODO
        //오디오 클립 넣고 로드하기
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
        string[] itemTypes = { "CA", "PA", "PL", "VI", "GL", "PE", "GE", "MI" };
        int randType = Random.Range(0, itemTypes.Length);
        int randNum;
        Item ret = null;
        randNum = Random.Range(0, 20);

        while (!items.TryGetValue(itemTypes[randType] + randNum.ToString(), out ret))
        {
            randNum = Random.Range(0, 20);
        }

        return ret;
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
