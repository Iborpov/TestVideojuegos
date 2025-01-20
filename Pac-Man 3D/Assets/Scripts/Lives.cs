using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lives : Singleton<Lives>
{
    int lives = 3;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public int LoseLive()
    {
        lives -= 1;
        return lives;
    }

    public int AddLive()
    {
        lives += 1;
        return lives;
    }

    //-------------------------------------------------------------
    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int newlives)
    {
        lives = newlives;
    }
}
