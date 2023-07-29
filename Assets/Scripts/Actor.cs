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

    //1
    public virtual void DidHitObject(Collider collider, Vector3 hitPoint, Vector3 hitVector)
    {
        Actor actor = collider.GetComponent<Actor>();
        if (actor != null)
        {
            if (collider.attachedRigidbody != null)
            {
                HitActor(actor, hitPoint, hitVector);
            }
        }
    }
    //2
    protected virtual void HitActor(Actor actor, Vector3 hitPoint, Vector3 hitVector)
    {
        Debug.Log(gameObject.name + " HIT " + actor.gameObject.name);
    }
}
