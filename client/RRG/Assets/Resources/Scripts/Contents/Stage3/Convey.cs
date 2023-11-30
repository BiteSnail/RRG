using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;

    private Item item;

    void Start()
    {
        for(int i= 0;i< gameObject.transform.childCount; i++)
        {
            transforms.Add(gameObject.transform.GetChild(i).GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
