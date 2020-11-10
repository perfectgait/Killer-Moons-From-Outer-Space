using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamageDealer : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] int iframes;

    private int iframesUsed = 0;

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
            iframesUsed++;

            if (iframesUsed >= iframes)
            {
                health.Damage(damage);
                iframesUsed = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        iframesUsed = 0;
    }
}
