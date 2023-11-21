using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    // Controls
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;
    public KeyCode layMineKey;

    // Score
    public int score;

    // Lives
    public int lives = 3;

    // Whether or not the pawn this controls is currently alive
    public bool pawnIsAlive = true;
    
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

        // Check to see if the pawn we're controlling is still alive
        if (pawn == null)
        {
            pawnIsAlive = false;
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

    public void AddToScore(int amount)
    {
        score += amount;

        // If manager exists
        {
            if (GameManager.instance != null)
            {
                // Update the score in the UI
                GameManager.instance.UpdateScore();
            }
        }
    }
}
