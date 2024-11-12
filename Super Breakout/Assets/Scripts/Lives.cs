using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Lives : Singleton<Lives>
{

    public int lives = 3;
    [SerializeField]
    List<Sprite> sprites;
    // Start is called before the first frame update
    void Start()
    {
        //LoadHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void LoadHearts(){
    //     for (int i = 0; i < lives; i++)
    //     {
    //         GameObject imgObject = new GameObject("live"+(i+1));
    //         Image image = imgObject.AddComponent<Image>();
    //     }
    // }
}
