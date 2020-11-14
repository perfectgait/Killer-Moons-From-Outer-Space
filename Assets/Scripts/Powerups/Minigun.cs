using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Powerup
{
    public override void Apply(MonoBehaviour behaviour)
    {
        if (behaviour is Player)
        {
            BulletEmitter emitter = behaviour.GetComponent<BulletEmitter>();

            if (emitter)
            {
                emitter.SetWaitTimeBetweenBullets(0.1f);
            }
        }
    }
}