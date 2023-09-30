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
        Debug.Log("MoveForward");
    }
    public override void MoveBackward()
    {
        Vector3 direction = this.transform.forward * -1;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        Debug.Log("MoveBackward");
    }
    public override void RotateClockwise()
    {
        this.transform.Rotate(0.0f, turnSpeed * Time.deltaTime, 0.0f, Space.Self);
        Debug.Log("RotateClockwise");
    }
    public override void RotateCounterClockwise()
    {
        this.transform.Rotate(0.0f, -turnSpeed * Time.deltaTime, 0.0f, Space.Self);
        Debug.Log("RotateCounterClockwise");
    }
    public override void Shoot()
    {
        shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
    }
}
