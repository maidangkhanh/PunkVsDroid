using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    
    public Animator baseAnim;
    public Rigidbody body;
    public SpriteRenderer shadowSprite;
    
    [SerializeField] float speed = 2;
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 5;

    // Walking and Running
    Vector3 currentDir;
    bool isFacingLeft;
    protected Vector3 frontVector;
    bool isRunning; 
    bool isMoving;
    float lastWalk;
    public bool canRun = true;
    float tapAgainToRunTime = 0.2f;
    Vector3 lastWalkVector;

    void FixedUpdate()
    {
        Vector3 moveVector = currentDir * speed;
        body.MovePosition(transform.position + moveVector *
        Time.fixedDeltaTime);
        baseAnim.SetFloat("Speed", moveVector.magnitude);
        
        if (moveVector != Vector3.zero)
        {
            if (moveVector.x != 0)
            {
                isFacingLeft = moveVector.x < 0;
            }
            FlipSprite(isFacingLeft);
        }
    }
    
    public void FlipSprite(bool isFacingLeft)
    {
        if (isFacingLeft)
        {
            frontVector = new Vector3(-1, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            frontVector = new Vector3(1, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnMove(InputValue value)
    {
        float h = value.Get<Vector2>().x;
        float v = value.Get<Vector2>().y;

        currentDir = new Vector3(h, 0, v);
        currentDir.Normalize();

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
}
