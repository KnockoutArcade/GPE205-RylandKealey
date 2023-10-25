using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        // Apply health changes
        Health targetHealth = target.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        Debug.LogWarning("This health powerup is permanent");
    }
}
