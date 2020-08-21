using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    private Rigidbody2D rbody;
    private float initial_x;
    private float initial_y;
    private bool sinking;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        initial_x = rbody.position.x;
        initial_y = rbody.position.y;
        sinking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sinking)
        {
            rbody.velocity = new Vector2(0, -0.00001f);
        }
    }
    
    //Moves the platform back to its starting position when called
    public void ResetPlatform()
    {
        sinking = false;
        rbody.velocity = new Vector2(0, 0);
        rbody.position = new Vector2(initial_x, initial_y);
    }

    //starts to move down when the player jumps on to the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.ToLower().Equals("player"))
        {
            sinking = true;
        }
    }

    //stops moving when the player jumps off the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name.ToLower().Equals("player"))
        {
            sinking = false;
        }
    }
}
