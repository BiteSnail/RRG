using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float calcRatio()
    {
        float correct = Managers.Save.getCorrects().Count;
        float wrong = Managers.Save.getWrongs().Count;
        return correct / (correct + wrong);
    }
    void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();

        slider.value = calcRatio();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
