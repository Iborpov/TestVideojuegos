using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    bool online = false; //¿Es una partida online?

    [SerializeField]
    Transform uf; //Transform padre de los diferentes grupos de unidades por jugador

    List<Transform> activeUnitsList = new List<Transform>(); //Lista de unidades activas
    int totalAP = 0; //Total de puntos de acción de las unidades activas

    public int turn; // number of the player turn

    public int players; // Number of players in game

    [Header("Canvas")]
    [SerializeField]
    GameObject canvas;

    [SerializeField]
    GameObject textPrefab;

    GameObject turns;
    TextMeshProUGUI turnText;

    void Awake()
    {
        turns = canvas.transform.GetChild(0).gameObject;
        turnText = turns.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        players = uf.childCount;

        //Turno del jugador 1
        turn = 1;
        if (online)
        {
            turnText.text = "Player 1 Turn";
            UnitsList(uf.GetChild(0));
        }
        else
        {
            turnText.text = "Your Turn";
            UnitsList(uf.GetChild(1));
        }
        turnText.color = Color.green;

        CanvasTurnUnits();
    }

    void Update()
    {
        CanvasTurnUnits();

        //Suma todos los puntos de la unidades activas
        for (int i = 0; i < activeUnitsList.Count(); i++)
        {
            totalAP += activeUnitsList[i].GetComponent<Unit>().GetActionPoints();
        }
        if (totalAP <= 0)
        {
            NextTurn();
        }
    }

    //Pasa de turno, reinicia los puntos y actualiza el canvas de turno
    public void NextTurn()
    {
        turn++;
        if (online)
        {
            if (turn > players)
            {
                turn = 1;
            }
        }
        else
        {
            if (turn > 1)
            {
                turn = 0;
            }
        }

        NewActionPoints();

        turnText.text = "Player " + turn + " Turn";

        switch (turn)
        {
            case 0:
                turnText.text = "Enemy Turn";
                turnText.color = Color.red;
                UnitsList(uf.GetChild(0));
                return;
            case 1:
                turnText.color = Color.green;

                if (online)
                {
                    UnitsList(uf.GetChild(0));
                }
                else
                {
                    turnText.text = "Your Turn";
                    UnitsList(uf.GetChild(1));
                }
                return;
            case 2:
                turnText.color = Color.blue;
                UnitsList(uf.GetChild(1));
                return;
            case 3:
                turnText.color = Color.yellow;
                UnitsList(uf.GetChild(2));
                return;
            case 4:
                turnText.color = Color.magenta;
                UnitsList(uf.GetChild(3));
                return;
            default:
                return;
        }
    }

    //Obtiene las unidades activas en el turno para pintar el canvas
    public void CanvasTurnUnits()
    {
        Transform panel = turns.transform.GetChild(1);
        foreach (Transform textPanel in panel)
        {
            Destroy(textPanel.gameObject);
        }
        for (int i = 0; i < activeUnitsList.Count(); i++)
        {
            var unitComponent = activeUnitsList[i].GetComponent<Unit>();
            GameObject text = Instantiate(textPrefab, panel);
            text.GetComponent<TextMeshProUGUI>().text =
                activeUnitsList[i].name + " | " + unitComponent.GetActionPoints() + "AP";
        }
    }

    public void UnitsList(Transform units)
    {
        if (activeUnitsList != null)
        {
            activeUnitsList.Clear();
        }

        foreach (Transform unit in units)
        {
            activeUnitsList.Add(unit);
        }
    }

    void NewActionPoints()
    {
        foreach (Transform unit in activeUnitsList)
        {
            unit.GetComponent<Unit>().ResetActionPoints();
        }
    }
}
