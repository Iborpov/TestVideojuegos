using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitsControler : MonoBehaviour
{
    //Layers ----------------------------
    [SerializeField]
    LayerMask unitsLayer;

    [SerializeField]
    LayerMask groundLayer;

    //InGame Objects ----------------------------

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    Transform buttonsGrid;

    [SerializeField]
    Transform floorVisuals;

    //Prefabs ----------------------------
    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    GameObject positionVPrefab;

    [SerializeField]
    GameObject attackVPrefab;

    //Variables ----------------------------
    Vector2 mousePosition;

    Unit selectedUnit = null;
    BaseAction selectedAction;

    List<GridPosition> validPositions;
    GameObject uiActions;

    private void Awake()
    {
        uiActions = canvas.transform.GetChild(0).gameObject;
    }

    //Captura el movimiento del raton
    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //Si se pulsa en UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (context.canceled)
        {
            //Raycas desde la camara segun la posición del raton
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            //Si hay una unidad seleccionada y se pulsa en el suelo
            if (selectedUnit && Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                var position = hit.point;
                GridPosition gridPos = LevelGrid.Instance.GetGridPosition(position);
                Debug.Log("Clicked position: " + gridPos);
                if (selectedAction)
                {
                    TakeAction(gridPos);
                }
            }

            //Si el rayo colisiona con una unidad
            if (Physics.Raycast(ray, out hit, float.MaxValue, unitsLayer))
            {
                SelectUnit(hit);

                //UI de aciones de la unidad visible
                uiActions.SetActive(true);
                //Action points text de la unidad
                uiActions.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "AP: " + selectedUnit.GetActionPoints();
                //Action buttons de la unidad
                UIActionButtons();

                return;
            }
        }
    }

    bool SelectUnit(RaycastHit hit)
    {
        //Si ya hay una unidad
        if (selectedUnit)
        {
            //Si ya habia una unidad seleccionada diferente
            if (selectedUnit != hit.collider.GetComponent<Unit>())
            {
                //Desseleciona la activa
                selectedUnit.DeselectUnit();

                //Selecciona la nueva unidad
                selectedUnit = hit.collider.GetComponent<Unit>();
                selectedUnit.SelectUnit();
            }

            //Borra las acciónes de esta unidad para evitar que se añadan de más
            foreach (Transform button in buttonsGrid)
            {
                Destroy(button.gameObject);
            }
        }
        else
        {
            //Selecciona la unidad
            selectedUnit = hit.collider.GetComponent<Unit>();
            selectedUnit.SelectUnit();
        }
        return false;
    }

    private void UIActionButtons()
    {
        BaseAction[] baseActions = selectedUnit.GetBaseActionArray();
        for (int i = 0; i < baseActions.Length; i++)
        {
            BaseAction baseAction = baseActions[i];
            GameObject button = Instantiate(buttonPrefab, buttonsGrid);
            button
                .GetComponent<Button>()
                .onClick.AddListener(
                    delegate
                    {
                        //Destruye los quad de acciones que existan
                        foreach (Transform fv in floorVisuals)
                        {
                            Destroy(fv.gameObject);
                        }

                        //Pone el color de todos los botones a negro
                        foreach (Transform button in buttonsGrid)
                        {
                            button.GetComponent<Image>().color = Color.black;
                        }

                        //El seleccionado se cambia a gris
                        button.GetComponent<Image>().color = Color.gray;

                        //Establece que función realiza
                        SetActiveAction(baseAction);
                    }
                );
            //Cambia el nombre de accion que aparece en el botón
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = baseActions[i]
                .GetActionName();
            //Cambia los puntos de accion requeridos que aparece en el botón
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                baseActions[i].GetActionPointsCost() + " AP";
        }
    }

    private void SetActiveAction(BaseAction baseAction)
    {
        //Establece la acción seleccionada
        selectedAction = baseAction;
        //Recoge todas las posiciones validas de esta acción
        validPositions = selectedAction.GetValidGridPositionList();

        //Crea todos los quads necesarios para indicar cuales son las posiciones validas
        foreach (GridPosition vp in validPositions)
        {
            Debug.Log(vp);
            GameObject posV = Instantiate(positionVPrefab, floorVisuals);
            posV.transform.position = LevelGrid.Instance.GetWorldPosition(vp);
        }
    }

    private void TakeAction(GridPosition gp)
    {
        Debug.Log(selectedAction);
        Debug.Log(selectedUnit.CanSpendPointsToTakeAction());

        //Comprueba si la unidad tiene puntos como para tomar la acción
        if (selectedUnit.CanSpendPointsToTakeAction())
        {
            Debug.Log(validPositions.Contains(gp));
            //Comprueva si la posicion selecionada es una valida dentro de la acción
            if (selectedAction.IsValidActionGridPosition(gp))
            {
                //Realiza la acción
                //isBusy = true;
                selectedAction.TakeAction(gp, ClearBusy);
                selectedUnit.SpendActionPoints(selectedAction.GetActionPointsCost());
                //Borra todos los quads visuales de la acción realizada
                foreach (Transform fv in floorVisuals)
                {
                    Destroy(fv.gameObject);
                }
            }
        }
    }

    private void ClearBusy()
    {
        //isBusy = false;
    }
}
