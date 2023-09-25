using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;
    
    public void OnTriggerEnter(Collider other)
    {
        // If the object this projectile hit has the health component
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // Damage it if it has a Health Component
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy the projectile when it hits anything
        Destroy(gameObject);
    }
}
