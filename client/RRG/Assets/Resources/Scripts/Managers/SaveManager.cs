using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    private List<Save> saves;
    private Save currentSave;
    
    void Start()
    {
        LoadSaves();
    }

    private void LoadSaves()
    {
        this.saves = new List<Save>();
        //파일에서 불러오는 것도 구상 중
    }

    public void startRecording(StageBase stage)
    {
        this.currentSave = new Save(stage);
    }

    public void wrong(Item item)
    {
        currentSave.addWrong(item);
    }

    public void correct(Item item)
    {
        currentSave.addCorrect(item);
    }

    public string report()
    {
        saves.Add(currentSave);
        currentSave = null;
        return string.Format("wrongs: %d \ncorrect: %d", currentSave.getWrongScore(), currentSave.getCorrectScore());
    }
}
