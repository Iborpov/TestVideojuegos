using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerControllerState
    {
        Idle,
        Run,
        Jump,
        DoubleJump,
        OnWall,
    }

    Animator animator;
    Rigidbody2D rbody;

    PlayerControllerState state = PlayerControllerState.Idle;

    float movement;
    float jump;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //Controlar la orientación

        //Establecer parámetros del animator

        if (movement != 0)
        {
            animator.SetBool("Running", true);
            state = PlayerControllerState.Run;
            if (movement < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void GetInput()
    {
        //Leer inputs del jugador (derecha/izquierda y salto)
        movement = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");
    }

    private void FixedUpdate()
    {
        Debug.Log(state);
        switch (state)
        {
            case PlayerControllerState.Idle:
                Idle();
                break;
            case PlayerControllerState.Run:
                Run();
                break;
            case PlayerControllerState.Jump:
                Jump();
                break;
            case PlayerControllerState.DoubleJump:
                DoubleJump();
                break;
            case PlayerControllerState.OnWall:
                OnWall();
                break;
        }
    }

    private void Idle()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player

        if (movement != 0)
        {
            state = PlayerControllerState.Run;
        }

        if (jump != 0)
        {
            state = PlayerControllerState.Jump;
        }
    }

    private void Run()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
        rbody.velocity = new Vector2(movement * 5, rbody.velocity.y);

        if (movement == 0)
        {
            state = PlayerControllerState.Idle;
        }

        if (jump != 0)
        {
            state = PlayerControllerState.Jump;
        }
    }

    private void Jump()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
        rbody.velocity = new Vector2(rbody.velocity.x, jump * 5);

        if (rbody.velocity.y == 0)
        {
            state = PlayerControllerState.Idle;
        }
    }

    private void DoubleJump()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
    }

    private void OnWall()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
    }
}
