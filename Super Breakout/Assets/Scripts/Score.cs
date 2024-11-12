using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : Singleton<Score>
{
    public int score = 0;

    List<int> topten;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int add){
        score += add;
        Debug.Log(score+" Puntos");
    }

    public void TopScore(){
        //for (int i = 0; i < topten.Count; i++) { }
    }
}
