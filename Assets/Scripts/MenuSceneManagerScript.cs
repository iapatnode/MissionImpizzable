using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuSceneManagerScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        //Quits the game when the esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnQuitButtonClick();
        }
    }

    //Loads the game when player is ready to start
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene1");
    }

    //Quits the game
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
