using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    [SerializeField] MoonCannon[] moonCannons;
    [SerializeField] GameObject invulnerabilityShield;
    [SerializeField] float delayUntilFightStarts = 5.0f;
    [SerializeField] float delayUntilFiringStarts = 10.0f;
    [SerializeField] MoonSatellite[] moonSatellites;

    private float countdownUntilFightStarts = 0.0f;
    private bool fightStarted = false;
    private float countdownUntilFiring = 0.0f;
    private bool firingStarted = false;
    private bool shieldLowered;
    private PathMovement pathMovement;
    private AudioManager audioManager;
    private MoonSatelliteCoordinator satelliteCoordinator;

    // Start is called before the first frame update
    void Start()
    {
        countdownUntilFightStarts = delayUntilFightStarts;
        countdownUntilFiring = delayUntilFiringStarts;
        satelliteCoordinator = GetComponent<MoonSatelliteCoordinator>();

        if (satelliteCoordinator)
        {
            satelliteCoordinator.DisableSatellites();
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdownUntilFightStarts -= Time.deltaTime;
        countdownUntilFightStarts = Mathf.Max(0, countdownUntilFightStarts);
        countdownUntilFiring -= Time.deltaTime;
        countdownUntilFiring = Mathf.Max(0, countdownUntilFiring);

        StartFight();
        StartFiring();
        LowerShield();
    }

    public MoonCannon[] GetMoonCannons()
    {
        return moonCannons;
    }

    private void StartFight()
    {
        if (!fightStarted && countdownUntilFightStarts <= 0)
        {
            fightStarted = true;

            pathMovement = GetComponent<PathMovement>();

            if (pathMovement)
            {
                StartCoroutine(SlowMovement());
            }

            audioManager = AudioManager.instance;

            if (audioManager)
            {
                StartCoroutine(PlayBossAudio());
            }
        }
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


    private IEnumerator SlowMovement()
    {
        while (!pathMovement.finishedMoving)
        {
            yield return new WaitForSeconds(.5f);
            pathMovement.movementSpeed *= .92f;
        }
    }

    private void StartFiring()
    {
        if (!firingStarted && countdownUntilFiring <= 0)
        {
            firingStarted = true;

            foreach (MoonCannon moonCannon in moonCannons)
            {
                if (moonCannon)
                {
                    moonCannon.StartFiring();
                }
            }

            if (satelliteCoordinator)
            {
                satelliteCoordinator.EnableSatellites();
                StartCoroutine(satelliteCoordinator.Attack());
            }
        }
    }

    private void LowerShield()
    {
        if (!shieldLowered && countdownUntilFiring <= 0)
        {
            invulnerabilityShield.SetActive(false);
        }
    }
}
