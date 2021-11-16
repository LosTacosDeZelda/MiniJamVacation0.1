using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager current;
    Scene currentScene;

    [SerializeField] float stopwatchSecs = 0;
    [SerializeField] float stopwatchMins = 0;
    public TMP_Text stopwatchText;
    public TMP_Text chicksKidnapped;
    [SerializeField] int nbOfChicksKidnapped = 0;

    [Header("Chick Variables")]
    public GameObject chickPrefab;
    public int nbOfChicksAtStart;

    [Header("Tourists Variables")]
    public int nbOfTouristsAtStart;
    public float touristMinSpawnTime = 3f;
    public float touristMaxSpawnTime = 8f;
    public GameObject[] tourists;
    public GameObject[] spawnPoints;

    public TMP_Text scoreText;
    bool gameHasStarted = false;

    private void Awake()
    {
        //Singleton code
        if (GameObject.FindGameObjectWithTag("GameController") && GameObject.FindGameObjectWithTag("GameController") != this.gameObject) Destroy(GameObject.FindGameObjectWithTag("GameController"));


        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    void SpawnChicksAtStart()
    {
        //Instantiate chicks at start
        for (int i = 0; i < nbOfChicksAtStart; i++)
        {
            Instantiate(chickPrefab, new Vector3(Random.Range(-7.24f, 7.43f), Random.Range(5.491206f, -5.89f), 0), Quaternion.identity);
        }

        chicksKidnapped.text = "Chicks Kidnapped : 0";
    }

    void SpawnTouristsAtStart()
    {
        for (int i = 0; i < nbOfTouristsAtStart; i++)
        {
            int randTouristIndex = Random.Range(0, tourists.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(tourists[randTouristIndex], spawnPoints[randSpawnPoint].transform.position, Quaternion.identity);
        }

        StartCoroutine(SpawnTouristsRepeatedly());
    }

    IEnumerator SpawnTouristsRepeatedly()
    {
        int randTouristIndex = Random.Range(0, tourists.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(tourists[randTouristIndex], spawnPoints[randSpawnPoint].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(touristMinSpawnTime, touristMaxSpawnTime));

        StartCoroutine(SpawnTouristsRepeatedly());
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        switch (currentScene.buildIndex)
        {
            case 0:
                break;

            case 1:

                if (stopwatchText == null)
                {
                    stopwatchText = GameObject.FindGameObjectWithTag("StopwatchText").GetComponent<TMP_Text>();
                }

                if (chicksKidnapped == null)
                {
                    chicksKidnapped = GameObject.FindGameObjectWithTag("ChicksText").GetComponent<TMP_Text>();
                    
                }

                spawnPoints = GameObject.FindGameObjectsWithTag("TouristSpawnPoint");

                if (gameHasStarted == false)
                {
                    gameHasStarted = true;
                    SpawnChicksAtStart();
                    SpawnTouristsAtStart();
                }

                if (Mathf.RoundToInt(stopwatchSecs) == 60)
                {
                    stopwatchSecs = 0;
                    stopwatchMins++;
                }
                else
                {
                    stopwatchSecs += Time.deltaTime;
                }

                //stopwatchMins = Mathf.RoundToInt(stopwatchSecs) / 60;

                stopwatchText.text = $"Time : {stopwatchMins} min {Mathf.RoundToInt(stopwatchSecs)} secs";

                break;

            case 2:

                if (scoreText == null)
                {
                    scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
                }

                //You lost... The tourists fled with your chicks...
                scoreText.text = $"You lost... The tourists fled with your chicks... But you managed to keep the tourists away for {stopwatchMins} minutes {Mathf.RoundToInt(stopwatchSecs)} seconds !";
                
                break;

            default:
                break;
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
        //Reset game variables
        nbOfChicksKidnapped = 0;
        gameHasStarted = false;

        //The game is over, load endscene
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        //Reset timer
        stopwatchSecs = 0;
        stopwatchMins = 0;
        SceneManager.LoadScene(0);
    }
}
