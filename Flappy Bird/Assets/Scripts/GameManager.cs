using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool started = false;
    public GameObject panel;
    public GameObject bott;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = GameObject.Find("Scene").GetComponent<Scene>();
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
        }
        else
        {
            scoreText.text = score.ToString();
            scene.Scroll();
        }
    }

    public void NewGame()
    {
        panel.gameObject.SetActive(false);
        var bird = GameObject.Find("Bird").GetComponent<Bird>();
        bird.active = true;
    }
}
