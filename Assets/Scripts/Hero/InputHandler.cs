using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    float horizontal;
    float vertical;
    bool jump;
    bool isJumping;
    bool attack;
    public float maxJumpDuration = 0.2f;

    PunkVsDroid inputActions;

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
    public bool GetAttackButtonDown()
    {
        return attack;
    }

    private void Awake()
    {
        inputActions = new PunkVsDroid();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_canceled;
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Jump.canceled += Jump_canceled;
        inputActions.Player.Attack.performed += Attack_performed;
        inputActions.Player.Attack.canceled += Attack_canceled;
    }

    private void Attack_canceled(InputAction.CallbackContext context)
    {
        attack = false;
    }

    private void Attack_performed(InputAction.CallbackContext context)
    {
        attack = true;
    }

    private void Move_canceled(InputAction.CallbackContext context)
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


 }
