using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private HashSet<string> wrongItems;
    public HashSet<string> WrongItems { get { return wrongItems; } }
    private HashSet<string> correctItems;
    public HashSet<string> CorrectItems { get { return correctItems; } }
    private string stage;
    public string Stage { get { return stage; } }

    public Save(StageBase stage)
    {
        this.wrongItems = new HashSet<string>();
        this.correctItems = new HashSet<string>();
        this.stage = stage.GetType().Name;
    }

    public void addWrong(Item item)
    {
        wrongItems.Add(item.name);
    }
    
    public void addCorrect(Item item)
    {
        correctItems.Add(item.name);
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
