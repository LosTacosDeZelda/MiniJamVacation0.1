using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    float stopwatch = 0;
    public TMP_Text stopwatchText;
    public TMP_Text chicksKidnapped;
    public int nbOfChicksKidnapped = 0;

    [Header("Chick Variables")]
    public GameObject chickPrefab;
    public int nbOfChicksAtStart;

    [Header("Tourists Variables")]
    public int nbOfTouristsAtStart;
    public GameObject[] tourists;
    public Transform[] spawnPoints;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

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

        chicksKidnapped.text = "Chicks Kidnapped : 0";
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

    public void LoseAChick()
    {
        if (nbOfChicksKidnapped < 3)
        {
            nbOfChicksKidnapped++;
            chicksKidnapped.text = "Chicks Kidnapped : " + nbOfChicksKidnapped.ToString();
        }
        
        if(nbOfChicksKidnapped == 3)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        //The game is over, load endscene
        SceneManager.LoadScene(2);
    }
}
