using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float _currentHealth;
    public float maxHealth;
    public Image healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Subract incoming damage from health
    /// </summary>
    /// <param name="amount">The amount of damage recieved</param>
    /// <param name="source">The player that caused this damage</param>
    public void TakeDamage(float amount, Pawn source)
    {
        _currentHealth = _currentHealth - amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);

        // Change our health bar
        if (healthBar != null)
        {
            healthBar.fillAmount = _currentHealth / maxHealth;
        }

        if (_currentHealth <= 0)
        {
            Die(source);
        }
    }

    public void Heal(float amount, Pawn source)
    {
        _currentHealth = _currentHealth + amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        // Change our health bar
        if (healthBar != null)
        {
            healthBar.fillAmount = _currentHealth / maxHealth;
        }
    }

    public void Die(Pawn source)
    {
        Debug.Log(source.name + "killed " + gameObject.name);

        // Get the player controller of the object that killed this
        PlayerController controller = source.playerController;
        // If the controller is found, add 500 points to the score
        if (controller != null)
        {
            controller.AddToScore(500);
        }

        Destroy(gameObject);
    }
}
