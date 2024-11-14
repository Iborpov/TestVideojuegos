using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : Singleton<PowerUps>
{
    [SerializeField]
    GameObject extraLife;

    [SerializeField]
    int tipe;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (tipe == 0)
        {
            AddLive();
        }
        else if (tipe == 1)
        {
            ExtraBall();
        }
        else if (tipe == 2)
        {
            BigBall();
        }
        else if (tipe == 3)
        {
            SmallBall();
        }
    }

    void AddLive()
    {
        Lives.Instance.lives += 1;
    }

    void ExtraBall() {
        
     }

    void BigBall() { }

    void SmallBall() { }
}
