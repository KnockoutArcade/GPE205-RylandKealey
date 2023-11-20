using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePowerup : Powerup
{
    public int scoreToAdd;

    public override void Apply(PowerupManager target)
    {
        // Find the pawn component of the target
        Pawn targetpawn = target.GetComponent<Pawn>();
        PlayerController controller = targetpawn.playerController;

        if (controller != null)
        {
            controller.AddToScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        Debug.LogWarning("This mine powerup is permanent");
    }
}
