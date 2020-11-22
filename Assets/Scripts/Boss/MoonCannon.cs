using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonCannon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartFiring()
    {
        BulletEmitter bulletEmitter = GetComponent<BulletEmitter>();

        if (bulletEmitter)
        {
            StartCoroutine(bulletEmitter.Emit());
        }
    }
}
