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
    BaseAction selectedAction = null;

    List<GridPosition> validPositions;
    GameObject uiActions;

    bool isBusy =false;

    private void Awake()
    {
        uiActions = canvas.transform.GetChild(1).gameObject;
    }

    //Captura el movimiento del raton
    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (isBusy)
        {
            return;
        }
        
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

            //Si el rayo colisiona con una unidad
            if (Physics.Raycast(ray, out hit, float.MaxValue, unitsLayer))
            {
                if (SelectUnit(hit))
                {
                    if (selectedUnit)
                    {
                        //Desseleciona la activa
                        selectedUnit.DeselectUnit();
                        selectedAction = null;
                        DestroyQuads();
                    }
                    //Selecciona la nueva unidad
                    selectedUnit = hit.collider.GetComponent<Unit>();
                    selectedUnit.SelectUnit();

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

            //Si hay una unidad seleccionada y se pulsa en el suelo
            if (selectedAction && Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                var position = hit.point;
                GridPosition gridPos = LevelGrid.Instance.GetGridPosition(position);
                Debug.Log("Clicked position: " + gridPos);

                TakeAction(gridPos);
                //Action points text de la unidad
                    uiActions.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        "AP: " + selectedUnit.GetActionPoints();
                //Action buttons de la unidad
                UIActionButtons();
            }
        }
    }

    bool SelectUnit(RaycastHit hit)
    {
        //Si hay una acción seleccionada
        if (selectedAction)
        {
            //Si ya habia una unidad seleccionada y es diferente a la nueva seleccionada
            if (selectedUnit != hit.collider.GetComponent<Unit>())
            {
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    private void UIActionButtons()
    {
        //Borra las acciónes de esta unidad para evitar que se añadan de más
        foreach (Transform button in buttonsGrid)
        {
            Destroy(button.gameObject);
        }

        BaseAction[] baseActions = selectedUnit.GetBaseActionArray();
        for (int i = 0; i < baseActions.Length; i++)
        {
            BaseAction baseAction = baseActions[i];
            GameObject button = Instantiate(buttonPrefab, buttonsGrid);

            //Cambia el nombre de accion que aparece en el botón
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = baseActions[i]
                .GetActionName();
            //Cambia los puntos de accion requeridos que aparece en el botón
            button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                baseActions[i].GetActionPointsCost() + " AP";
            //Si la unidad tiene los puntos que cuesta realizar la acción
            if (baseAction.GetActionPointsCost() <= selectedUnit.GetActionPoints())
            {
                button
                    .GetComponent<Button>()
                    .onClick.AddListener(
                        delegate
                        {
                            DestroyQuads();

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
            }
            else
            {
                //Los no disponibles por puntos se cambian a rojo
                button.GetComponent<Image>().color = Color.red;
            }
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
            Debug.Log(
                "GridPosition: "
                    + vp
                    + " Global position: "
                    + LevelGrid.Instance.GetWorldPosition(vp)
            );
            GameObject posV = Instantiate(positionVPrefab, floorVisuals);
            posV.transform.position = LevelGrid.Instance.GetWorldPosition(vp);
        }
    }

    private void TakeAction(GridPosition gp)
    {
        Debug.Log(selectedAction);

        //Comprueba si la unidad tiene puntos como para tomar la acción
        if (selectedUnit.CanSpendPointsToTakeAction())
        {
            //Comprueva si la posicion selecionada es una valida dentro de la acción
            if (selectedAction.IsValidActionGridPosition(gp))
            {
                //Realiza la acción
                isBusy = true;
                selectedAction.TakeAction(gp, ClearBusy);
                selectedUnit.SpendActionPoints(selectedAction.GetActionPointsCost());
                //Borra todos los quads visuales de la acción realizada
                foreach (Transform fv in floorVisuals)
                {
                    Destroy(fv.gameObject);
                }
                selectedAction = null;
            }
        }
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private void DestroyQuads()
    {
        //Destruye los quad de acciones que existan
        foreach (Transform fv in floorVisuals)
        {
            Destroy(fv.gameObject);
        }
    }
}
