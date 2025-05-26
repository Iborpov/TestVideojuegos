using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    public ulong player;
    private HealthSystem healthSystem;
    GameObject selectedVisuals;
    public Animator animatior;

    [SerializeField]
    int maxPointsTurn = 2;

    public NetworkVariable<int> actionPoints;
    int oflActionPoints;

    public int attackRange = 3;
    public int attack = 20;

    public GridPosition gridPosition;

    BaseAction[] dispActions;

    GameObject canvas;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        selectedVisuals = transform.GetChild(1).gameObject;
        selectedVisuals.SetActive(false);
        canvas = transform.GetChild(2).gameObject;
        animatior = transform.GetChild(0).GetComponent<Animator>();
        animatior.SetBool("IsActive", false);
        dispActions = GetComponents<BaseAction>();
        ResetActionPoints();
    }

    void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(this);
        TextMeshProUGUI name = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        name.text = this.name;
        switch (player)
        {
            case 1:
                name.color = Color.green;
                break;
            case 2:
                name.color = Color.blue;
                break;
            case 3:
                name.color = Color.yellow;
                break;
            case 4:
                name.color = Color.magenta;
                break;

            default:
                return;
        }
    }

    public override void OnNetworkSpawn()
    {
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnDead += HealthSystem_OnDead;
        canvas.GetComponentInChildren<Scrollbar>().size = healthSystem.GetHealthNormalized();
    }

    public void TakeDamage(int damage)
    {
        healthSystem.Damage(damage);
    }

    private void HealthSystem_OnDamage(object sender, EventArgs e)
    {
        canvas.GetComponentInChildren<Scrollbar>().size = healthSystem.GetHealthNormalized();
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        healthSystem.OnDamage -= HealthSystem_OnDamage;
        healthSystem.OnDead -= HealthSystem_OnDead;

        Destroy(this.gameObject);
    }

    void Update()
    {
        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (gridPosition != newGridPosition)
        {
            LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition);
            gridPosition = newGridPosition;
            LevelGrid.Instance.AddUnitAtGridPosition(this);
        }
    }

    //Selecciona la unidad
    public void SelectUnit()
    {
        animatior.SetBool("IsActive", true);
        selectedVisuals.SetActive(true);
    }

    //Deselecciona la unidad
    public void DeselectUnit()
    {
        animatior.SetBool("IsActive", false);
        selectedVisuals.SetActive(false);
    }

    //Comprueba si quedan puntos de accion disponibles
    public bool CanSpendPointsToTakeAction()
    {
        return actionPoints.Value > 0;
    }

    //Reestablece los puntos de accion al maximo por turno
    public void ResetActionPoints()
    {
        actionPoints.Value = maxPointsTurn;
    }

    //Resta los puntos a gastar a los actuales
    public void SpendActionPoints(int points)
    {
        actionPoints.Value -= points;
    }

    //Devuelbe la posicion en el grid de la unidad
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    //Devuelve la lista de las acciones disponobles para la unidad
    public BaseAction[] GetBaseActionArray()
    {
        return dispActions;
    }

    //Devielde los puntos de accion actuales de la unidad
    public int GetActionPoints()
    {
        return actionPoints.Value;
    }
}
