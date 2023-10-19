using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior_Turret : AIController
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
            case AIState.Gaurd:
                // Do work
                DoGaurdState();
                //Check for transitions
                
                // Check if we have a target
                if (!IsHasTarget())
                {
                    ChangeState(AIState.ChooseTarget);
                }

                if (CanSee(target))
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

                if (!CanSee(target))
                {
                    ChangeState(AIState.Gaurd);
                }
                break;

            case AIState.ChooseTarget:
                TargetPlayerOne();

                ChangeState(AIState.Gaurd);
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
