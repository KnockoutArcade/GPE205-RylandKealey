using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehavior_Rambo : AIController
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

        //Debug.Log(GameObject.Find(target.gameObject.name) != null);
    }

    protected override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Attack:
                TargetNearestTank();
                // Do Work
                DoAttackState();
                //Check for transitions
                
                break;

            case AIState.ChooseTarget:
                TargetNearestTank();

                ChangeState(AIState.Attack);
                break;
        }
    }
}
