using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBase : MonoBehaviour
{
    public int bpm = 60; //���� �ʿ�
    protected double currentTime = 0;
    public double exceedRange = 0.2f;

    public int[] hitBeatNums; //������������ �ۼ� �ʿ�
    protected bool[] isHitBeat = new bool[200];
    protected int nowBeatIndex = 0;

    protected Item item = null;

    public Sprite gameBackground;
    public Sprite openingBackground;

    public float itemMoveSpeed = 10f;
    public GameObject canPos;
    public GameObject plasticPos;
    public GameObject paperPos;
    public GameObject glassPos;
    public GameObject generalPos;


    public virtual void Start()
    {
        //StartCoroutine(Opening());
        foreach (int num in hitBeatNums)
            isHitBeat[num] = true;
    }


    protected void DestroyItem()
    {
        if (item != null)
        {
            Destroy(item.gameObject);
            item = null;
        }
    }

   //IEnumerator Opening()
   // {
   //     GameObject  
   //         .Find("Background")
   //         .GetComponent<SpriteRenderer>()
   //         .sprite = openingBackground;

   //     yield return new WaitForSeconds(3.0f);
   //     GameObject
   //         .Find("Background")
   //         .GetComponent<SpriteRenderer>()
   //         .sprite = gameBackground;

   //     gameStarted = true;
   // }
}
