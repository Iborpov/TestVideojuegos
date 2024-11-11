using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int lives = 3;

    [SerializeField]
    GameObject ballpref;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate() { }

    public void Loselive()
    {
        lives -= 1;
        if (lives <= 0)
        {
            Gamelose();
        }
        else
        {
            Newball();
        }
    }

    void Gamelose() { 
        Debug.Log("GAME OVER");
    }

    void Newball()
    {
        var padle = GameObject.Find("Padle");
        var ball = FindFirstObjectByType<Ball>().gameObject;
        Destroy(ball);
        GameObject newball = Instantiate(ballpref);
        newball.transform.parent = padle.transform;
    }
}
