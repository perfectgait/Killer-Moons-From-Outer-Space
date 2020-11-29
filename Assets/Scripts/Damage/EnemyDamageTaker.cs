using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaker : DamageTaker
{
    [SerializeField] GameObject objectToDamage;
    [SerializeField] GameObject objectToFlash;
    [SerializeField] int score = 0;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] string explosionSfxName = "Small Explosion";

    protected Health health;
    protected SpriteFlasher spriteFlasher;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        objectToDamage = objectToDamage != null ? objectToDamage : gameObject;
        objectToFlash = objectToFlash != null ? objectToFlash : gameObject;

        health = objectToDamage.GetComponent<Health>();
        audioManager = AudioManager.instance;
        spriteFlasher = objectToFlash.GetComponent<SpriteFlasher>();
    }

    public override void TakeDamage(float damage)
    {
        if (health)
        {
            health.Damage(damage);
        }

        if (health.GetHealth() > 0)
        {
            Damage();
        }
        else
        {
            Kill();
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

    public override void Kill()
    {
        GameScore.instance.IncrementBy(score);

        // Hack: Set delay on destroy so that the Update method in HealthDisplay has a chance to register
        // the final health change before the gameObject is destroyed. Health should probably be updating HealthDisplay
        // but this is fine for now because it is late.
        Destroy(objectToDamage, .01f);
        Explode();

        CameraShake.Shake(0.1f, 0.04f);
    }

    private void Explode()
    {
        if (explosionPrefab)
        {
            if (audioManager)
            {
                audioManager.PlaySoundEffect(explosionSfxName);
            }

            GameObject explosion = Instantiate(explosionPrefab, objectToDamage.transform.position, objectToDamage.transform.rotation);
            Destroy(explosion, 1f);
        }
    }
}
