using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @TODO Create powerup interface

public class Minigun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply(BulletEmitter emitter)
    {
        emitter.SetWaitTimeBetweenBullets(0.01f);
    }
}
