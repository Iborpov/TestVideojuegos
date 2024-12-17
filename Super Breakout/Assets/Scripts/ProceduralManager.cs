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

    //Maximos de posiciones
    float minXPos = -8f;
    float maxXPos = 8f;

    float minYPos = 4f;
    float maxYPos = 0;

    //Nivel de dificultad - seed
    public int level = 1;

    float probability;

    //Sprites
    [SerializeField]
    private List<Sprite> sprites;

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
        float filas = Math.Clamp(UnityEngine.Random.Range(2, level), 2, 10);
        //Ladrillos minimos: 6 minimo - Ladrillos maximos: 110 / Maximo 11 por fila


        //Filas
        for (int i = 0; i < filas; i++)
        {
            //Vidas minimas: 1 - Vidas Maximas: 9
            int live = Math.Clamp(UnityEngine.Random.Range(1, level / (i + 1)), 1, 9);

            float cantidadFila = Math.Clamp(UnityEngine.Random.Range(1, level), 3, 11);
            //float cantidadFila = Math.Clamp(cantidadTotal, 4, 11);

            //Posición de la fila
            float yPos = Mathf.Lerp(minYPos, maxYPos, i / (filas - 1));

            //Columnas
            for (int j = 0; j < cantidadFila; j++)
            {
                GameObject brick = Instantiate(brickPref);
                brick.GetComponent<Brick>().lives = live; //Le pone la vida correspondiente
                //brick.GetComponent<Brick>().probPowerup = probability; //Le añade una probabilidad de soltar poweup
                //Posicion del ladrillo en columna
                float xPos = Mathf.Lerp(minXPos, maxXPos, j / (cantidadFila - 1));

                brick.transform.position = new Vector2(xPos, yPos); //Se coloca el ladrillo eb una posición
                brick.transform.parent = bricks.transform; //Se añade el ladrillo al padre bricks
            }
        }
    }
}

//El número de filas de ladrillos
//El número de ladrillos en cada fila
//El color de los ladrillos (para simplificar, se puede seleccionar el mismo color para toda la fila)
//Vidas de cada ladrillo (por coherencia, también se puede hacer que todos los ladrillos de una fila tengan las mismas vidas)
