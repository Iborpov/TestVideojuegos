using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public Ball clasebola;
    public GameObject player1;
    public GameObject player2;

    private int score1 = 0;
    private int score2 = 0;
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI winText;
    public GameObject panel;
    bool gameActive = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == false)
        {
            ball.transform.position = new Vector2(0, 0);
            if (ball.transform.position.x == 0 && ball.transform.position.y == 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    NewGame();
                }
            }
            else
            {
                Debug.Log("Ball postion ERROR");
            }
        }
    }

    public void NewGame()
    {
        gameActive = true;
        panel.gameObject.SetActive(false);
        ball.transform.position = new Vector2(0, 0);
        player1.transform.position = new Vector2(-8, 0);
        player2.transform.position = new Vector2(8, 0);
        clasebola.Launch();
    }

    public void Scored(bool goal1)
    {
        gameActive = false;
        if (goal1)
        {
            score2 += 1;
            scoreText2.text = score2.ToString();
            ChangePaddles(player2, player1);
            if (Win(score2))
            {
                winText.text = "Gana el jugador 2";
                panel.gameObject.SetActive(true);
                Debug.Log("Ha marcado el jugador 2: " + score2);
                ResetGame();
            }
        }
        else
        {
            score1 += 1;
            scoreText1.text = score1.ToString();
            ChangePaddles(player1, player2);
            if (Win(score1))
            {
                winText.text = "Gana el jugador 1";
                panel.gameObject.SetActive(true);
                Debug.Log("Ha marcado el jugador 1: " + score1);
                ResetGame();
            }
        }
    }

    bool Win(int score)
    {
        bool wins = false;
        if (score >= 5)
        {
            wins = true;
        }
        return wins;
    }

    void ResetGame()
    {
        score2 = 0;
        score1 = 0;
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
        player1.transform.localScale = new Vector3(0.5f, 3, 1);
        player2.transform.localScale = new Vector3(0.5f, 3, 1);
    }

    void ChangePaddles(GameObject winer, GameObject loser)
    {
        Vector3 scaleWiner = new Vector2(0, -0.5f);
        winer.transform.localScale += scaleWiner;
        Vector3 scaleLoser = new Vector2(0, 0.5f);
        loser.transform.localScale += scaleLoser;
    }
}
