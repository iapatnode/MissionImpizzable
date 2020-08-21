using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class EndSceneManagerScript : MonoBehaviour
{

    public Text stats;

    void Start()
    {
        int attempts = PlayerPrefs.HasKey("attempts") ? PlayerPrefs.GetInt("attempts") : 1;
        int minutes = PlayerPrefs.HasKey("minutes") ? PlayerPrefs.GetInt("minutes") : 0;
        int seconds = PlayerPrefs.HasKey("seconds") ? PlayerPrefs.GetInt("seconds") : 0;
        string duration = minutes + (seconds < 10 ? ":0" : ":") + seconds;
        int totalSeconds = seconds + 60 * minutes;
        int tip = System.Math.Max(10 + (30 - totalSeconds), 0) * 2;
        stats.text = "Attempts: " + attempts + "\nDuration: " + duration + "\nReceived tip: $" + tip;
    }

    // Update is called once per frame
    void Update()
    {
        //Goes to main scene when esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnMainMenuButtonClick();
        }
    }

    //Loads main menu 
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
