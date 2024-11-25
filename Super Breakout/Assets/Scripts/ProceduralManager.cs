using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProceduralManager : MonoBehaviour
{
    [SerializeField]
    GameObject brickPref;

    [SerializeField]
    GameObject bricks;

    [SerializeField]
    TextMeshProUGUI levelText;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Nivel " + level;
    }

    public void GenerateLevel()
    {
        int filas = Math.Clamp(level, 1, 10);
        int cantidad = Math.Clamp(level * UnityEngine.Random.Range(1, 3), 5, 110);

        for (int i = 0; i < cantidad; i++)
        {
            GameObject brick = Instantiate(brickPref);
            brick.GetComponent<Brick>().lives = Math.Clamp(
                UnityEngine.Random.Range(level, level * 2),
                1,
                9
            );
        }
    }
}
