using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;
    protected TankShooter shooter;
    private Rigidbody rb;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shooter = GetComponent<TankShooter>();

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveForward()
    {
        Vector3 direction = this.transform.forward;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        //Debug.Log("MoveForward");
        MakeNoise(5);
    }
    public override void MoveBackward()
    {
        Vector3 direction = this.transform.forward * -1;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        //Debug.Log("MoveBackward");
        MakeNoise(5);
    }
    public override void RotateClockwise()
    {
        this.transform.Rotate(0.0f, turnSpeed * Time.deltaTime, 0.0f, Space.Self);
        //Debug.Log("RotateClockwise");
        MakeNoise(5);
    }
    public override void RotateCounterClockwise()
    {
        this.transform.Rotate(0.0f, -turnSpeed * Time.deltaTime, 0.0f, Space.Self);
        //Debug.Log("RotateCounterClockwise");
        MakeNoise(5);
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
        // Find the vector to our target
        Vector3 vectorToTarget = targetPosition - transform.position;

        // Find the rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        // Rotate closer to that vector, but don't rotate more than our turn speed allows in one frame
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Make some noise
        MakeNoise(5);
    }
    public override void Shoot()
    {
        shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);

        // Make some noise
        MakeNoise(10);
    }
    public override void MakeNoise(float volume)
    {
        NoiseMaker noiseMaker = GetComponent<NoiseMaker>();

        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = volume;
            //Debug.Log(noiseMaker.volumeDistance);
        }
        else
        {
            //Debug.Log("No noise maker on object");
        }
    }
}
