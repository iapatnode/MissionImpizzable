using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MSMScript : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public PlayerScript player;
    public PlatformScript platform;
    public MovingRockScript movingRock;
    public FallingRockScript fallingRock;
    public Text timerText;
    double startTime;
    int timePassed;
    private bool overtime;
    private int attempts;

    private System.Random rand;
    private bool fallingRocksEnabled;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        startTime = Time.time;
        timePassed = 0;
        overtime = false;
        attempts = 1;
        rand = new System.Random();
        fallingRocksEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Goes to main menu when player wants to quit the current game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        UpdateTimer();
        UpdateFallingRocks();
    }

    //sets Timer and times how long the player takes to finish the game
    private void UpdateTimer()
    {
        timePassed = (int)(Time.time - startTime);
        int min = timePassed / 60;
        int sec = timePassed % 60;
        if (sec > 9) timerText.text = min + ":" + sec;
        else timerText.text = min + ":0" + sec;
        if (timePassed > 30 && !overtime)
        {
            timerText.color = Color.red;
            overtime = true;
        }
    }

    private void UpdateFallingRocks()
    {
        if (!fallingRocksEnabled) return;
        //generates falling rocks when triggered
        if (rand.Next(50) == 0)
        {
            int xPos = rand.Next(3) - 9;
            int yPos = 1;
            Instantiate(fallingRock, new Vector2(xPos, yPos), Quaternion.identity);
        }
    }

    //Resets the entire game when the player dies and keeps track of how many times he has died
    public void ResetEverything()
    {
        audioSource.PlayOneShot(deathSound, 0.5f);
        startTime = Time.time;
        timerText.color = Color.white;
        StopFallingRocks();
        platform.ResetPlatform();
        movingRock.ResetMovingRock();
        player.ResetPlayer();
        attempts += 1;
    }

    //Finishes game when the player completes the delivery to the house
    public void OnDelivery()
    {
        timePassed = (int)(Time.time - startTime);
        PlayerPrefs.SetInt("attempts", attempts);
        PlayerPrefs.SetInt("minutes", timePassed / 60);
        PlayerPrefs.SetInt("seconds", timePassed % 60);
        SceneManager.LoadScene("EndScene");
    }
    
    //allows falling rocks to be generated
    public void StartFallingRocks()
    {
        fallingRocksEnabled = true;
    }

    //disables the generating of falling rocks
    public void StopFallingRocks()
    {
        fallingRocksEnabled = false;
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound, 6f);
    }

}
