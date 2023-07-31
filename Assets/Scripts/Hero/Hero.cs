using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : Actor
{
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 5;

    public InputHandler input;

    // Walking and Running
    Vector3 currentDir;
    bool isFacingLeft;
    bool isRunning; 
    bool isMoving;
    float lastWalk;
    public bool canRun = true;
    float tapAgainToRunTime = 0.2f;
    Vector3 lastWalkVector;

    // Jumping
    bool isJumpLandAnim;
    bool isJumpingAnim;
    bool jump;    
    public float jumpForce = 1750;
    private float jumpDuration = 0.2f;
    private float lastJumpTime;

    // Attacking
    bool isAttackingAnim;
    float lastAttackTime;
    float attackLimit = 0.14f;

    public override void Update()
    {
        base.Update();
        if (!isAlive)
        {
            return;
        }

        isAttackingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("attack1");
        isJumpLandAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("jump_land");
        isJumpingAnim = baseAnim.GetCurrentAnimatorStateInfo(0).IsName("jump_rise") ||
        baseAnim.GetCurrentAnimatorStateInfo(0).IsName("jump_fall");

        float h = input.GetHorizontalAxis();
        float v = input.GetVerticalAxis();
        jump = input.GetJumpButtonDown();
        bool attack = input.GetAttackButtonDown();

        currentDir = new Vector3(h, 0, v);
        currentDir.Normalize();

        // Move
        if(!isAttackingAnim)
        {
            if ((v == 0 && h == 0))
            {
                Stop();
                isMoving = false;
            }
            else if (!isMoving && (v != 0 || h != 0))
            {
                isMoving = true;
                float dotProduct = Vector3.Dot(currentDir, lastWalkVector);
                if (canRun && (Time.time < lastWalk + tapAgainToRunTime) && dotProduct > 0)
                {
                    Run();
                }
                else
                {
                    Walk();

                    if (h != 0)
                    {
                        lastWalkVector = currentDir;
                        lastWalk = Time.time;
                    }
                }
            }
        }

        // Attack
        if (attack && Time.time >= lastAttackTime + attackLimit)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }
    void FixedUpdate()
    {
        Vector3 moveVector = currentDir * speed;
        if (isGrounded && !isAttackingAnim)
        {
            body.MovePosition(transform.position + moveVector *
            Time.fixedDeltaTime);
            baseAnim.SetFloat("Speed", moveVector.magnitude);
        }
        if (moveVector != Vector3.zero)
        {
            if (moveVector.x != 0)
            {
                isFacingLeft = moveVector.x < 0;
            }
            FlipSprite(isFacingLeft);
        }

        if (jump && !isJumpLandAnim && !isAttackingAnim && (isGrounded || (isJumpingAnim && Time.time < lastJumpTime + jumpDuration)))
        {
            jump = false;
            Jump(currentDir);
        }
    }
    
    public void Stop()
    {
        speed = 0;
        isRunning = false;
        baseAnim.SetBool("IsRunning", isRunning);
        baseAnim.SetFloat("Speed", speed);
    }
    
    public void Walk()
    {
        speed = walkSpeed;
        isRunning = false;
        baseAnim.SetBool("IsRunning", isRunning);
        baseAnim.SetFloat("Speed", speed);
    }

    public void Run()
    {
        speed = runSpeed;
        isRunning = true;
        baseAnim.SetBool("IsRunning", isRunning);
        baseAnim.SetFloat("Speed", speed);
    }

    void Jump(Vector3 direction)
    {
        if (!isJumpingAnim)
        {
            baseAnim.SetTrigger("Jump");
            lastJumpTime = Time.time;
            Vector3 horizontalVector = new Vector3(direction.x, 0, direction.z) * speed * 40;
            body.AddForce(horizontalVector, ForceMode.Force);
        }
        Vector3 verticalVector = Vector3.up * jumpForce * Time.deltaTime;
        body.AddForce(verticalVector, ForceMode.Force);
    }

    protected override void DidLand()
    {
        base.DidLand();
        Walk();
    }
    public override void Attack()
    {
        baseAnim.SetInteger("EvaluatedChain", 0);
        baseAnim.SetInteger("CurrentChain", 1);
    }
    public void DidChain(int chain)
    {
        baseAnim.SetInteger("EvaluatedChain", 1);
    }
}
