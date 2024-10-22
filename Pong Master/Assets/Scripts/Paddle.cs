using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool player1;
    public float speed = 5;
    public Rigidbody2D rbody;
    float movement;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (player1)
        {
            movement = Input.GetAxisRaw("Vertical");
            rbody.velocity = new Vector2(rbody.velocity.x, movement * speed);
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical2");
            rbody.velocity = new Vector2(rbody.velocity.x, movement * speed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
         var go = GameObject.Find("Ball").GetComponent<Ball>();
        go.AcelerateBall();
    }
}
