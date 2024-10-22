using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool goal1;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        var go = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        go.Scored(goal1);
        
        
    }
}
