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
    int nbOfChicksKidnapped = 0;

    [Header("Chick Variables")]
    public GameObject chickPrefab;
    public int nbOfChicksAtStart;

    [Header("Tourists Variables")]
    public int nbOfTouristsAtStart;
    public float touristMinSpawnTime = 3f;
    public float touristMaxSpawnTime = 8f;
    public GameObject[] tourists;
    public Transform[] spawnPoints;

    public TMP_Text scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < nbOfChicksAtStart; i++)
        {
            Instantiate(chickPrefab, new Vector3(Random.Range(-7.24f, 7.43f), Random.Range(5.491206f, -5.89f), 0), Quaternion.identity);
        }
        
        spawnTourists();

        chicksKidnapped.text = "Chicks Kidnapped : 0";
    }

    // Start is called before the first frame update
    void spawnTourists()
    {
        for (int i = 0; i < nbOfTouristsAtStart; i++)
        {
            int randTouristIndex = Random.Range(0, tourists.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(tourists[randTouristIndex], spawnPoints[randSpawnPoint].position, Quaternion.identity);
        }

        nbOfTouristsAtStart += 1;
        Invoke("spawnTourists", Random.Range(touristMinSpawnTime, touristMaxSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
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
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (scoreText == null)
            {
                scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
            }

            if (nbOfChicksKidnapped < 3)
            {
                //You managed to save most of of chicks ! Congrats !
                scoreText.text = "You managed to save most of of chicks ! Congrats !";
            }
            else
            {
                //You lost... The tourists fled with your chicks...
                scoreText.text = "You lost... The tourists fled with your chicks...";
            }
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
