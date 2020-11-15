using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Powerup
{
    private float waitTimeBetweenBullets = 0.1f;
    private float maximumHeatLevel = 3.0f;
    private float currentHeatLevel = 0.0f;
    private bool wasFiring = false;
    private float timeOfFiringStateChange = 0.0f;

    public override void Apply(MonoBehaviour behaviour)
    {
        if (behaviour is Player)
        {
            Player player = behaviour as Player;
            BulletEmitter emitter = behaviour.GetComponent<BulletEmitter>();

            player.SetCanFire(CanFire());

            if (emitter)
            {
                bool wasPreviouslyFiring = wasFiring;

                //if (player.IsFiring())
                //{
                //    wasFiring = true;
                //}
                wasFiring = player.IsFiring();

                AdjustHeatLevel(wasPreviouslyFiring, player.IsFiring());

                emitter.SetWaitTimeBetweenBullets(waitTimeBetweenBullets);
                emitter.SetBulletSfx("Fast Laser");
            }
        }
    }

    private bool CanFire()
    {
        //return true;

        return currentHeatLevel < maximumHeatLevel;
    }

    private void AdjustHeatLevel(bool wasPreviouslyFiring, bool isFiring)
    {
        if ((isFiring && !wasPreviouslyFiring) || (wasPreviouslyFiring && !isFiring))
        {
            timeOfFiringStateChange = Time.time;

            Debug.Log("timeOfFiringStateChange: " + timeOfFiringStateChange);
        }

        if (isFiring)
        {
            //currentHeatLevel = timeOfFiringStateChange + Time.deltaTime;
            currentHeatLevel = Time.time - timeOfFiringStateChange;
            currentHeatLevel = Mathf.Min(maximumHeatLevel, currentHeatLevel);
        }
        else
        {
            currentHeatLevel -= Time.time - timeOfFiringStateChange;
        }

        Debug.Log("currentHeatLevel: " + currentHeatLevel);
        // @TODO Every second increase heat by 1
        // @TODO Every 0.5 second decrease heat by 1


        // @TODO if currentHeatLevel > 0 and is not firing, decrease heat level

        // @TODO if is firing, increase heat level

        // @TODO Ensure heat level is between 0 and maximum
    }
}