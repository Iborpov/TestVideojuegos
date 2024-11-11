using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool started = false;
    public GameObject birdPrefab;
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
        bird = GameObject.Find("birdPrefab").GetComponent<Bird>();
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
        score = 0;
        startPanel.gameObject.SetActive(false);
        losePanel.gameObject.SetActive(false);

        var pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        foreach (var pipe in pipes)
        {
            pipe.GetComponent<Pipes>().DestroyPipe();
        }

        if (bird == null)
        {
            bird = Instantiate(birdPrefab).GetComponent<Bird>();
        }

        Debug.Log(GameObject.FindObjectOfType<Bird>());
        if (bird.transform.position.y != 0f)
        {
            bird.transform.position = new Vector2(0f, 0f);
        }
        Paused(false);
    }

    public void LoseGame()
    {
        started = false;
        GameObject.Destroy(FindAnyObjectByType<Bird>().gameObject);
        losePanel.gameObject.SetActive(true);
        var scored = losePanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Debug.Log(scored);
        scored.text = "Has conseguido " + score + " puntos";
        score = 0;
    }

    void Paused(bool paused)
    {
        if (paused)
        {
            bird.active = false;
            pausePanel.SetActive(true);
        }
        else
        {
            bird.active = true;
            pausePanel.SetActive(false);
        }
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
