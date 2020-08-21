using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRockScript : MonoBehaviour
{
    private Transform tran;
    private Rigidbody2D rbody;
    private Vector2 startPosition;
    private float speed;

    void Start()
    {
        tran = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody2D>();
        startPosition = tran.position;
        speed = 1.5f;
    }

    void Update()
    {
        //moves downward when the rock is high or at the coordinates where it started
        if (tran.position.y >= startPosition.y)
            rbody.velocity = new Vector2(0, -speed);
    }

    //Resets the rock to its original position when caled
    public void ResetMovingRock()
    {
        rbody.position = startPosition;
        rbody.velocity = new Vector2(0, -speed);
    }

    //moves up towards starting position when it hits the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PhysicsMaterial2D mat = collision.collider.sharedMaterial;
        if (mat != null && mat.name.Equals("Ground"))
            rbody.velocity = new Vector2(0.0f, speed);
    }
}
