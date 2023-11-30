using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class MineBehavior : MonoBehaviour
{
    /// <summary>
    /// The amount of time it takes for this mine to arm itself
    /// </summary>
    public float armingTime;

    /// <summary>
    /// Whether or not this mine is capable of exploding
    /// </summary>
    public bool isAllowedToExplode = false;

    /// <summary>
    /// The object that owns this mine
    /// </summary>
    public Pawn owner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease the arming time by the amount of time that has passed
        armingTime -= Time.deltaTime;

        // If the arming time has run out, allow this object to explode
        if (armingTime <= 0)
        {
            isAllowedToExplode = true;
        }

        // If this mine's owner is gone, destroy it
        if (owner == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we are touching something and we are allowed to explode...
        if (isAllowedToExplode)
        {
            // If the object this mine is colliding with has a health component
            Health otherHealth = other.gameObject.GetComponent<Health>();

            // Damage it if it has a Health Component
            if (otherHealth != null)
            {
                otherHealth.TakeDamage(100.0f, owner);
            }

            // Destroy the projectile when it hits anything
            Destroy(gameObject);
        }
    }
}
