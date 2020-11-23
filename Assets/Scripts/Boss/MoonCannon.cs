using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCannon : MonoBehaviour
{
    public void StartFiring()
    {
        BulletEmitter bulletEmitter = GetComponent<BulletEmitter>();

        if (bulletEmitter)
        {
            StartCoroutine(bulletEmitter.Emit());
        }
    }
}
