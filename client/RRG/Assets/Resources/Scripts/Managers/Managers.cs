using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //Managers
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    //Game Manager
    GameManager game = new GameManager();
    public static GameManager Game { get { Init(); return instance.game; } }

    //Resource Manager
    ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { Init(); return instance.resource; } }

    //Sound Manager
    SoundManager sound = new SoundManager();
    public static SoundManager Sound { get { Init(); return instance.sound; } }

    SaveManager save = new SaveManager();
    public static SaveManager Save { get { Init(); return instance.save; } }

    private void Start()
    {
        Init();
        Resource.Start();
        sound.Start();
    }

    private void Update()
    {
        
    }

    static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
        }
    }
}
