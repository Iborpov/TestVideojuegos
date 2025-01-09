using System;
using System.Collections;
using System.Collections.Generic;
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

    public enum PlayerImput
    {
        Left,
        Rigth,
        Jump,
        NoImput,
    }

    Animator animator;

    PlayerControllerState state = PlayerControllerState.Idle;
    PlayerImput imput = PlayerImput.NoImput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //Controlar la orientación

        //Establecer parámetros del animator

        if (imput == PlayerImput.Rigth)
        {
            animator.SetBool("Running", true);
        }
    }

    private void GetInput()
    {
        //Leer inputs del jugador (derecha/izquierda y salto)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            imput = PlayerImput.Jump;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            imput = PlayerImput.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            imput = PlayerImput.Rigth;
        }
    }

    private void FixedUpdate()
    {
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
    }

    private void Run()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
    }

    private void Jump()
    {
        //Comprobar si tengo que cambiar de estado
        //Mover al player
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
