using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Lives : Singleton<Lives>
{
    private int lives = 3;

    [SerializeField]
    private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int newlives){
        lives = newlives;
    }

    public Sprite GetSprite(int i){
        return sprites[i];
    }

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
}
