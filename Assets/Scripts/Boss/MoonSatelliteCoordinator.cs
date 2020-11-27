using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSatelliteCoordinator : MonoBehaviour
{
    [SerializeField] MoonSatellite[] moonSatellites;
    [SerializeField] float delayBetweenAttacks = 20.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public IEnumerator Attack()
    {
        while (true)
        {
            foreach (MoonSatellite moonSatellite in moonSatellites)
            {
                if (moonSatellite)
                {
                    StartCoroutine(moonSatellite.Attack());
                }
            }

            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
}
