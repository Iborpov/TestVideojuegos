using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padle : MonoBehaviour
{
    float speed = 5;
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle(Input.GetAxisRaw("Horizontal"));
    }

    void MovePaddle(float movement)
    {
        rbody.velocity = new Vector2(movement * speed, rbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        var ball = GameObject.FindAnyObjectByType<Ball>();

        var direction = collisionInfo.transform.position;

        if (direction.x < rbody.transform.position.x-0.5)
        { //Izquierda
            Debug.Log("Izquierda"+direction.x +" "+rbody.transform.position.x);
            ball.ChangeDirection(-2.5f);
        }
        else if (direction.x > rbody.transform.position.x+0.5)
        { //Derecha
            Debug.Log("Derecha"+direction.x +" "+rbody.transform.position.x);
            ball.ChangeDirection(2.5f);
        }
    }
}
