using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Score : Singleton<Score>
{
    public int score = 0;

    public List<int> topten = new List<int>(10) { 1000, 900, 800, 550, 50 };

    [SerializeField]
    TextMeshProUGUI scoretext;

    // Start is called before the first frame updatechild
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (scoretext != null)
        {
            PrintScore();
        }
        else
        {
            GameObject gotext = GameObject.Find("ScoreText");
            if (gotext != null)
            {
                scoretext = gotext.GetComponent<TextMeshProUGUI>();
            }
            Debug.LogWarning("No existe el TextMeshPro");
        }
    }

    //Recibe la puntuación obtenida y la suma a la existente
    public void AddScore(int add)
    {
        score += add;
        Debug.Log(score + " Puntos");
    }

    void PrintScore()
    { //Actualiza el texto de la puntuación
        scoretext.text = score + " Puntos";
    }

    public void TopScore(int newNumber)
    {
        topten.Sort((a, b) => b.CompareTo(a));
        if (newNumber > topten[topten.Count - 1])
        {
            // Insertar el nuevo número en la posición correcta para mantener el orden descendente
            int index = topten.FindIndex(x => x < newNumber);
            topten.Insert(index, newNumber);

            // Elimina el último elemento para mantener la lista con 10 elementos
            topten.RemoveAt(topten.Count - 1);
        }
    }
}
