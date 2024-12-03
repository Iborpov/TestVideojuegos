using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopTen : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> toptexts;

    // Start is called before the first frame update
    void Start()
    {
        PrintTopTen();
    }

    // Update is called once per frame
    void Update() { }

    public void PrintTopTen()
    {
        for (int i = 0; i < 10; i++)
        {
            // Obtener el hijo en el índice i
            var tmp = toptexts[i];

            if (tmp != null)
            {
                if (Score.Instance.topten.Count > i)
                {
                    Debug.Log(Score.Instance.topten.Count);
                    // Asignar el valor de la lista convertido a texto
                    tmp.text =
                        i
                        + 1
                        + " - "
                        + Score.Instance.topten[i].Item2
                        + " - "
                        + Score.Instance.topten[i].Item1;
                }
                else
                {
                    tmp.text = i + 1 + " ------------------------------------";
                }
            }
            else
            {
                Debug.LogWarning("El hijo " + i + " no tiene un componente TextMeshPro.");
            }
        }
    }
}
