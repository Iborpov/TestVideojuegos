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
        NextLevelNormal();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PantallaStart();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SkipLevel();
        }
    }

    void FixedUpdate() { }

    //Pantalla de titulo del juego
    public void PantallaStart()
    {
        SceneManager.LoadScene(0);
    }

    //Primer nivel del modo limitado
    public void ModoNormal()
    {
        Lives.Instance.SetLives(3);
        SceneManager.LoadScene(1);
    }

    //Pasa a la siguiente nivel/pantalla del modo limitado o si detecta que no quedan bloques a la pantalla de victoria
    void NextLevelNormal()
    {
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    //Modo creado procedimentalmente
    public void ModoInfinito()
    {
        //SceneManager.LoadScene(1);
    }

    //Pantalla del top 10 de mejores puntuaciónes
    public void PantallaScore()
    {
        SceneManager.LoadScene(8);
    }

    //Pantalla para ragistrar una puntuación nueva
    public void PantallaGuardar()
    {
        SceneManager.LoadScene(9);
    }

    //Pantalla de fin de juego
    void Gamelose()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(7);
    }

    //Pantalla de victoria del modo limitado
    void Win()
    {
        SceneManager.LoadScene(6);
        Score.Instance.TopScore(Score.Instance.score);
    }

    //Se pierde una vida
    public void Loselive()
    {
        int lives = Lives.Instance.LoseLive();
        if (lives <= 0)
        {
            Gamelose();
        }
        else
        {
            Newball();
        }
    }

    //Creación de una bola nueva al perder la anterior
    public void Newball()
    {
        var padle = GameObject.Find("Padle");
        var ball = FindFirstObjectByType<Ball>().gameObject;
        Destroy(ball);
        GameObject newball = Instantiate(ballpref);
        newball.transform.parent = padle.transform;
    }


    void SkipLevel() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Funcion para cerrar el juego
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
