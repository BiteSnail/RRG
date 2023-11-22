using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaInfo
{
    private int wrongCount;
    public int WrongCount { get { return this.wrongCount; } }
    private int correctCount;
    public int CorrectCount { get { return this.correctCount; } }

    public EncyclopediaInfo()
    {
        wrongCount = 0;
        correctCount = 0;
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
