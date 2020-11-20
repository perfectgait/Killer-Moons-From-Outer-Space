using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTaker damageTaker = collision.gameObject.GetComponent<DamageTaker>();

        if (damageTaker)
        {
            Debug.Log("Take Damage");
            damageTaker.TakeDamage(damage);
        }


        //Health health = collision.gameObject.GetComponent<Health>();

        //if (health)
        //{
        //    health.Damage(damage);
        //}

        Destroy(gameObject);
    }
}
