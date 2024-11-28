using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProceduralManager : MonoBehaviour
{
    [SerializeField]
    GameObject brickPref;

    [SerializeField]
    GameObject bricks;

    [SerializeField]
    TextMeshProUGUI levelText;


    float minXPos = -8;
    float maxXPos = 7;

    float minYPos = 0;
    float maxYPos = 4.25f;

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
        //Filas minimas: 1 - Filas Maximas: 10
        float filas = Math.Clamp(level * UnityEngine.Random.Range(0.5f, 3), 1, 10);
        //Ladrillos minimos: 6 minimo 3 por fila - Ladrillos maximos: 110 / Maximo 11 por fila 
        int cantidadTotal = Math.Clamp(level * UnityEngine.Random.Range(1, 3), 6, 110);
        for (int i = 0; i < filas; i++)
        {
            float cantidadFila = Math.Clamp(cantidadTotal/filas,3, 11);
            for (int j = 0; j < cantidadFila; j++)
            {
                
                GameObject brick = Instantiate(brickPref);
                brick.GetComponent<Brick>().lives = Math.Clamp(
                    UnityEngine.Random.Range(level, level * 2),
                    1,
                    9
                );
                float xPos = Mathf.Lerp(minXPos, maxXPos, j / cantidadFila);
                float yPos = Mathf.Lerp(minYPos, maxYPos, i / filas);
                brick.transform.position = new Vector2(xPos, yPos);
                brick.transform.parent = bricks.transform;
            }
        }
    }
}

//El número de filas de ladrillos
//El número de ladrillos en cada fila
//El color de los ladrillos (para simplificar, se puede seleccionar el mismo color para toda la fila)
//Vidas de cada ladrillo (por coherencia, también se puede hacer que todos los ladrillos de una fila tengan las mismas vidas)
