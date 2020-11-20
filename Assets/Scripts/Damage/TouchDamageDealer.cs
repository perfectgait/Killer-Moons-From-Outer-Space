using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamageDealer : MonoBehaviour
{
    [SerializeField] float damage;
    [Tooltip("The frame that the touch damage is applied")]
    [SerializeField] int touchDamageFrame;

    private int currentDamageFrame = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTaker damageTaker = collision.gameObject.GetComponent<DamageTaker>();

        if (damageTaker)
        {
            Debug.Log("Take Touch Damage");
            damageTaker.TakeDamage(damage);
        }


        //Health health = collision.gameObject.GetComponent<Health>();

        //if (health)
        //{
        //    health.Damage(damage);
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageTaker damageTaker = collision.gameObject.GetComponent<DamageTaker>();

        if (damageTaker)
        {
            currentDamageFrame++;

            if (currentDamageFrame >= touchDamageFrame)
            {
                damageTaker.TakeDamage(damage);
                currentDamageFrame = 0;
            }
        }


        //Health health = collision.gameObject.GetComponent<Health>();

        //if (health)
        //{
        //    currentDamageFrame++;

        //    if (currentDamageFrame >= touchDamageFrame)
        //    {
        //        health.Damage(damage);
        //        currentDamageFrame = 0;
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentDamageFrame = 0;
    }
}
