using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool active = false;
    public Rigidbody2D rbody;

    public float JumpSpeed = 5f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            rbody.gravityScale = 1f;
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Jump();
            }
        } else
        {
            rbody.gravityScale = 0f;
            rbody.velocity = new Vector2(0f, 0f);
        }
    }

    public void Jump()
    {
        if (rbody.transform.position.y < 4f)
        {
         rbody.velocity = new Vector2(0f, JumpSpeed);   
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Estas Muerto");
        var gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.LoseGame();
        rbody.constraints = RigidbodyConstraints2D.None;
        rbody.gravityScale = 1f;
        
    }
}
