using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTaker damageTaker = collision.gameObject.GetComponent<DamageTaker>();

        if (damageTaker)
        {
            damageTaker.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
