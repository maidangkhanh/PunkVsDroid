using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Actor))]
public class Walker : MonoBehaviour
{
   
    public NavMeshAgent navMeshAgent;
    private NavMeshPath navPath;
    private List<Vector3> corners;

    float currentSpeed;
    float speed;

    private Actor actor;
    private System.Action didFinishWalk;

    void Start()
    {
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        actor = GetComponent<Actor>();
    }

    public bool MoveTo(Vector3 targetPosition, System.Action callback = null)
    {
        navMeshAgent.Warp(transform.position);
        didFinishWalk = callback;
        speed = actor.speed;
        navPath = new NavMeshPath();
        bool pathFound = navMeshAgent.CalculatePath(targetPosition, navPath);
        if (pathFound)
        {
            corners = navPath.corners.ToList();
            return true;
        }
        return false;
    }
    public void StopMovement()
    {
        navPath = null;
        corners = null;
        currentSpeed = 0;
    }
    protected void FixedUpdate()
    {
        bool canWalk = actor.CanWalk();
        if (canWalk && corners != null && corners.Count > 0)
        {
            currentSpeed = speed;
            actor.body.MovePosition(Vector3.MoveTowards(transform.position, corners[0], Time.fixedDeltaTime * speed));
            
            if (Vector3.SqrMagnitude(
            transform.position - corners[0]) < 0.6f)
            {
                corners.RemoveAt(0);
            }
            if (corners.Count > 0)
            {
                currentSpeed = speed;
                
                Vector3 direction = transform.position - corners[0];
                actor.FlipSprite(direction.x >= 0);
            }
            else
            {                
                currentSpeed = 0.0f;
                if (didFinishWalk != null)
                {
                    didFinishWalk.Invoke();
                    didFinishWalk = null;
                }
            }
        }
        actor.baseAnim.SetFloat("Speed", currentSpeed);
    }
}