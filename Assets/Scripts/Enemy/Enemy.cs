using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Enemy : Actor
{
    public static int TotalEnemies;
    public Walker walker;
    public bool stopMovementWhenHit = true;
    public EnemyAI ai;
    public void RegisterEnemy()
    {
        TotalEnemies++;
    }
    protected override void Die()
    {
        base.Die();
        walker.enabled = false;
        TotalEnemies--;
        ai.enabled = false;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        walker.MoveTo(targetPosition);
    }
    public void MoveToOffset(Vector3 targetPosition, Vector3 offset)
    {
        if (!walker.MoveTo(targetPosition + offset))
        {
            walker.MoveTo(targetPosition - offset);
        }
    }
    public void Wait()
    {
        walker.StopMovement();
    }
    public override void TakeDamage(float value, Vector3 hitVector)
    {
        if (stopMovementWhenHit)
        {
            walker.StopMovement();
        }
        base.TakeDamage(value, hitVector);
    }
    public override bool CanWalk()
    {
        return
        !baseAnim.GetCurrentAnimatorStateInfo(0).IsName("hurt");
    }
}