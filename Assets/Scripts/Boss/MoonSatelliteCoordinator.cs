using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSatelliteCoordinator : MonoBehaviour
{
    [SerializeField] MoonSatellite[] moonSatellites;
    [SerializeField] float delayBetweenAttacks = 20.0f;

    public void DisableSatellites()
    {
        foreach (MoonSatellite moonSatellite in moonSatellites)
        {
            if (moonSatellite)
            {
                moonSatellite.GetComponent<SpriteRenderer>().enabled = false;
                moonSatellite.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void EnableSatellites()
    {
        foreach (MoonSatellite moonSatellite in moonSatellites)
        {
            if (moonSatellite)
            {
                moonSatellite.GetComponent<SpriteRenderer>().enabled = true;
                moonSatellite.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void InitializeSatellites()
    {
        foreach (MoonSatellite moonSatellite in moonSatellites)
        {
            if (moonSatellite)
            {
                moonSatellite.Initialize();
            }
        }
    }

    public void Attack()
    {
        //while (true)
        //{
        foreach (MoonSatellite moonSatellite in moonSatellites)
        {
            if (moonSatellite)
            {
                StartCoroutine(moonSatellite.Attack());
            }
        }

        //    yield return new WaitForSeconds(delayBetweenAttacks);
        //}
    }
}
