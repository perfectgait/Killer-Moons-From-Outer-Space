using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    [SerializeField] GameObject invulnerabilityShield;
    [SerializeField] float delayUntilFightStarts = 5.0f;
    [SerializeField] float delayUntilShieldStartsLowering = 2.0f;
    [SerializeField] float delayUntilAttackStartsAfterShieldIslowered = 1.0f;
    [SerializeField] MoonSatellite[] moonSatellites;
    [SerializeField] MoonCannon eyeCannon;
    [SerializeField] float delayBetweenEyeCannonAttacks = 15.0f;
    [SerializeField] MoonCannon[] heavyCannons;
    [SerializeField] float delayBetweenHeavyCannonAttacks = 10.0f;
    [SerializeField] MoonCannon[] lightCannons;
    [SerializeField] float delayBetweenLightCannonAttacks = 5.0f;

    private PathMovement pathMovement;
    private AudioManager audioManager;
    private MoonSatelliteCoordinator satelliteCoordinator;

    // Start is called before the first frame update
    void Start()
    {
        satelliteCoordinator = GetComponent<MoonSatelliteCoordinator>();
        audioManager = AudioManager.instance;

        StartCoroutine(StartFight());
    }

    public MoonCannon GetEyeCannon()
    {
        return eyeCannon;
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
        StopCannonAttack(new MoonCannon[] { eyeCannon });
        StopCannonAttack(heavyCannons);
        StopCannonAttack(lightCannons);

        if (satelliteCoordinator)
        {
            satelliteCoordinator.StopAttacking();
        }
    }

    private IEnumerator StartFight()
    {
        // Disable satellites until the moon is in position
        if (satelliteCoordinator)
        {
            satelliteCoordinator.DisableSatellites();
        }

        yield return new WaitForSeconds(delayUntilFightStarts);

        pathMovement = GetComponent<PathMovement>();

        // Start the music coroutine
        StartCoroutine(PlayBossAudio());

        // Move the moon into position
        if (pathMovement)
        {
            while (!pathMovement.finishedMoving)
            {
                yield return new WaitForSeconds(.5f);
                pathMovement.movementSpeed *= .92f;
            }
        }

        yield return new WaitForSeconds(delayUntilShieldStartsLowering);

        // Flash then lower the shield
        SpriteFlasher spriteFlasher = invulnerabilityShield.GetComponent<SpriteFlasher>();

        spriteFlasher.Flash();

        // Wait for the flashing to stop
        yield return new WaitForSeconds(spriteFlasher.GetFlashDuration());

        invulnerabilityShield.SetActive(false);

        yield return new WaitForSeconds(delayUntilAttackStartsAfterShieldIslowered);

        if (satelliteCoordinator)
        {
            satelliteCoordinator.InitializeSatellites();
        }

        StartCoroutine(CannonAttack(new MoonCannon[] { eyeCannon }, delayBetweenEyeCannonAttacks));
        StartCoroutine(CannonAttack(heavyCannons, delayBetweenHeavyCannonAttacks));
        StartCoroutine(CannonAttack(lightCannons, delayBetweenLightCannonAttacks));
        StartCoroutine(SatelliteAttack());
    }

    private IEnumerator PlayBossAudio()
    {
        audioManager.StopCurrentlyPlayingMusic();

        while (!pathMovement.finishedMoving)
        {
            audioManager.PlaySoundEffect("Mechanical Noise");
            yield return new WaitForSeconds(1.532f + 1f);
        }

        audioManager.PlayMusic("Boss Battle");
    }

    private IEnumerator CannonAttack(MoonCannon[] moonCannons, float delayBetweenAttacks)
    {
        while (true)
        {
            foreach (MoonCannon moonCannon in moonCannons)
            {
                if (moonCannon)
                {
                    moonCannon.StartFiring();
                }
            }

            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    private void StopCannonAttack(MoonCannon[] moonCannons)
    {
        foreach (MoonCannon moonCannon in moonCannons)
        {
            if (moonCannon)
            {
                moonCannon.StopFiring();
            }
        }
    }

    private IEnumerator SatelliteAttack()
    {
        if (satelliteCoordinator)
        {
            satelliteCoordinator.EnableSatellites();

            while (true)
            {
                satelliteCoordinator.Attack();

                yield return new WaitForSeconds(satelliteCoordinator.GetDelayBetweenAttacks());
            }
        }
    }
}
