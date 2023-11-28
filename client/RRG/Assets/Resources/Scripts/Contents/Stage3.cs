using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : StageBase
{
    // Update is called once per frame
    public Transform firstPos;
    public Transform secondPos;
    public Transform hitPos;
    Vector3 targetPos;
    int pressed = 0;

    private Dictionary<KeyCode, ItemType> codeTable;

    private void setCodeTable()
    {
        codeTable.Add(KeyCode.Space, ItemType.General);codeTable.Add(KeyCode.Q, ItemType.Plastic);
        codeTable.Add(KeyCode.Q, ItemType.Plastic);
        codeTable.Add(KeyCode.W, ItemType.Can);
        codeTable.Add(KeyCode.E, ItemType.Glass);
        codeTable.Add(KeyCode.R, ItemType.Paper);
    }
    
    private new void Start()
    {
        base.Start();
        setCodeTable();
        
    }

    private void processSpecialKey()
    {

    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (item == null)
            targetPos = Vector3.zero;

        if (Input.anyKeyDown)
        {
            foreach(var codeType in codeTable)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    processSpecialKey();
                    break;
                }
                if (Input.GetKeyDown(codeType.Key))
                {
                    switch (codeType.Value)
                    {
                        
                    }
                }
            }
        }

    }
}
