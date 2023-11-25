using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public GameObject wrongItem;
    void Start()
    {
        foreach(string item in Managers.Save.getWrongs())
        {
            Debug.Log(item);
            GameObject listItem = Instantiate(wrongItem);
            listItem.transform.SetParent(gameObject.transform);
            listItem.GetComponent<WrongItem>().registerItem(Managers.Resource.GetItem(item));
            listItem.transform.position.Set(0, 0, 0);
            listItem.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
