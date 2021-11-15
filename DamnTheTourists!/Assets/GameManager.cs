using System.Collections;
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
    public Transform[] spawnPoints;

    private void Awake()
    {
        for (int i = 0; i < nbOfChicksAtStart; i++)
        {
            Instantiate(chickPrefab, new Vector3(Random.Range(-34, 34), Random.Range(-20, 15), 0), Quaternion.identity);
        }

        for (int i = 0; i < nbOfTouristsAtStart; i++)
        {
            int randTouristIndex = Random.Range(0, tourists.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(tourists[randTouristIndex], spawnPoints[randSpawnPoint].position, Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
