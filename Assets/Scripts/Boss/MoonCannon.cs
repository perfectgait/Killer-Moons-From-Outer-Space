using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCannon : MonoBehaviour
{
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        BulletEmitter bulletEmitter = GetComponent<BulletEmitter>();

        if (bulletEmitter)
        {
            coroutine = bulletEmitter.Emit();
        }
    }

    public void StartFiring()
    {
        if (coroutine != null)
        {
            StartCoroutine(coroutine);
        }
    }

    public void StopFiring()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
