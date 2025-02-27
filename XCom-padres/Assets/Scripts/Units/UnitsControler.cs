using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitsControler : MonoBehaviour
{
    [SerializeField]
    LayerMask unitsLayer;
    Vector2 mousePosition;

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, unitsLayer))
                {
                    Unit unit = hit.collider.GetComponent<Unit>();
                    unit.SelectUnit();
                }
            }
        }
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
