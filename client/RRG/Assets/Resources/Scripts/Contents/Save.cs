using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private List<Item> wrongItems;
    private List<Item> correctItems;
    private StageBase stage;

    public Save(StageBase stage)
    {
        this.wrongItems = new List<Item>();
        this.correctItems = new List<Item>();
        this.stage = stage;
    }

    public void addWrong(Item item)
    {
        wrongItems.Add(item);
    }
    
    public void addCorrect(Item item)
    {
        correctItems.Add(item);
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
}
