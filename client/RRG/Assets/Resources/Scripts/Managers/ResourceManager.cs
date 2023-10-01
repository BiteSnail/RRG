using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string /*id*/, GameObject /*item*/> items = new Dictionary<string, GameObject>();
    private Dictionary<string /*name*/, AudioClip /*Audio*/> audios = new Dictionary<string , AudioClip >();
   
    public void Start()
    {
        LoadItems();
        LoadAudioClips();
    }
     
    private void LoadItems()
    {
        //TODO 
        //������ ������ ������ �����ϰ� �ε�&�ν��Ͻ�ȭ�ϱ�
    }

    private void LoadAudioClips()
    {
        //TODO
        //����� Ŭ�� �ְ� �ε��ϱ�
    }

    public GameObject GetItem(string name)
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
