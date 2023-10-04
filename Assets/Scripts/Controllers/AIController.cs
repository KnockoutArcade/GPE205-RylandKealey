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
        Debug.Log("making decisions");
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
}
