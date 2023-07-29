using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Actor : MonoBehaviour
{
    public Animator baseAnim;
    public Rigidbody body;
    public SpriteRenderer baseSprite;
    public SpriteRenderer shadowSprite;

    public float speed = 2;
    protected Vector3 frontVector;
    public bool isGrounded;
    public bool isAlive = true;

    public float maxLife = 100.0f;
    public float currentLife = 100.0f;

    protected virtual void Start()
    {
        currentLife = maxLife;
        isAlive = true;
        baseAnim.SetBool("IsAlive", isAlive);
    }

    public virtual void Update()
    {
        Vector3 shadowSpritePosition = shadowSprite.transform.position;
        shadowSpritePosition.y = 0;
        shadowSprite.transform.position = shadowSpritePosition;
    }

    public virtual void Attack()
    {
        baseAnim.SetTrigger("Attack");
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Floor")
        {
            isGrounded = true;
            baseAnim.SetBool("IsGrounded", isGrounded);
            DidLand();
        }
    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Floor")
        {
            isGrounded = false;
            baseAnim.SetBool("IsGrounded", isGrounded);
        }
    }
    protected virtual void DidLand()
    {
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

    public virtual void DidHitObject(Collider collider, Vector3 hitPoint, Vector3 hitVector)
    {
        Actor actor = collider.GetComponent<Actor>();
        if (actor != null && actor.CanBeHit())
        {
            if (collider.attachedRigidbody != null)
            {
                HitActor(actor, hitPoint, hitVector);
            }
        }
    }

    public bool CanBeHit()
    {
        return isAlive;
    }

    protected virtual void HitActor(Actor actor, Vector3 hitPoint, Vector3 hitVector)
    {
        actor.TakeDamage(10, hitVector);
    }

    public virtual void TakeDamage(float value, Vector3 hitVector)
    {
        FlipSprite(hitVector.x > 0);
        currentLife -= value;

        if (isAlive && currentLife <= 0)
        {
            Die();
        }
        else
        {
            baseAnim.SetTrigger("IsHurt");
        }
    }

    protected virtual void Die()
    {
        isAlive = false;
        baseAnim.SetBool("IsAlive", isAlive);
        StartCoroutine(DeathFlicker());
    }
    
    protected virtual void SetOpacity(float value)
    {
        Color color = baseSprite.color;
        color.a = value;
        baseSprite.color = color;
    }
    
    private IEnumerator DeathFlicker()
    {
        int i = 5;
        while (i > 0)
        {
            SetOpacity(0.5f);
            yield return new WaitForSeconds(0.1f);
            SetOpacity(1.0f);
            yield return new WaitForSeconds(0.1f);
            i--;
        }
    }

    // AI pathfinding
    public virtual bool CanWalk()
    {
        return true;
    }

    public virtual void FaceTarget(Vector3 targetPoint)
    {
        FlipSprite(transform.position.x - targetPoint.x > 0);
    }
}
