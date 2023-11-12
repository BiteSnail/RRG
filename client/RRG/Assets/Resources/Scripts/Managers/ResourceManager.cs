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
        Object[] objects = Resources.LoadAll("Prefabs/Items");
        foreach (Object obj in objects)
        {
            Item item = (Item)GameObject.Instantiate(obj);
            items.Add(item.name, item);
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
