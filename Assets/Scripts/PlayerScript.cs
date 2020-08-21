using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public MSMScript msm;
    public float hSpeed;

    private Rigidbody2D rbody;
    private Transform trans;

    private float initialElevation;
    private Vector2 initialPosition;

    public bool onGround;
    private bool canMove;
    private bool sinking;
    private float delta;

    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
        trans = gameObject.GetComponent<Transform>();
        initialPosition = rbody.position;
        initialElevation = initialPosition.y;
        onGround = true;
        canMove = true;
        sinking = false;
        hSpeed = 3.5f;
    }

    void Update()
    {
        //user loses control of object and sinks in quicksand when necessary
        if (sinking)
        {
            rbody.velocity = new Vector2(0, -0.00001f);
        }
        //player can move left or right and can jump when on ground (cannot fly)
        else if (canMove)
        {
            trans.position += new Vector3(hSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0.0f);

            bool jump = Input.GetKeyDown(KeyCode.Space);

            if (jump && onGround)
            {
                msm.PlayJumpSound();
                rbody.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colName = collision.collider.name.ToLower();
        PhysicsMaterial2D mat = collision.collider.sharedMaterial;
        //player dies and restarts the game when he falls off the cliff
        if (mat != null && mat.name.Equals("Ground"))
        {
            if (!canMove || initialElevation - rbody.position.y > 4) msm.ResetEverything();
        }
        //player dies and restarts game when he hits any of the moving objects/quicksand/spikes
        if (colName.StartsWith("spike") || colName.StartsWith("fallingrock") || 
            colName.StartsWith("falling") || colName.StartsWith("movingrock"))
        {
            msm.ResetEverything();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PhysicsMaterial2D mat = collision.collider.sharedMaterial;
        //keeps track of the position of the ground
        if (mat != null && mat.name.Equals("Ground"))
        {
            initialElevation = rbody.position.y;
        }
    }

    //Resets player back at the beginning of the game when called
    public void ResetPlayer()
    {
        trans.position = initialPosition;
        initialElevation = initialPosition.y;
        onGround = true;
        canMove = true;
        sinking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string name = other.gameObject.name.ToLower();
        //user loses control of the player when he falls into the pit of spikes
        if (name.Equals("pittrigger")) canMove = false;
        //triggers rocks to start falling when player steps onto that ground
        else if (name.Equals("fallingrockstrigger")) msm.StartFallingRocks();
        //resets the game when the player falls into the quicksand while on top of the platform
        else if (name.StartsWith("falling") || name.Equals("quicksandtrigger")) msm.ResetEverything();
        //user loses control of the player when he touches the quicksand
        else if (name.Equals("quicksand")) sinking = true;
        //finishes the game when player reaches the house
        else if (name.Equals("housetrigger")) msm.OnDelivery();
    }
}
