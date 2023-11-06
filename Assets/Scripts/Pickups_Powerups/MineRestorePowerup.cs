using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MineRestorePowerup : Powerup
{
    public float minesToAdd;

    public override void Apply(PowerupManager target)
    {
        // Increase the number of mines a player has
        MineDeployer targetMineDeployer = target.GetComponent<MineDeployer>();

        if (targetMineDeployer != null)
        {
            targetMineDeployer.numberOfMines += minesToAdd;
        }
    }

    public override void Remove(PowerupManager target)
    {
        Debug.LogWarning("This mine powerup is permanent");
    }
}
