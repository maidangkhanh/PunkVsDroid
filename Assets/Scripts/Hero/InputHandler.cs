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
    float lastJumpTime;
    bool isJumping;
    public float maxJumpDuration = 0.2f;

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

    void OnMove(InputValue value)
    {
        horizontal = value.Get<Vector2>().x;
        vertical = value.Get<Vector2>().y;
    }

    void OnJump(InputValue value)
    {
        if (!jump && !isJumping)
        jump = true;
        lastJumpTime = Time.time;
        isJumping = true;

        if (jump && Time.time > lastJumpTime + maxJumpDuration)
        {
            jump = false;
        }
    }

    void OnJumpCanceled(InputValue value)
    {
        jump = false;
        isJumping = false;
    }
}
