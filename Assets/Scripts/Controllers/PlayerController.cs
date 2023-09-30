using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // If manager exists
        {
            if (GameManager.instance != null)
            {
                // And if it can track players
                if (GameManager.instance.players != null)
                {
                    // Register it to the GameManager
                    GameManager.instance.players.Add(this);
                }
            }
        }
    }

    public void OnDestroy()
    {
        // If manager exists
        {
            if (GameManager.instance != null)
            {
                // And if it can track players
                if (GameManager.instance.players != null)
                {
                    // Remove it from the GameManager
                    GameManager.instance.players.Remove(this);
                }
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        ProcessInputs();
    }

    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }
}
