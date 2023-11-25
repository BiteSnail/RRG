using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReTry_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameScnesCtrl()
    {
        SceneManager.LoadScene(Managers.Save.getStageName()); //Stage3 æ¿¿∏∑Œ ¿Ãµø
    }
}
