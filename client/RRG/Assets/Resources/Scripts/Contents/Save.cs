using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private HashSet<Item> wrongItems;
    private HashSet<Item> correctItems;
    private StageBase stage;

    public Save(StageBase stage)
    {
        this.wrongItems = new HashSet<Item>();
        this.correctItems = new HashSet<Item>();
        this.stage = stage;
    }

    public void addWrong(Item item)
    {
        wrongItems.Add(getItem(item));
    }
    
    public void addCorrect(Item item)
    {
        correctItems.Add(getItem(item));
    }

    public void updateDictionary()
    {
        return;
    }

    public int getWrongScore()
    {
        return wrongItems.Count;
    }
    
    public int getCorrectScore()
    {
        return correctItems.Count;
    }

    public Item getItem(Item item)
    {
        return Managers.Resource.GetItem(item.name);
    }
}
