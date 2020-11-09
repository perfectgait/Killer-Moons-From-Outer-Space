using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BulletEmitter bulletEmitter;

    // Start is called before the first frame update
    void Start()
    {
        bulletEmitter = GetComponent<BulletEmitter>();

        if (bulletEmitter)
        {
            StartCoroutine(bulletEmitter.Emit());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
