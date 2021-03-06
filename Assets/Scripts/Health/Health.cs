using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health;

    public void Damage(float amount)
    {
        health -= amount;

        // Ensure health doesn't drop below zero
        health = Mathf.Max(0, health);
    }

    public float GetHealth()
    {
        return health;
    }
}
