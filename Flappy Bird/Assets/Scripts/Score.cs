using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().score++;

        //score += 1;
    }
}
