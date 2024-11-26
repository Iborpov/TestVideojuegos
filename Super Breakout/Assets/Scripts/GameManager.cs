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

    [SerializeField]
    List<AudioClip> sounds;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        NextLevel();

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
        SoundManager.Instance.PlayClip(sounds[1]);
        SceneManager.LoadScene(0);
    }

    //Primer nivel del modo limitado
    public void ModoNormal()
    {
        SoundManager.Instance.PlayClip(sounds[0]);
        Lives.Instance.SetLives(3);
        SceneManager.LoadScene(1);
    }

    //Pasa a la siguiente nivel/pantalla del modo limitado o si detecta que no quedan bloques a la pantalla de victoria
    void NextLevel()
    {
        if (briks != null) //Comprueba que exista el grupo de ladrillos
        {
            if (briks.transform.childCount <= 0) // Entra si no queda ninguno
            {
                if (SceneManager.GetActiveScene().buildIndex == 6) //Si el nivel es el infinito
                {
                    Newball();
                    var pm = FindAnyObjectByType<ProceduralManager>();
                    pm.level += 1;
                    pm.GenerateLevel();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 5) //Si es el ultimo nivel del modo normal
                {
                    Win();
                }
                else //Pasa de escena/nivel del modo normal
                {
                    SoundManager.Instance.PlayClip(sounds[5]);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    //Modo creado procedimentalmente
    public void ModoInfinito()
    {
        SoundManager.Instance.PlayClip(sounds[0]);
        Lives.Instance.SetLives(6);
        SceneManager.LoadScene(6);
    }

    //Pantalla del top 10 de mejores puntuaciónes
    public void PantallaScore()
    {
        SceneManager.LoadScene(9);
    }

    //Pantalla para ragistrar una puntuación nueva
    public void PantallaGuardar()
    {
        SceneManager.LoadScene(10);
    }

    //Pantalla de fin de juego
    void Gamelose()
    {
        SoundManager.Instance.PlayClip(sounds[3]);
        Debug.Log("GAME OVER");
        SceneManager.LoadScene(8);
        Lives.Instance.SetLives(3);
    }

    //Pantalla de victoria del modo limitado
    void Win()
    {
        SoundManager.Instance.PlayClip(sounds[2]);
        SceneManager.LoadScene(7);
        Score.Instance.TopScore(Score.Instance.score);
    }

    //Se pierde una vida
    public void Loselive()
    {
        SoundManager.Instance.PlayClip(sounds[4]);
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

    void SkipLevel()
    {
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
