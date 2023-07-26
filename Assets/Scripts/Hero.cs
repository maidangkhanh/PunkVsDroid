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
    
    Vector3 currentDir;
    bool isFacingLeft;
    protected Vector3 frontVector;

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
        }
        else if ((v != 0 || h != 0))
        {
            Walk();
        }
    }

    public void Stop()
    {
        speed = 0;
        baseAnim.SetFloat("Speed", speed);
    }
    
    public void Walk()
    {
        speed = walkSpeed;
        baseAnim.SetFloat("Speed", speed);
    }
}
