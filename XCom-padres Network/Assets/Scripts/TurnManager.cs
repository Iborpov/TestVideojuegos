using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    GameManager gm;

    [SerializeField]
    Transform uFather; //Transform padre de los diferentes grupos de unidades por jugador

    List<Transform> activeUnitsList = new List<Transform>(); //Lista de unidades activas
    int totalAP = 0; //Total de puntos de acción de las unidades activas

    public NetworkVariable<int> turn; // Numero del turno del jugador online
    public bool oflTurn = true; //Turno offline - True Jugador / False IA

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
        gm = FindFirstObjectByType<GameManager>();
        turns = canvas.transform.GetChild(0).gameObject;
        turnText = turns.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (gm.IsOnline)
        {
            if (IsServer)
            {
                players = uFather.childCount;
            }
            Debug.Log(players);

            //Turno del jugador 1
            turn = new NetworkVariable<int>(1);
            turnText.text = "Player 1 Turn";
            UnitsList(uFather.GetChild(0));
        }
        else
        {
            turnText.text = "Your Turn";
            UnitsList(uFather.GetChild(1));
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
            NextTurnServerRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void NextTurnServerRpc()
    {
        if (gm.IsOnline) //¿Es Online?
        {
            if (IsServer) //¿Es Servidor?
            {
                if (turn.Value + 1 > players)
                {
                    turn.Value = 1;
                }
                else
                {
                    turn.Value++;
                }
            }
        }
        else
        {
            if (oflTurn)
            {
                oflTurn = false;
            }
            else
            {
                oflTurn = true;
            }
        }

        NewActionPoints();
    }

    //Pasa de turno, reinicia los puntos y actualiza el canvas de turno
    public void NextTurn()
    {
        NextTurnServerRpc();
    }

    private void UpdateCanvas(int previousValue, int newValue)
    {
        turnText.text = "Player " + turn.Value + " Turn";
        int tn;

        if (gm.IsOnline)
        {
            tn = turn.Value;
        }
        else
        {
            tn = oflTurn ? 1 : 0;
        }

        switch (tn)
        {
            case 0: //Turno de la IA
                turnText.text = "Enemy Turn";
                turnText.color = Color.red;
                UnitsList(uFather.GetChild(0));
                return;
            case 1: //Turno del Jugador 1 o Singleplayer
                turnText.color = Color.green;
                if (!gm.IsOnline)
                {
                    turnText.text = "Your Turn";
                }
                UnitsList(uFather.GetChild(gm.IsOnline ? 0 : 1));
                return;
            case 2: //Turno del jugador 2
                turnText.color = Color.blue;
                UnitsList(uFather.GetChild(1));
                return;
            case 3: //Turno del jugador 3
                turnText.color = Color.yellow;
                UnitsList(uFather.GetChild(2));
                return;
            case 4: //Turno del jugador 4
                turnText.color = Color.magenta;
                UnitsList(uFather.GetChild(3));
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
        if (activeUnitsList != null) //¿Esta vacia la lista de unidades activa?
        {
            activeUnitsList.Clear();
        }

        foreach (Transform unit in units)
        {
            activeUnitsList.Add(unit);
        }
    }

    //Reinicia los puntos de todos las unidades activas en el turno
    void NewActionPoints()
    {
        foreach (Transform unit in activeUnitsList)
        {
            unit.GetComponent<Unit>().ResetActionPoints();
        }
    }

    //Si no es Servidor se subscrive al evento del cambio del canvas al iniciar la conexión
    public override void OnNetworkSpawn()
    {
        turn.OnValueChanged += UpdateCanvas;
    }

    //Si no es Servidor se desubscrive al evento del cambio del canvas al terminar la conexión
    public override void OnNetworkDespawn()
    {
        turn.OnValueChanged -= UpdateCanvas;
    }
}
