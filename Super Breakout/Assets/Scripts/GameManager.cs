using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    [SerializeField]
    GameObject ballpref;

    [SerializeField]
    GameObject briks = null;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        Selector(SceneManager.GetActiveScene().buildIndex);
        if (briks != null)
        {
            if (briks.transform.childCount <= 0)
            {
                if (SceneManager.GetActiveScene().buildIndex == 5)
                {
                    Win();
                }
                else
                {
                    NextLevel();
                }
            }
        }
    }

    void FixedUpdate() { }

    void Selector(int indx)
    {
        if (indx == 0) //Pantalla de inicio
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("Entra2");
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                //SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                SceneManager.LoadScene(8);
            }
        }
        else if (indx == 7) //Pantalla de Game Over
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                SceneManager.LoadScene(0);
            }
        }
        else //Todas las demas
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Loselive()
    {
        Lives.Instance.lives -= 1;
        if (Lives.Instance.lives <= 0)
        {
            Gamelose();
        }
        else
        {
            Newball();
        }
    }

    void Gamelose()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(7);
    }

    void Newball()
    {
        var padle = GameObject.Find("Padle");
        var ball = FindFirstObjectByType<Ball>().gameObject;
        Destroy(ball);
        GameObject newball = Instantiate(ballpref);
        newball.transform.parent = padle.transform;
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Win()
    {
        SceneManager.LoadScene(6);

    }
}
