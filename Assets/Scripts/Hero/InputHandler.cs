using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // TODO: research input system input
    // input.Player.AttackOnRelease.performed += AttackOnRelease;
    // input.Player.AttackOnHold.performed += AttackOnHold;
    // input.Player.AttackOnDoubleTap.performed += AttackOnDoubleTap;
    // input.Player.AttackOnRelease.Enable();
    // input.Player.AttackOnHold.Enable();
    // input.Player.AttackOnDoubleTap.Enable();
    // input.Player.Dash.Enable();
    float horizontal;
    float vertical;
    bool jump;
    bool isJumping;
    public float maxJumpDuration = 0.2f;

    PunkVsDroid inputActions;

    private void Awake()
    {
        inputActions = new PunkVsDroid();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_canceled;
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Jump.canceled += Jump_canceled;

    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        horizontal = 0;
        vertical = 0;
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        jump = false;
        isJumping = false;
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (!jump && !isJumping)
        {
            jump = true;
            isJumping = true;
        }
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public float GetVerticalAxis()
    {
        return vertical;
    }
    public float GetHorizontalAxis()
    {
        return horizontal;
    }
    public bool GetJumpButtonDown()
    {
        return jump;
    }
 }
