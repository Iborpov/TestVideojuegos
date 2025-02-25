using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitsControler : MonoBehaviour
{
    Vector2 mousePosition;

    public void OnClick(InputAction.CallbackContext context) { }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        if (mousePosition != Vector2.zero)
        {
            mousePosition.Normalize();
        }
        Debug.Log(mousePosition);
    }
}
