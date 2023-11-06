using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireratePowerup : Powerup
{
    public float fireRateIncrease;

    public override void Apply(PowerupManager target)
    {
        // Increase the firing rate of the player
        TankShooter targetShooter = target.GetComponent<TankShooter>();

        if (targetShooter != null)
        {
            targetShooter.shotsPerSecond += fireRateIncrease;
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankShooter targetShooter = target.GetComponent<TankShooter>();

        targetShooter.shotsPerSecond -= fireRateIncrease;
    }
}
