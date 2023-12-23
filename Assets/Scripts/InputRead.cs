using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputRead : MonoBehaviour
{
    public Action<Vector3> mousePositionEvent;

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector3 positionOnWord = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        mousePositionEvent?.Equals(positionOnWord);
    }
}