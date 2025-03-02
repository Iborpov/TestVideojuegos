using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitsControler : MonoBehaviour
{
    [SerializeField]
    LayerMask unitsLayer;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    Transform buttonsGrid;

    [SerializeField]
    GameObject buttonPrefab;

    Vector2 mousePosition;

    Unit selectedUnit = null;
    BaseAction selectedAction;

    List<GridPosition> validPositions;

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

            //Si el rayo colisiona con una unidad
            if (Physics.Raycast(ray, out hit, float.MaxValue, unitsLayer))
            {
                //Si ya habia una unidad seleccionada
                if (selectedUnit)
                {
                    selectedUnit.DeselectUnit();
                    foreach (Transform button in buttonsGrid)
                    {
                        GameObject.Destroy(button.gameObject);
                    }
                }

                selectedUnit = hit.collider.GetComponent<Unit>();
                selectedUnit.SelectUnit();

                //UI de aciones visible
                canvas.SetActive(true);
                //Action points text
                canvas
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .GetOrAddComponent<TextMeshProUGUI>()
                    .text = "AP: " + selectedUnit.GetActionPoints();
                //Action buttons
                UIActionButtons();
                return;
            }

            //Si hay una unidad seleccionada y se pulsa en el suelo
            if (selectedUnit && Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                var position = hit.point;
                GridPosition gridPos = LevelGrid.Instance.GetGridPosition(position);
                TakeAction(gridPos);
            }
        }
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
                        SetActiveAction(baseAction);
                    }
                );
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = baseActions[i]
                .GetActionName();
        }
    }

    private void SetActiveAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        validPositions = selectedAction.GetValidGridPositionList();
    }

    private void TakeAction(GridPosition gp)
    {
        Debug.Log(selectedUnit.CanSpendPointsToTakeAction());
        if (selectedUnit.CanSpendPointsToTakeAction())
        {
            Debug.Log(validPositions.Contains(gp));
            if (validPositions.Contains(gp))
            {
                //isBusy = true;
                selectedAction.TakeAction(gp, ClearBusy);
            }
        }
    }

    private void ClearBusy()
    {
        //isBusy = false;
    }
}
