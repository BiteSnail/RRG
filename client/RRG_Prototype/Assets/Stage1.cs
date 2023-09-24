using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    public AudioSource musicSource;

    [SerializeField]
    public AudioSource effectSource;

    public bool[] ishitBeat = new bool[71];
    public int nowBeatIndex = 0;
    float time = 0f;

    [SerializeField]
    TextMeshProUGUI rightOrWrongText;

    [SerializeField]
    Image image;

    int trashNum = 0;

    int[] hitBeatNums = { 4, 8, 12, 16, 20, 24, 26, 28, 30, 32, 40 };

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = Resources.Load<AudioClip>("Music/Music_Sample");
        effectSource.clip = Resources.Load<AudioClip>("Music/Effect_Sample");

        musicSource.Play();
        time = 0f;

        foreach(int num  in hitBeatNums)
        {
            ishitBeat[num] = true;
        }

        Color newColor = image.color;
        newColor.a = 0f;
        image.color = newColor;
    }


    private void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
     
            if (Mathf.Abs(time - 60f / 70f) < 0.3f && ishitBeat[nowBeatIndex-1] == true)
            {
                rightOrWrongText.SetText("Good!");
                effectSource.clip = Resources.Load<AudioClip>("Music/Effect_Correct");
                effectSource.Play();
            }
            else
            {
                rightOrWrongText.SetText("Bad");
                effectSource.clip = Resources.Load<AudioClip>("Music/Effect_Wrong");
                effectSource.Play();
            }
        }


        // 7/6초 마다 
        if (Mathf.Abs(time - 60f / 70f) < 0.01f)
        {
            time = 0;
            //지금 hit 이면 이미지 띄운다 
            if (ishitBeat[nowBeatIndex] == true)
            {
                while (true)
                {
                    int randValue = Random.Range(0, 7);
                    if (trashNum != randValue)
                    {
                        trashNum = randValue;
                        image.sprite = Resources.Load<Sprite>("Images/glass" + randValue);
                        Color newColor = image.color;
                        newColor.a = 1f;
                        image.color = newColor;
                        effectSource.clip = Resources.Load<AudioClip>("Music/Effect_Sample");
                        effectSource.Play();
                        break;
                    }
                }
            }
            else if (nowBeatIndex > 0 && ishitBeat[nowBeatIndex-1] == true)
            {
                Color newColor = image.color;
                newColor.a = 0f;
                image.color = newColor;
            }

            nowBeatIndex++;
        }
    }


}
