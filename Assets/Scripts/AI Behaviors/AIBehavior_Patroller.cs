using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior_Patroller : AIController
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

                if (IsDistanceLessThan(target, 3) || CanHear(target))
                {
                    ChangeState(AIState.Attack);
                }
                break;

            case AIState.Attack:
                // Do Work
                DoAttackState();
                //Check for transitions
                // Check if we have a target
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }

                if (!IsDistanceLessThan(target, 3) && !CanHear(target))
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
}
