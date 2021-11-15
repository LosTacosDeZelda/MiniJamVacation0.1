﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    float stopwatch = 0;
    public TMP_Text stopwatchText;

    [Header("Chick Variables")]
    public GameObject chickPrefab;
    public int nbOfChicksAtStart;

    [Header("Tourists Variables")]
    public int nbOfTouristsAtStart;
    public GameObject[] tourists;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbOfChicksAtStart; i++)
        {
           Instantiate(chickPrefab, new Vector3(Random.Range(-34,34), Random.Range(-20, 15),0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stopwatch += Time.deltaTime;

        if (stopwatch > 59)
        {
            if (stopwatch % 60 == 0)
            {
                stopwatchText.text = "Time : " + (Mathf.RoundToInt(stopwatch) / 60).ToString() + "min ";
            }
            else
            {
                stopwatchText.text = "Time : " + (Mathf.RoundToInt(stopwatch) / 60).ToString() + "min " + Mathf.RoundToInt(stopwatch % 60).ToString() + " sec";
            }
           
        }
        else
        {
            stopwatchText.text = "Time : " + Mathf.RoundToInt(stopwatch).ToString() + " sec";
        }
        
    }

    void IncrementTime()
    {

    }
}