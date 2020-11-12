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
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
        {
            health.Damage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
        {
            currentDamageFrame++;

            if (currentDamageFrame >= touchDamageFrame)
            {
                health.Damage(damage);
                currentDamageFrame = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentDamageFrame = 0;
    }
}
