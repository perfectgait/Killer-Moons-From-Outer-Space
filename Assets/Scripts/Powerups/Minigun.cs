using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Powerup
{
    private float waitTimeBetweenBullets = 0.15f;
    private float maximumHeatLevel = 3.0f;
    private float currentHeatLevel = 0.0f;
    // Lower value means the weapon will overheat more slowly
    private float rateOfHeatIncreaseRelativeToTime = 0.75f;
    // Higher value means the weapon will cooldown more quickly
    private float rateOfHeatDecreaseRelativeToTime = 2.0f;
    private float overheatExtraHeatApplied = 3.0f;
    private bool overheated = false;

    public override void Apply(MonoBehaviour behaviour)
    {
        if (behaviour is Player)
        {
            Player player = behaviour as Player;
            BulletEmitter emitter = behaviour.GetComponent<BulletEmitter>();

            if (emitter)
            {
                AdjustHeatLevel(player.IsFiring());

                player.SetCanFire(CanFire());

                emitter.SetWaitTimeBetweenBullets(waitTimeBetweenBullets);
                emitter.SetBulletSfx("Fast Laser");
            }
        }
    }

    private bool CanFire()
    {
        return !overheated && currentHeatLevel < maximumHeatLevel;
    }

    private void AdjustHeatLevel(bool isFiring)
    {
        if (isFiring)
        {
            currentHeatLevel += Time.deltaTime * rateOfHeatIncreaseRelativeToTime;

            // If the player overheats the weapon they cannot fire again until the weapon has fully cooled down
            if (currentHeatLevel >= maximumHeatLevel)
            {
                overheated = true;
                currentHeatLevel += overheatExtraHeatApplied;
            }
            else
            {
                currentHeatLevel = Mathf.Min(maximumHeatLevel, currentHeatLevel);
            }
        }
        else
        {
            currentHeatLevel -= Time.deltaTime * rateOfHeatDecreaseRelativeToTime;
            currentHeatLevel = Mathf.Max(0, currentHeatLevel);
        }

        // The player overheated the weapon but the weapon has fully cooled down
        if (overheated && currentHeatLevel <= 0)
        {
            overheated = false;
        }

        Debug.Log("currentHeatLevel: " + currentHeatLevel);
    }

    public float GetCurrentHeatLevel()
    {
        return currentHeatLevel;
    }
}