using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    #region Variables
    public enum AIState { Gaurd, Chase, Flee, Patrol, Attack, BackToPost };

    /// <summary>
    /// The current state of this FSM
    /// </summary>
    public AIState currentState;

    private float lastStateChangeTime;

    public GameObject target;

    public float fleeDistance;
    #endregion

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        MakeDecisions();
    }

    /// <summary>
    /// Autonomously make decisions about what to do in the current state
    /// </summary>
    protected void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Gaurd:
                // Do work
                DoIdleState();
                //Check for transitions
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Flee);
                }
                break;

            case AIState.Attack:
                // Do Work
                DoAttackState();
                //Check for transitions
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Gaurd);
                }
                break;

            case AIState.Flee:
                DoFleeState();

                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Attack);
                }
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        // Change the current state
        currentState = newState;

        // Log the time that this change happened
        lastStateChangeTime = Time.time;
    }

    public void DoSeekState()
    {
        Seek(target);
    }

    public virtual void DoIdleState()
    {
        // Do Nothing
    }

    public virtual void DoAttackState()
    {
        // Chase
        Seek(target);
        // Shoot
        Shoot();
    }

    public virtual void DoFleeState()
    {
        Flee();
    }

    public void Seek(GameObject target)
    {
        // Rotate Towards target
        pawn.RotateTowards(target.transform.position);

        // Move forward
        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        // Rotate Towards target
        pawn.RotateTowards(targetTransform.position);

        pawn.MoveForward();
    }

    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.gameObject);
    }

    public void Seek(Vector3 targetPosition)
    {
        // Rotate Towards target
        pawn.RotateTowards(targetPosition);

        pawn.MoveForward();
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }   

    public void Shoot()
    {
        // Tell the pawn to shoot
        pawn.Shoot();
    }

    protected void Flee()
    {
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        
        // Find vector to the target position
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Point target vector in the opposite direction
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * (fleeDistance * flippedPercentOfFleeDistance);
        // Seek the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector);
    }
}
