using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEditor.SearchService;
using UnityEngine;

public class Stage2 : StageBase
{
    [SerializeField] int itemSpawnXStart;
    [SerializeField] int itemSpawnXEnd;
    [SerializeField] float itemSpawnY;
    [SerializeField] float hitY;

    private void Update()
    {
        if (currentTime >= 60d / bpm) //매 박자마다
        {
            nowBeatIndex++;
            currentTime -= 60d / bpm;
            //Managers.Sound.Play("Beat");
            if (isHitBeat[nowBeatIndex] == true)
            {
                ItemSpawn();
                Managers.Sound.Play("ItemSpawn");
            }
            //if (targetPos == itemGotoPos.transform.position)
            {
                DropItem();
            }
        }
    }

    void ItemSpawn()
    {
        if (item) DestroyItem();
        Item randomItem = Managers.Resource.GetRandomItem();
        item = GameObject.Instantiate(randomItem);
        randomItem.isEncounter = true;

        int randX = Random.Range(itemSpawnXStart, itemSpawnXEnd);
        item.transform.position = new Vector2(randX, itemSpawnY);
        
    }
    void DropItem()
    {
        item.transform.position = new Vector2(item.transform.position.x, transform.position.y + 1);
    }
}
