using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padle : MonoBehaviour
{
    float speed = 5;
    float movement;
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
}
