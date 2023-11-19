using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaInfo
{
    private int wrongCount;
    public int WrongCount { get { return this.wrongCount; } }
    private int correctCount;
    public int CorrectCount { get { return this.correctCount; } }
    private bool isEncounter;
    public bool IsEncounter { 
        get { return this.isEncounter; }
        set { this.isEncounter = true; }
    }
    public EncyclopediaInfo()
    {
        wrongCount = 0;
        correctCount = 0;
        isEncounter = false;
    }

    public void addWrong()
    {
        wrongCount++;
    }
    public void addCorrect()
    {
        correctCount++;
    }
}
