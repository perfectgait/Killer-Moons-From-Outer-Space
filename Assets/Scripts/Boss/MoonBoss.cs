using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    [SerializeField] MoonCannon[] moonCannons;
    [SerializeField] GameObject invulnerabilityShield;
    [SerializeField] float delayUntilFightStarts = 5.0f;
    [SerializeField] float delayUntilShieldStartsLowering = 2.0f;
    [SerializeField] float delayUntilAttackStartsAfterShieldIslowered = 1.0f;
    [SerializeField] MoonSatellite[] moonSatellites;

    private PathMovement pathMovement;
    private AudioManager audioManager;
    private MoonSatelliteCoordinator satelliteCoordinator;
    private IEnumerator attackCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        satelliteCoordinator = GetComponent<MoonSatelliteCoordinator>();
        audioManager = AudioManager.instance;
        attackCoroutine = Attack();

        StartCoroutine(StartFight());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public MoonCannon[] GetMoonCannons()
    {
        return moonCannons;
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
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

        StartCoroutine(attackCoroutine);
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

    private IEnumerator Attack()
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

            // Start the satellite attack
            if (satelliteCoordinator)
            {
                //satelliteCoordinator.InitializeSatellites();
                satelliteCoordinator.EnableSatellites();
                //StartCoroutine(satelliteCoordinator.Attack());
                satelliteCoordinator.Attack();
            }

            // @TODO Have a coroutine for the satellites and one for the cannons so each can fire at different intervals
            yield return new WaitForSeconds(15.0f);
        }
    }
}
