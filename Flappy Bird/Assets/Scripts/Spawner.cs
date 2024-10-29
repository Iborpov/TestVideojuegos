using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipesPrefab;

    float timePassedSinceLastPipe = 0f;
    float randomTime = 2f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        var started = GameObject.Find("GameManager").GetComponent<GameManager>().started;
        if (started)
        {
            timePassedSinceLastPipe += Time.deltaTime;

            if (timePassedSinceLastPipe > randomTime)
            {
                timePassedSinceLastPipe = 0;
                randomTime = Random.Range(1.5f, 5f);

                CreateRandom();
            }
        }
    }

    void CreateRandom()
    {
        var pipes = Instantiate(pipesPrefab);
        pipes.transform.position = new Vector2(10, Random.Range(-3f, 3f));
        var topPipe = pipes.transform.GetChild(0);
        var bottomPipe = pipes.transform.GetChild(1);
    }
}
