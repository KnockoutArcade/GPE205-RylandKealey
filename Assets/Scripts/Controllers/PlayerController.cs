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
    public KeyCode layMineKey;
    
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


    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        ProcessInputs();

        if (pawn == null)
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

            Destroy(this.gameObject);
        }
    }

    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
            pawn.isMakingNoise = true;
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
            pawn.isMakingNoise = true;
        }
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
            pawn.isMakingNoise = true;
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
            pawn.isMakingNoise = true;
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
            pawn.isMakingNoise = true;
        }
        if (Input.GetKeyDown(layMineKey))
        {
            pawn.LayMine();
            pawn.isMakingNoise = true;
        }

        // If we are not inputting anything, don't make noise
        if (!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey) && !Input.GetKey(rotateClockwiseKey) && !Input.GetKey(rotateCounterClockwiseKey) && !Input.GetKeyDown(shootKey) && !Input.GetKeyDown(layMineKey))
        {
            pawn.isMakingNoise = false;
        }
    }
}
