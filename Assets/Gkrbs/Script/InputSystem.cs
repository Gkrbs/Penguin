using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Player Component
public class InputSystem : MonoBehaviour
{
    [Header("Character Input Value")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool run;
    public bool rightClick;
    public bool leftClick;
    public bool plusButton;
    public bool minusButton;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    // Keyboard Input System
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }
    public void OnRun(InputValue value)
    {
        run = value.isPressed;
    }
    public void OnPlus(InputValue value)
    {
        plusButton = value.isPressed;
    }
    public void OnMinus(InputValue value)
    {
        minusButton = value.isPressed;
    }

    // Mouse Input System
    public void OnMouse(InputValue value)
    {
        look = value.Get<Vector2>();
    }
    public void OnRightClick(InputValue value)
    {
        rightClick = value.isPressed;
    }
    public void OnLeftClick(InputValue value)
    {
        leftClick = value.isPressed;
    }
}
