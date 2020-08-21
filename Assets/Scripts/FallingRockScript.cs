using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : MonoBehaviour
{

    private Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys the falling rocks on collision with person
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    //Destroys falling rocks after passing by ground
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (rbody.position.y < -0.5)
            Destroy(gameObject);
    }
}
