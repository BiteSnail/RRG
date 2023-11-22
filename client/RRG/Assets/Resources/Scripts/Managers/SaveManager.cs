using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager
{
    private List<Save> saves;
    private Save currentSave;
    private string path = "playerData.json";


    public void Start()
    {
        LoadSaves();
    }

    private void LoadSaves()
    {
        if(File.Exists(Path.Combine(Application.dataPath, path)))
        {
            saves = loadFromLocal<List<Save>>(path);
            return;
        }
        this.saves = new List<Save>();
    }

    public void startRecording(StageBase stage)
    {
        this.currentSave = new Save(stage);
    }

    public void wrong(Item item)
    {
        eraseCloneText(item);
        currentSave.addWrong(item);
    }

    public void correct(Item item)
    {
        eraseCloneText(item);
        currentSave.addCorrect(item);
    }

    public string report()
    {
        saves.Add(currentSave);
        currentSave = null;
        return string.Format("wrongs: %d \ncorrect: %d", currentSave.getWrongScore(), currentSave.getCorrectScore());
    }

    public void saveToLocal()
    {
        saveToLocal(path, this.saves);
    }
    
    public void saveToLocal<T>(string path, T obj)
    {
        string jsonData = JsonUtility.ToJson(obj);
        File.WriteAllText(Path.Combine(Application.dataPath, path), jsonData);
    }

    public T loadFromLocal<T>(string path)
    {
        string jsonData = File.ReadAllText(Path.Combine(Application.dataPath, path));
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void eraseCloneText(Item item)
    {
        item.name = item.name.Split("(")[0];
    }
}
