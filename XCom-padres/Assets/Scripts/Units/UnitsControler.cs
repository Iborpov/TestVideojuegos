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

    public void OnClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (context.canceled)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, unitsLayer))
            {
                if (selectedUnit)
                {
                    selectedUnit.DeselectUnit();
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
            if (selectedUnit && Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
            {
                var position = hit.point;
                GridPosition gridPos = LevelGrid.Instance.GetGridPosition(position);
                TakeAction(gridPos);
            }
        }
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
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
        if (selectedUnit.CanSpendPointsToTakeAction())
        {
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
