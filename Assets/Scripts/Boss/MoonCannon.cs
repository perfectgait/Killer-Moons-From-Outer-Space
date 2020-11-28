using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCannon : MonoBehaviour
{
    [SerializeField] FiringIndicator firingIndicator;

    private BulletEmitter bulletEmitter;

    // Start is called before the first frame update
    void Start()
    {
        bulletEmitter = GetComponent<BulletEmitter>();
    }

    public void StartFiring()
    {
        if (!bulletEmitter)
        {
            return;
        }

        StartCoroutine(Fire());
    }

    public void StopFiring()
    {
        if (firingIndicator && firingIndicator.IsIndicating())
        {
            firingIndicator.StopIndicator();
        }

        StopAllCoroutines();
    }

    private IEnumerator Fire()
    {
        if (firingIndicator)
        {
            firingIndicator.StartIndicator();

            while (firingIndicator.IsIndicating())
            {
                yield return null;
            }
        }

        StartCoroutine(bulletEmitter.Emit());
    }
}
