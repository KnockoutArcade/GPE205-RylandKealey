using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    /// <summary>
    /// Variable for Movement Speed
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// Variable for the rotation speed of the pawn
    /// </summary>
    public float turnSpeed;
    public bool isMakingNoise;
    public PlayerController playerController;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        //moveSpeed = 0;
        //turnSpeed = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isMakingNoise)
        {
            MakeNoise(0);
        }
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void Shoot();
    public abstract void LayMine();
    public abstract void MakeNoise(float volume);

}
