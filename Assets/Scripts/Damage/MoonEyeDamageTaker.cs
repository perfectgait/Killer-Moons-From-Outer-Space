using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonEyeDamageTaker : DamageTaker
{
    [SerializeField] MoonBoss moon;
    [SerializeField] float angleChangeDuringDeathSpin = 20f;
    [SerializeField] float timeToWaitBetweenAngleChangeDuringDeathSpin = 0.01f;

    private MoonDamageTaker moonDamageTaker;
    private SpriteFlasher spriteFlasher;
    private AudioManager audioManager;
    private MoonEye eye;

    // Start is called before the first frame update
    void Start()
    {
        moonDamageTaker = moon.GetComponent<MoonDamageTaker>();
        audioManager = AudioManager.instance;
        spriteFlasher = GetComponent<SpriteFlasher>();
        eye = GetComponent<MoonEye>();
    }

    public override void TakeDamage(float damage)
    {
        if (moonDamageTaker && !moonDamageTaker.IsDying())
        {
            moonDamageTaker.TakeDamage(damage);
            Damage();
        }
    }

    public override void Kill()
    {
        // Make the eye stop following the player so the spin works
        eye.StopFollowingPlayer();
        StartCoroutine(DeathSpin());
    }

    private IEnumerator DeathSpin()
    {
        float currentAngle = gameObject.transform.rotation.z;

        // Just keep spinning since the moon will destroy itself along with all
        // of its child game objects.
        while (true)
        {
            currentAngle += angleChangeDuringDeathSpin;

            if (currentAngle > 360)
            {
                currentAngle = 0;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentAngle);

            yield return new WaitForSeconds(timeToWaitBetweenAngleChangeDuringDeathSpin);
        }
    }

    private void Damage()
    {
        if (audioManager)
        {
            audioManager.PlaySoundEffect("Damage Hit");
        }

        if (spriteFlasher)
        {
            spriteFlasher.Flash();
        }
    }
}
