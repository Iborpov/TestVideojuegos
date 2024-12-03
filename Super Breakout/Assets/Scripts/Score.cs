using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class Score : Singleton<Score>
{
    public int score = 0;

    public List<Tuple<int, string>> topten = new List<Tuple<int, string>>();

    [SerializeField]
    TextMeshProUGUI scoretext;

    private string filePath;

    // Start is called before the first frame updatechild
    void Start()
    {
        // Ruta del archivo CSV
        filePath = Path.Combine(Application.persistentDataPath, "scores.csv");

         // Verificar si el archivo existe, y si no, crearlo con datos iniciales
        if (!File.Exists(filePath))
        {
            CreateDefaultCsv();
        }

        // Leer los datos del archivo al inicio
        LoadScoresFromFile();
    }

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
            //Debug.LogWarning("No existe el TextMeshPro");
        }
    }

    private void CreateDefaultCsv()
    {
        // Datos iniciales de ejemplo
        var defaultScores = new List<Tuple<int, string>>
        {
            Tuple.Create(1000, "ElPro69"),
            Tuple.Create(900, "Pepe"),
            Tuple.Create(800, "Amador"),
            Tuple.Create(550, "Elena"),
            Tuple.Create(50, "asdfXD")
        };

        // Crear el archivo CSV con los datos iniciales
        List<string> lines = new List<string>();
        foreach (var entry in defaultScores)
        {
            lines.Add($"{entry.Item1},{entry.Item2}");
        }

        File.WriteAllLines(filePath, lines);

        Debug.Log("Archivo CSV creado con valores predeterminados en: " + filePath);
    }

    public void LoadScoresFromFile()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            topten.Clear(); // Limpia la lista actual

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 2 && int.TryParse(parts[0], out int score))
                {
                    string name = parts[1];
                    topten.Add(Tuple.Create(score, name));
                }
            }

            // Ordenar por puntuación descendente
            topten.Sort((a, b) => b.Item1.CompareTo(a.Item1));
        }
        else
        {
            Debug.LogWarning(
                "No se encontró el archivo de puntuaciones. Usando valores predeterminados."
            );
        }
    }

    public void SaveScoresToFile()
    {
        List<string> lines = new List<string>();

        foreach (var entry in topten)
        {
            lines.Add($"{entry.Item1},{entry.Item2}");
        }

        File.WriteAllLines(filePath, lines);
    }

    public void AddNewScore(int newScore, string playerName)
    {
        if (newScore > 0)
        {
            // Agregar la nueva tupla
            topten.Add(Tuple.Create(newScore, playerName));

            // Ordenar por puntuación descendente
            topten.Sort((a, b) => b.Item1.CompareTo(a.Item1));

            // Limitar la lista a los 10 mejores
            if (topten.Count > 10)
            {
                topten.RemoveAt(topten.Count - 1);
            }

            // Guardar en el archivo
            SaveScoresToFile();
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

    public void TopScore(int newScore, string playerName)
    {
        // Ordenar la lista por puntuación descendente
        topten.Sort((a, b) => b.Item1.CompareTo(a.Item1));

        // Verificar si el nuevo puntaje entra en la lista
        if (newScore > topten[topten.Count - 1].Item1)
        {
            // Crear la nueva entrada
            Tuple<int, string> newEntry = Tuple.Create(newScore, playerName);

            // Insertar en la posición correcta
            int index = topten.FindIndex(entry => entry.Item1 < newScore);
            topten.Insert(index, newEntry);

            // Eliminar el último elemento para mantener la lista con 10 elementos
            topten.RemoveAt(topten.Count - 1);
        }
    }
}
