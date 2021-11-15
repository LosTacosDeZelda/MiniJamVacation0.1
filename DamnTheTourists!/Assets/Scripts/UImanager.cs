using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject HowToPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToToggle()
    {
        //TODO : if howto panel isnt activated, activate it. If it is, deactivate it
        HowToPanel.SetActive(!HowToPanel.activeInHierarchy);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
