using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    GameObject selectedVisuals;
    public Animator animatior;

    [SerializeField]
    int maxPointsTurn = 2;

    int actionPoints;

    GridPosition gridPosition;

    BaseAction[] dispActions;

    void Awake()
    {
        selectedVisuals = transform.GetChild(1).gameObject;
        selectedVisuals.SetActive(false);
        animatior = transform.GetChild(0).GetComponent<Animator>();
        animatior.SetBool("IsActive", false);
        dispActions = GetComponents<BaseAction>();
        ResetActionPoints();
    }

    void Start() { }

    void Update() { }

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
        return actionPoints > 0;
    }

    //Reestablece los puntos de accion al maximo por turno
    public void ResetActionPoints()
    {
        actionPoints = maxPointsTurn;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return dispActions;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }
}
