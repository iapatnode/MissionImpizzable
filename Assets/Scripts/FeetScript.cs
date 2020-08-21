using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of whether or not the player is on the ground
public class FeetScript : MonoBehaviour
{

    public PlayerScript player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.onGround = false;
    }
}
