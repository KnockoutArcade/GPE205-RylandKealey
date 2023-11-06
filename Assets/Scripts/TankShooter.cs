using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;
    public float shotsPerSecond;
    private float secondsPerShot;
    private float shotTimer;
    private bool canShoot = false;
    
    // Start is called before the first frame update
    public override void Start()
    {
        // Get the reciprical of shotsPerSecond to make it easier to work with
        secondsPerShot = 1 / shotsPerSecond;

        shotTimer = secondsPerShot;
    }

    // Update is called once per frame
    public override void Update()
    {
        // Subtract how much time has passed from the timer
        shotTimer -= Time.deltaTime;

        // if the timer is below 0
        if (shotTimer <= 0)
        {
            canShoot = true;
        }

        secondsPerShot = 1 / shotsPerSecond;
    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        if (canShoot)
        {
            // Instantiate the projectile
            GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation);

            #region Modify DamageOnHit Component
            // Get the DamageOnHit component
            DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

            // if it has one...
            if (doh != null)
            {
                // Set damageDone to the value passed in
                doh.damageDone = damageDone;

                // Set the owner to this pawn
                doh.owner = this.GetComponent<Pawn>();
            }
            #endregion

            #region Launch Rigidbody Forwards
            // Get the rigidbody
            Rigidbody rb = newShell.GetComponent<Rigidbody>();

            // if it has a rigidbody...
            if (rb != null)
            {
                rb.AddForce(firepointTransform.forward * fireForce);
            }
            #endregion

            // Destroy it after a set time
            Destroy(newShell, lifespan);

            // Reset the Timer
            shotTimer = secondsPerShot;

            // Reset CanShoot
            canShoot = false;
        }
    }
}
