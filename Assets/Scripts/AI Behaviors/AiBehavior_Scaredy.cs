using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior_Scaredy : AIController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                // Do work
                DoPatrolState();
                //Check for transitions
                
                // Check if we have a target
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }

                if (IsDistanceLessThan(target, 5) || CanHear(target))
                {
                    ChangeState(AIState.Flee);
                }
                break;

            case AIState.Flee:
                DoFleeState();

                // Check if we have a target
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }
                if (!IsDistanceLessThan(target, fleeDistance))
                {
                    ChangeState(AIState.Patrol);
                }
                break;

            case AIState.ChooseTarget:
                TargetPlayerOne();

                ChangeState(AIState.Patrol);
                break;
        }
    }

    public override void DoAttackState()
    {
        // Chase
        Seek(target);
        // Shoot
        Shoot();
    }
}
