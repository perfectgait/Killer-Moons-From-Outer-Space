using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoss : MonoBehaviour
{
    PathMovement pathMovement;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        pathMovement = GetComponent<PathMovement>();
        StartCoroutine(SlowMovement());

        audioManager = AudioManager.instance;
        StartCoroutine(PlayBossAudio());
    }

    // Update is called once per frame
    void Update()
    {
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
}
