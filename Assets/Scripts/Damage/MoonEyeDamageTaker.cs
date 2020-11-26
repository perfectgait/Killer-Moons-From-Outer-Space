using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonEyeDamageTaker : DamageTaker
{
    [SerializeField] MoonBoss moon;

    private MoonDamageTaker moonDamageTaker;
    private SpriteFlasher spriteFlasher;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        moonDamageTaker = moon.GetComponent<MoonDamageTaker>();
        audioManager = AudioManager.instance;
        spriteFlasher = GetComponent<SpriteFlasher>();
    }

    public override void TakeDamage(float damage)
    {
        if (moonDamageTaker)
        {
            moonDamageTaker.TakeDamage(damage);
            Damage();
        }
    }

    private void Damage()
    {
        audioManager.PlaySoundEffect("Damage Hit");
        spriteFlasher.Flash();
    }
}
