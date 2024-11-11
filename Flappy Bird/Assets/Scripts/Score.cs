using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start() { audioData = GetComponent<AudioSource>();}

    // Update is called once per frame
    void Update() { }

    void OnTriggerExit2D(Collider2D collision)
    {
        audioData.Play();
        GameObject.Find("GameManager").GetComponent<GameManager>().score++;
    }
}
