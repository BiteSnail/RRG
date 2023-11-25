using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultReaction : MonoBehaviour
{
    public Sprite positiveSprite;
    public Sprite negativeSprite;
    public Slider slider;
    private Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        if (slider.value >= 0.5)
        {
            image.sprite = positiveSprite;
        }
        else
        {
            image.sprite = negativeSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
