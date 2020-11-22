using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    [SerializeField] MoonCannon[] moonCannons;
    [SerializeField] float delayUntilFightStarts = 5.0f;
    [SerializeField] float delayUntilFiringStarts = 10.0f;

    private float countdownUntilFightStarts = 0.0f;
    private bool fightStarted = false;
    private float countdownUntilFiring = 0.0f;
    private bool firingStarted = false;
    private PathMovement pathMovement;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //pathMovement = GetComponent<PathMovement>();
        //StartCoroutine(SlowMovement());

        //audioManager = AudioManager.instance;
        //StartCoroutine(PlayBossAudio());

        countdownUntilFightStarts = delayUntilFightStarts;
        countdownUntilFiring = delayUntilFiringStarts;
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
    }

    private void StartFight()
    {
        if (!fightStarted && countdownUntilFightStarts <= 0)
        {
            fightStarted = true;

            pathMovement = GetComponent<PathMovement>();
            StartCoroutine(SlowMovement());

            audioManager = AudioManager.instance;
            StartCoroutine(PlayBossAudio());
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
                moonCannon.StartFiring();
            }
        }
    }
}
