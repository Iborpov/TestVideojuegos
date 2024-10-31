using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool started = false;
    public GameObject startPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    Scene scene;
    Bird bird;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<Scene>();
        bird = GameObject.Find("Bird").GetComponent<Bird>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                started = true;
                NewGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                started = true;
                Paused(false);
            }
        }
        else
        {
            scoreText.text = score.ToString();
            scene.Scroll();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                started = false;
                Paused(true);
            }
        }
    }

    public void NewGame()
    {
        startPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);
        bird.active = true;

        var pipes = GameObject.Find("pipesPrefab").GetComponent<Pipes>();
        if (pipes != null)
        {
            pipes.DestroyPipe();
        }
        
        
    }

    public void LoseGame()
    {
        started = false;
        bird.active = false;
        losePanel.gameObject.SetActive(true);
        var scored = losePanel.transform.GetChild(1);
        //scored.text = "Has conseguido "+score+" puntos";
        score = 0;
    }

    void Paused(bool paused){
        if (paused)
        {
            bird.active = false;
            pausePanel.SetActive(true);     
        } else
        {
            bird.active = true;
            pausePanel.SetActive(false);
        }
    }
}
